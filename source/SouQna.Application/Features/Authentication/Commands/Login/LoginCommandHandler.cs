using MediatR;
using SouQna.Domain.Extensions;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;
using SouQna.Application.Exceptions.Enums;
using TokenEntity = SouQna.Domain.Aggregates.UserAggregate;

namespace SouQna.Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler(
        IUnitOfWork unitOfWork,
        ICryptoService cryptoService,
        IJwtTokenService jwtTokenService,
        IRandomTokenService randomTokenService
    ) : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(
            LoginCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.Users.FindAsync(
                u => u.Email == command.Email,
                u => u.RefreshTokens
            );

            if(user is null || !cryptoService.Verify(command.Password, user.PasswordHash))
            {
                throw new AuthenticationException(
                    AuthenticationErrorType.InvalidCredentials,
                    "Email or Password is incorrect!"
                );
            }

            if(!user.EmailConfirmed)
            {
                throw new AuthenticationException(
                    AuthenticationErrorType.EmailNotConfirmed,
                    "Email not confirmed. Please verify your account!"
                );
            }

            var accessToken = jwtTokenService.Generate(user);
            var refreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsValid);

            user.CleanupInvalidRefreshTokens();

            if(refreshToken is null)
            {
                refreshToken = TokenEntity.RefreshToken.Create(
                    user.Id,
                    randomTokenService.Generate(64),
                    7
                );

                user.AddRefreshToken(refreshToken);
            }

            await unitOfWork.SaveChangesAsync();

            return new LoginResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiration = refreshToken.ExpiresAt
            };
        }
    }
}