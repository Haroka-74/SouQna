using MediatR;

namespace SouQna.Application.Features.Authentication.Register
{
    public record RegisterRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<RegisterResponse>;
}