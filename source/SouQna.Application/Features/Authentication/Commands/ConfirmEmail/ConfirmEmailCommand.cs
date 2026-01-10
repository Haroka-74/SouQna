using MediatR;

namespace SouQna.Application.Features.Authentication.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(
        string Token
    ) : IRequest;
}