using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using SouQna.Application.Interfaces;
using SouQna.Infrastructure.Configuration.Settings;

namespace SouQna.Infrastructure.Services.Email
{
    public class EmailService(
        EmailSettings emailSettings
    ) : IEmailService
    {
        public async Task SendAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(emailSettings.Smtp.Sender));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new BodyBuilder { HtmlBody = body }.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                emailSettings.Smtp.Host,
                emailSettings.Smtp.Port,
                SecureSocketOptions.StartTls
            );
            await smtp.AuthenticateAsync(
                emailSettings.Smtp.Username,
                emailSettings.Smtp.Password
            );
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}