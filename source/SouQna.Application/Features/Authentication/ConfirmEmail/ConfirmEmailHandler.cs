using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Authentication.ConfirmEmail
{
    public class ConfirmEmailHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<ConfirmEmailRequest>
    {
        public async Task Handle(
            ConfirmEmailRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.Users.FindAsync(
                u => u.EmailConfirmationToken == request.Token
            ) ?? throw new NotFoundException($"User with token ({request.Token}) was not found");

            user.ConfirmEmail(request.Token);

            await unitOfWork.SaveChangesAsync();
        }
    }
}