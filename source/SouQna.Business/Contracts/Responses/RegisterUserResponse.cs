namespace SouQna.Business.Contracts.Responses
{
    public record RegisterUserResponse(
        Guid Id,
        string FullName,
        string Email
    );
}