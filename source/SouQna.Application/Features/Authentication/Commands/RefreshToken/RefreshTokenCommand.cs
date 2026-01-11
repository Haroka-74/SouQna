using MediatR;

namespace SouQna.Application.Features.Authentication.Commands.RefreshToken
{
    public record RefreshTokenCommand(
        string RefreshToken
    ) : IRequest<RefreshTokenResponse>;
}