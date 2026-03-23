using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Authentication.Customer.LoginUser
{
    public class LoginUserRequestHandler(
        IUnitOfWork unitOfWork,
        IHasher hasher,
        IJwtService jwtService
    ) : IRequestHandler<LoginUserRequest, DTOs.LoginUserDTO>
    {
        public async Task<DTOs.LoginUserDTO> Handle(
            LoginUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            var user = await unitOfWork.Users.FindAsync(
                u => u.Email == normalizedEmail,
                "UserRoles.Role"
            );

            if(user is null || !hasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException("Invalid Email or Password");

            return new DTOs.LoginUserDTO(jwtService.Generate(user));
        }
    }
}