namespace SouQna.Application.Interfaces
{
    public interface IEmailTemplateService
    {
        string GetConfirmationEmail(string name, string confirmationLink);
    }
}