using MediatR;

namespace SouQna.Application.Features.Authentication.RegisterUser
{
    public record RegisterUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<RegisterUserResponse>;
}