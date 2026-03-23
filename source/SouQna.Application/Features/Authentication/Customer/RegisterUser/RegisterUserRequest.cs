using MediatR;

namespace SouQna.Application.Features.Authentication.Customer.RegisterUser
{
    public record RegisterUserRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<DTOs.RegisterUserDTO>;
}