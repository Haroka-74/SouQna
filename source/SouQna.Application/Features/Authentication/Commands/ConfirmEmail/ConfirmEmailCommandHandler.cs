using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<ConfirmEmailCommand>
    {
        public async Task Handle(
            ConfirmEmailCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.Users.FindAsync(
                u => u.EmailConfirmationToken == command.Token
            ) ?? throw new NotFoundException($"User with token ({command.Token}) was not found");

            user.ConfirmEmail(command.Token);
            await unitOfWork.SaveChangesAsync();
        }
    }
}