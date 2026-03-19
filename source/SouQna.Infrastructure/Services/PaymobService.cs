using System.Text.Json;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using SouQna.Application.Interfaces;
using SouQna.Infrastructure.Settings;
using SouQna.Application.DTOs.Orders;

namespace SouQna.Infrastructure.Services
{
    public class PaymobService(
        HttpClient client,
        PaymobSettings paymobSettings,
        ServerSettings serverSettings,
        ClientSettings clientSettings
    ) : IPaymentService
    {
        public async Task<(
            long IntentionOrderId,
            DateTime CreatedAt,
            string CheckoutUrl
        )> CreateIntentionAsync(
            string userEmail,
            Guid orderId,
            decimal total,
            string shippingFullName,
            string shippingPhoneNumber,
            string shippingCity,
            string shippingAddressLine,
            List<OrderItemDTO> orderItems
        )
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", paymobSettings.SecretKey);

            var tax = total - orderItems.Sum(d => d.Subtotal);

            var items = orderItems.Select(d => new
            {
                name = d.ItemName,
                amount = (long) (d.ItemPrice * 100),
                quantity = d.ItemQuantity
            }).ToList();

            items.Add(new
            {
                name = "Tax",
                amount = (long) (tax * 100),
                quantity = 1
            });

            var requestBody = new
            {
                amount = (long) (total * 100),
                currency = "EGP",
                payment_methods = new[] { 5514498, 5514764 },
                items = items.ToArray(),
                billing_data = new
                {
                    first_name = shippingFullName.Split(' ')[0],
                    last_name = shippingFullName.Contains(' ')
                        ? shippingFullName.Split(' ')[1]
                        : shippingFullName,
                    street = shippingAddressLine,
                    phone_number = shippingPhoneNumber,
                    city = shippingCity,
                    email = userEmail
                },
                extras = new
                {
                    order_id = orderId.ToString()
                },
                special_reference = $"{orderId}_{Guid.NewGuid()}",
                expiration = 900,
                notification_url = $"{serverSettings.BaseAddress}/api/payments/webhook",
                redirection_url = $"{clientSettings.BaseAddress}"
            };

            var response = await client.PostAsJsonAsync(
                "/v1/intention/",
                requestBody
            );

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStreamAsync();
            using var document = await JsonDocument.ParseAsync(content);

            var intentionOrderId = document.RootElement.GetProperty("intention_order_id").GetInt64();
            var created = document.RootElement.GetProperty("created").GetDateTime().ToUniversalTime();
            var clientSecret = document.RootElement.GetProperty("client_secret").GetString();

            var checkoutUrl = $"{paymobSettings.BaseAddress}/unifiedcheckout/?publicKey={paymobSettings.PublicKey}&clientSecret={clientSecret}";

            return (intentionOrderId, created, checkoutUrl);
        }
    }
}