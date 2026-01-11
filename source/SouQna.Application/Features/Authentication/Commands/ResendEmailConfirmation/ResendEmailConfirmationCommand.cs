using MediatR;

namespace SouQna.Application.Features.Authentication.Commands.ResendEmailConfirmation
{
    public record ResendEmailConfirmationCommand(
        string Email
    ) : IRequest;
}