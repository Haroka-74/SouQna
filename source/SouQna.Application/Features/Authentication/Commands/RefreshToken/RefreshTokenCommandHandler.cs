using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;
using SouQna.Domain.Aggregates.UserAggregate.Extensions;
using TokenEntity = SouQna.Domain.Aggregates.UserAggregate;

namespace SouQna.Application.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(
        IUnitOfWork unitOfWork,
        IRandomTokenService randomTokenService,
        IJwtTokenService jwtTokenService
    ) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        public async Task<RefreshTokenResponse> Handle(
            RefreshTokenCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.Users.FindAsync(
                u => u.RefreshTokens.Any(
                    t => t.Token == command.RefreshToken
                ),
                u => u.RefreshTokens
            ) ?? throw new InvalidRefreshTokenException("Invalid refresh token");

            var refreshToken = user.RefreshTokens.Single(t => t.Token == command.RefreshToken);

            if (!refreshToken.IsValid())
                throw new InvalidRefreshTokenException("Refresh token has expired or been revoked");

            refreshToken.Revoke();

            user.CleanupInvalidRefreshTokens();

            var newAccessToken = jwtTokenService.Generate(user);
            var newRefreshToken = TokenEntity.RefreshToken.Create(
                user.Id,
                randomTokenService.Generate(64),
                7
            );

            user.AddRefreshToken(newRefreshToken);
            await unitOfWork.SaveChangesAsync();

            return new RefreshTokenResponse()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiresAt
            };
        }
    }
}