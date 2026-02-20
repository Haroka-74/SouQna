namespace SouQna.Business.Contracts.Responses.Authentication
{
    public record RegisterResponse(
        Guid Id,
        string FullName,
        string Email
    );
}