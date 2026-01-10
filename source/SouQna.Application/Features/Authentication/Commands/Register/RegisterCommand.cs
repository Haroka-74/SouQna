using MediatR;

namespace SouQna.Application.Features.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<RegisterResponse>;
}