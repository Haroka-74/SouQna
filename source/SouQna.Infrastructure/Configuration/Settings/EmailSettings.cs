namespace SouQna.Infrastructure.Configuration.Settings
{
    public class EmailSettings
    {
        public SmtpSettings Smtp { get; set; } = new();
    }
}