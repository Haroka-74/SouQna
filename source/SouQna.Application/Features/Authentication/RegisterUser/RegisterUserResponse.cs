namespace SouQna.Application.Features.Authentication.RegisterUser
{
    public record RegisterUserResponse(
        Guid Id,
        string FirstName,
        string LastName,
        string Email
    );
}