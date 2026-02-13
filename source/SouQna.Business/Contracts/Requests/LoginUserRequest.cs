namespace SouQna.Business.Contracts.Requests
{
    public record LoginUserRequest(
        string Email,
        string Password
    );
}