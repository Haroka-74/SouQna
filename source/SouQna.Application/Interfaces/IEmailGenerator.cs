namespace SouQna.Application.Interfaces
{
    public interface IEmailGenerator
    {
        string GetConfirmationEmail(string name, string confirmationLink);
    }
}