using System.Text.Json;
using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Services
{
    public class PaymobWebhookParser : IWebhookParser
    {
        public (bool Success, Guid OrderId, long IntentionOrderId) Parse(string json)
        {
            using var document = JsonDocument.Parse(json);

            var root = document.RootElement;

            var type = root.GetProperty("type").GetString();

            if (type != "TRANSACTION")
                return (false, Guid.Empty, 0);

            var obj = root.GetProperty("obj");
            var success = obj.GetProperty("success").GetBoolean();
            var intentionOrderId = obj.GetProperty("order").GetProperty("id").GetInt64();

            if(!Guid.TryParse(
                obj
                    .GetProperty("payment_key_claims")
                    .GetProperty("extra")
                    .GetProperty("order_id")
                    .GetString(),
                out var orderId
                )
            )
                return (false, Guid.Empty, 0);

            return (success, orderId, intentionOrderId);
        }
    }
}