using MediatR;

namespace SouQna.Application.Features.Authentication.Commands.RevokeToken
{
    public record RevokeTokenCommand(
        string RefreshToken
    ) : IRequest;
}