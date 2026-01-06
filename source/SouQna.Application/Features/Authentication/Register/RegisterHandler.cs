using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;
using SouQna.Domain.Aggregates.UserAggregate;

namespace SouQna.Application.Features.Authentication.Register
{
    public class RegisterHandler(
        IUnitOfWork unitOfWork,
        ICryptoService cryptoService
    ) : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        public async Task<RegisterResponse> Handle(
            RegisterRequest request,
            CancellationToken cancellationToken
        )
        {
            if(await unitOfWork.Users.FindAsync(u => u.Email == request.Email) is not null)
                throw new ConflictException($"The email address '{request.Email}' is already associated with an account");

            var user = await unitOfWork.Users.AddAsync(
                User.Create(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    cryptoService.Hash(request.Password)
                )
            );

            await unitOfWork.SaveChangesAsync();

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