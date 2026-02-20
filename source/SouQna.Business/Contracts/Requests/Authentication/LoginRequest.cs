namespace SouQna.Business.Contracts.Requests.Authentication
{
    public record LoginRequest(
        string Email,
        string Password
    );
}