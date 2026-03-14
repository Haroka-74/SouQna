using MediatR;

namespace SouQna.Application.Features.Authentication.LoginUser
{
    public record LoginUserRequest(
        string Email,
        string Password
    ) : IRequest<LoginUserResponse>;
}