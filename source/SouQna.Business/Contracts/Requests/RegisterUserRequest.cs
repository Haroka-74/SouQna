namespace SouQna.Business.Contracts.Requests
{
    public record RegisterUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}