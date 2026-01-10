using FluentValidation;

namespace SouQna.Application.Features.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
    {
        public ConfirmEmailCommandValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("First name is required");
        }
    }
}