using MediatR;

namespace SouQna.Application.Features.Authentication.Customer.LoginUser
{
    public record LoginUserRequest(
        string Email,
        string Password
    ) : IRequest<DTOs.LoginUserDTO>;
}