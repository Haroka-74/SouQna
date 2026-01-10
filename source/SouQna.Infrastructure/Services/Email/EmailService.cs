using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using SouQna.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace SouQna.Infrastructure.Services.Email
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        public async Task SendAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(configuration["Email:Smtp:Sender"]));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new BodyBuilder { HtmlBody = body }.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(configuration["Email:Smtp:Host"], int.Parse(configuration["Email:Smtp:Port"]!), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(configuration["Email:Smtp:Username"], configuration["Email:Smtp:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}