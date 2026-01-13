using MediatR;
using SouQna.Domain.Extensions;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;
using SouQna.Application.Exceptions.Enums;
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
            ) ?? throw new AuthenticationException(
                AuthenticationErrorType.InvalidRefreshToken,
                "Invalid refresh token"
            );

            var refreshToken = user.RefreshTokens.Single(t => t.Token == command.RefreshToken);

            if (refreshToken.IsRevoked)
            {
                throw new AuthenticationException(
                    AuthenticationErrorType.TokenRevoked,
                    "Refresh token has been revoked"
                );
            }

            if (refreshToken.IsExpired)
            {
                throw new AuthenticationException(
                    AuthenticationErrorType.TokenExpired,
                    "Refresh token has expired"
                );
            }

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