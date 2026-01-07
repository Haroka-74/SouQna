using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Services
{
    public class EmailGenerator : IEmailGenerator
    {
        public string GetConfirmationEmail(string name, string confirmationLink)
        {
            return
            $@"
                <html>
                <body>
                    <p> Hello {name}, </p>
                    <p> Please confirm your email by clicking the link below: </p>
                    <p><a href = '{confirmationLink}'> Confirm Email </a></p>
                </body>
                </html>
            ";
        }
    }
}