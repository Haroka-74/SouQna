using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;
using SouQna.Domain.Aggregates.UserAggregate;

namespace SouQna.Application.Features.Authentication.Register
{
    public class RegisterHandler(
        ICryptoService cryptoService,
        IUnitOfWork unitOfWork,
        ITokenGenerator tokenGenerator,
        IEmailService emailService,
        IEmailGenerator emailGenerator
    ) : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        public async Task<RegisterResponse> Handle(
            RegisterRequest request,
            CancellationToken cancellationToken
        )
        {
            if(await unitOfWork.Users.FindAsync(u => u.Email == request.Email) is not null)
                throw new ConflictException($"The email address '{request.Email}' is already associated with an account");

            var user = User.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                cryptoService.Hash(request.Password)
            );

            var confirmationToken = tokenGenerator.Generate();

            user.SetEmailConfirmationToken(confirmationToken, 2);

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            var confirmationLink = $"http://localhost:5239/api/auth/confirm-email?token={Uri.EscapeDataString(confirmationToken)}";

            var body = emailGenerator.GetConfirmationEmail($"{user.FirstName}", confirmationLink);

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