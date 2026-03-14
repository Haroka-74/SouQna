namespace SouQna.Infrastructure.Settings
{
    public class PaymobSettings
    {
        public string BaseAddress { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string PublicKey { get; set; } = string.Empty;
        public string HmacSecret { get; set; } = string.Empty;
    }
}