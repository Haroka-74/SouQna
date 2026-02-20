using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using SouQna.Business.Interfaces;
using SouQna.Business.Exceptions;
using SouQna.Infrastructure.Enums;
using System.Security.Cryptography;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Infrastructure.Configurations.Settings;

namespace SouQna.Business.Services
{
    public class PaymobService(
        HttpClient client,
        IUnitOfWork unitOfWork,
        PaymobSettings paymobSettings,
        ServerSettings serverSettings
    ) : IPaymentService
    {
        public async Task<string> CreatePaymentIntentAsync(Guid orderId)
        {
            var order = await unitOfWork.Orders.FindAsync(
                o => o.Id == orderId,
                o => o.User,
                o => o.OrderItems
            ) ?? throw new NotFoundException($"Order with (id: {orderId}) was not found");

            if(order.Status != OrderStatus.Pending)
                throw new InvalidOrderStateException($"Order {orderId} status is {order.Status}");

            var existingPayment = await unitOfWork.Payments.FindAsync(
                p => p.OrderId == orderId
            );

            if(existingPayment is not null)
            {
                if(existingPayment.Status == PaymentStatus.Pending)
                    throw new ConflictException($"Order with (id: {orderId}) already has an active payment intent");

                if(existingPayment.Status == PaymentStatus.Succeeded)
                    throw new ConflictException($"Order with (id: {orderId}) is already paid");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", paymobSettings.SecretKey);

            var subtotal = order.OrderItems.Sum(oi => oi.Price * oi.Quantity);
            var taxAndFees = order.Total - subtotal;

            var items = order.OrderItems.Select(oi => new
            {
                name = oi.ProductName,
                amount = (long) (oi.Price * 100),
                quantity = oi.Quantity
            }).ToList();

            items.Add(new
            {
                name = "Tax & Service Fees",
                amount = (long) (taxAndFees * 100),
                quantity = 1
            });

            var requestBody = new
            {
                amount = (long) (order.Total * 100),
                currency = "EGP",
                payment_methods = new[] { 5514498, 5514764 },
                items = items.ToArray(),
                billing_data = new
                {
                    first_name = order.ShippingFullName.Split(' ')[0],
                    last_name = order.ShippingFullName.Contains(' ')
                        ? order.ShippingFullName.Split(' ')[1]
                        : order.ShippingFullName,
                    street = order.ShippingAddressLine,
                    phone_number = order.ShippingPhoneNumber,
                    city = order.ShippingCity,
                    email = order.User.Email
                },
                extras = new
                {
                    order_id = order.Id.ToString()
                },
                special_reference = $"{orderId}_{Guid.NewGuid()}",
                expiration = 3600,
                notification_url = $"{serverSettings.BaseAddress}/api/payments/webhook",
                redirection_url = "https://frontend.com/payment-result"
            };

            var response = await client.PostAsJsonAsync(
                "/v1/intention/",
                requestBody
            );

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStreamAsync();
            using var document = await JsonDocument.ParseAsync(content);

            var intentionId = document.RootElement.GetProperty("id").GetString();
            var clientSecret = document.RootElement.GetProperty("client_secret").GetString();
            var redirectionUrl = document.RootElement.GetProperty("redirection_url").GetString();

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Amount = order.Total,
                Status = PaymentStatus.Pending,
                TransactionId = null,
                IntentId = intentionId!,
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.Payments.AddAsync(payment);
            await unitOfWork.SaveChangesAsync();

            return $"{paymobSettings.BaseAddress}/unifiedcheckout/?publicKey={paymobSettings.PublicKey}&clientSecret={clientSecret}";
        }

        public async Task ProcessPaymentWebhookAsync(string json, string hmac)
        {
            using var document = JsonDocument.Parse(json);

            var obj = document.RootElement.GetProperty("obj");

            var concatenation =
                obj.GetProperty("amount_cents").GetRawText().Replace("\"", "") +
                obj.GetProperty("created_at").GetRawText().Replace("\"", "") +
                obj.GetProperty("currency").GetRawText().Replace("\"", "") +
                obj.GetProperty("error_occured").GetRawText().Replace("\"", "").ToLower() +
                obj.GetProperty("has_parent_transaction").GetRawText().Replace("\"", "").ToLower() +
                obj.GetProperty("id").GetRawText().Replace("\"", "") +
                obj.GetProperty("integration_id").GetRawText().Replace("\"", "") +
                obj.GetProperty("is_3d_secure").GetRawText().Replace("\"", "").ToLower() +
                obj.GetProperty("is_auth").GetRawText().Replace("\"", "").ToLower() +
                obj.GetProperty("is_capture").GetRawText().Replace("\"", "").ToLower() +
                obj.GetProperty("is_refunded").GetRawText().Replace("\"", "").ToLower() +
                obj.GetProperty("is_standalone_payment").GetRawText().Replace("\"", "").ToLower() +
                obj.GetProperty("is_voided").GetRawText().Replace("\"", "").ToLower() +
                obj.GetProperty("order").GetProperty("id").GetRawText().Replace("\"", "") +
                obj.GetProperty("owner").GetRawText().Replace("\"", "") +
                obj.GetProperty("pending").GetRawText().Replace("\"", "").ToLower() +
                obj.GetProperty("source_data").GetProperty("pan").GetRawText().Replace("\"", "") +
                obj.GetProperty("source_data").GetProperty("sub_type").GetRawText().Replace("\"", "") +
                obj.GetProperty("source_data").GetProperty("type").GetRawText().Replace("\"", "") +
                obj.GetProperty("success").GetRawText().Replace("\"", "").ToLower();

            using var hasher = new HMACSHA512(
                Encoding.UTF8.GetBytes(paymobSettings.HmacSecret)
            );

            var calculated = Convert.ToHexStringLower(
                hasher.ComputeHash(
                    Encoding.UTF8.GetBytes(concatenation)
                )
            );

            if(calculated != hmac)
                throw new UnauthorizedException("Invalid HMAC");

            if(!Guid.TryParse(
                obj
                    .GetProperty("payment_key_claims")
                    .GetProperty("extra")
                    .GetProperty("order_id")
                    .GetString(),
                out var orderId
                )
            )
                return;

            var payment = await unitOfWork.Payments.FindAsync(
                p => p.OrderId == orderId,
                p => p.Order
            );

            if(payment is null)
                return;

            bool success = obj.GetProperty("success").GetBoolean();

            if(success)
            {
                payment.Status = PaymentStatus.Succeeded;
                payment.TransactionId = obj.GetProperty("id").GetRawText();
                payment.Order.Status = OrderStatus.Confirmed;
                payment.Order.ConfirmedAt = DateTime.UtcNow;

                var userId = payment.Order.UserId;
                var cart = await unitOfWork.Carts.FindAsync(
                    c => c.UserId == userId,
                    c => c.CartItems
                );

                if(cart is not null && cart.CartItems.Count != 0)
                {
                    await unitOfWork.CartItems.DeleteRangeAsync(cart.CartItems);
                }
            }
            else
            {
                payment.Status = PaymentStatus.Failed;
            }

            await unitOfWork.SaveChangesAsync();
        }
    }
}