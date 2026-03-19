using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using SouQna.Application.Interfaces;
using SouQna.Infrastructure.Settings;

namespace SouQna.Infrastructure.Services
{
    public class PaymobHmacVerifier(
        PaymobSettings settings
    ) : IHmacVerifier
    {
        public bool Verify(string json, string hmac)
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
                Encoding.UTF8.GetBytes(settings.HmacSecret)
            );

            var calculated = Convert.ToHexStringLower(
                hasher.ComputeHash(
                    Encoding.UTF8.GetBytes(concatenation)
                )
            );

            return calculated == hmac;
        }
    }
}