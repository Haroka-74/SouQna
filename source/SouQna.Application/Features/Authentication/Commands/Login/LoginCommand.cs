using MediatR;

namespace SouQna.Application.Features.Authentication.Commands.Login
{
    public record LoginCommand(
        string Email,
        string Password
    ) : IRequest<LoginResponse>;
}