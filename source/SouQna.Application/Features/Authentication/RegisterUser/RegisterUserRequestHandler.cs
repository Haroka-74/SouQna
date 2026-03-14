using MediatR;
using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Authentication.RegisterUser
{
    public class RegisterUserRequestHandler(
        IUnitOfWork unitOfWork,
        IHasher hasher,
        IMapper mapper
    ) : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        public async Task<RegisterUserResponse> Handle(
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

            return mapper.Map<RegisterUserResponse>(user);
        }
    }
}