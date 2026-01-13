using MediatR;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Authentication.Commands.RevokeToken
{
    public class RevokeTokenCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<RevokeTokenCommand>
    {
        public async Task Handle(
            RevokeTokenCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.Users.FindAsync(
                u => u.RefreshTokens.Any(
                    t => t.Token == command.RefreshToken
                ),
                u => u.RefreshTokens
            );

            if(user is null)
                return;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == command.RefreshToken);

            if (!refreshToken.IsValid)
                return;

            refreshToken.Revoke();
            await unitOfWork.SaveChangesAsync();
        }
    }
}