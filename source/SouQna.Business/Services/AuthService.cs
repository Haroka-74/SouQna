using SouQna.Business.Exceptions;
using SouQna.Business.Interfaces;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Services
{
    public class AuthService(
        IUnitOfWork unitOfWork,
        IJwtService jwtService,
        IValidationService validationService
    ) : IAuthService
    {
        public async Task<RegisterUserResponse> RegisterAsync(RegisterUserRequest request)
        {
            await validationService.ValidateAsync(request);

            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            if(await unitOfWork.Users.AnyAsync(u => u.Email == normalizedEmail))
                throw new ConflictException("Email address is already registered");

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = normalizedEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            return new RegisterUserResponse(
                user.Id,
                $"{user.FirstName} {user.LastName}",
                user.Email
            );
        }

        public async Task<LoginUserResponse> LoginAsync(LoginUserRequest request)
        {
            await validationService.ValidateAsync(request);

            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var user = await unitOfWork.Users.FindAsync(u => u.Email == normalizedEmail);

            if(user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid email or password");

            return new LoginUserResponse(jwtService.Generate(user));
        }
    }
}