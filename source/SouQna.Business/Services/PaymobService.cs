using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using SouQna.Business.Interfaces;
using SouQna.Business.Exceptions;
using SouQna.Infrastructure.Enums;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Infrastructure.Configurations.Settings;

namespace SouQna.Business.Services
{
    public class PaymobService(
        HttpClient client,
        IUnitOfWork unitOfWork,
        PaymobSettings settings
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

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", settings.SecretKey);

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
                notification_url = "https://webhook.site/58637ab6-b4d5-4e6b-b21f-86ead656d220",
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

            return $"{settings.BaseAddress}/unifiedcheckout/?publicKey={settings.PublicKey}&clientSecret={clientSecret}";
        }
    }
}