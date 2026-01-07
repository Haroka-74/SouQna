using MediatR;

namespace SouQna.Application.Features.Authentication.ConfirmEmail
{
    public record ConfirmEmailRequest(
        string Token
    ) : IRequest;
}