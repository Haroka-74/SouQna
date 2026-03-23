using MediatR;
using SouQna.Domain.Entities;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;

namespace SouQna.Application.Features.Authentication.Customer.RegisterUser
{
    public class RegisterUserRequestHandler(
        IUnitOfWork unitOfWork,
        IHasher hasher
    ) : IRequestHandler<RegisterUserRequest, DTOs.RegisterUserDTO>
    {
        public async Task<DTOs.RegisterUserDTO> Handle(
            RegisterUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var normalizedEmail = request.Email.Trim().ToLowerInvariant();

            if(await unitOfWork.Users.AnyAsync(u => u.Email == normalizedEmail))
                throw new ConflictException("Email address is already registered");

            var user = User.Create(
                request.FirstName,
                request.LastName,
                request.Email,
                hasher.Hash(request.Password)
            );

            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            return new DTOs.RegisterUserDTO(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email
            );
        }
    }
}