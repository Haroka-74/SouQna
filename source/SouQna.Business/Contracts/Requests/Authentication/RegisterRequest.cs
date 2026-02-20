namespace SouQna.Business.Contracts.Requests.Authentication
{
    public record RegisterRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}