using FluentValidation;

namespace SouQna.Application.Features.Authentication.ConfirmEmail
{
    public class ConfirmEmailRequestValidator : AbstractValidator<ConfirmEmailRequest>
    {
        public ConfirmEmailRequestValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("First name is required");
        }
    }
}