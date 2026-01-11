using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Authentication.Commands.ResendEmailConfirmation
{
    public class ResendEmailConfirmationCommandHandler(
        IUnitOfWork unitOfWork,
        IRandomTokenService randomTokenService,
        IEmailTemplateService emailTemplateService,
        IEmailService emailService
    ) : IRequestHandler<ResendEmailConfirmationCommand>
    {
        public async Task Handle(
            ResendEmailConfirmationCommand command,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.Users.FindAsync(
                u => u.Email == command.Email
            ) ?? throw new NotFoundException($"No account found with email '{command.Email}'");

            if(user.EmailConfirmed)
                throw new InvalidOperationException("Email is already confirmed");

            var confirmationToken = randomTokenService.Generate(64);

            user.SetEmailConfirmationToken(confirmationToken, 2);

            await unitOfWork.SaveChangesAsync();

            var confirmationLink = $"http://localhost:5239/api/auth/confirm-email?token={Uri.EscapeDataString(confirmationToken)}";

            var body = emailTemplateService.GetConfirmationEmail(user.FirstName, confirmationLink);

            _ = emailService.SendAsync(user.Email, "Please confirm your email", body);
        }
    }
}