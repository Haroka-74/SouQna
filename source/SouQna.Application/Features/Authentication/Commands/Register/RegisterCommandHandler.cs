using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;
using SouQna.Domain.Aggregates.UserAggregate;

namespace SouQna.Application.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler(
        ICryptoService cryptoService,
        IUnitOfWork unitOfWork,
        IRandomTokenService randomTokenService,
        IEmailService emailService,
        IEmailTemplateService emailTemplateService
    ) : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        public async Task<RegisterResponse> Handle(
            RegisterCommand command,
            CancellationToken cancellationToken
        )
        {
            var normalizedEmail = command.Email.Trim().ToLowerInvariant();

            if(await unitOfWork.Users.AnyAsync(u => u.Email == normalizedEmail))
                throw new ConflictException($"The email address '{command.Email}' is already registered");

            var user = User.Create(
                command.FirstName,
                command.LastName,
                normalizedEmail,
                cryptoService.Hash(command.Password)
            );

            var confirmationToken = randomTokenService.Generate(64);

            user.SetEmailConfirmationToken(confirmationToken, 2);

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            var confirmationLink = $"http://localhost:5239/api/auth/confirm-email?token={Uri.EscapeDataString(confirmationToken)}";

            var body = emailTemplateService.GetConfirmationEmail(user.FirstName, confirmationLink);

            _ = emailService.SendAsync(user.Email, "Please confirm your email", body);

            return new RegisterResponse()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }
    }
}