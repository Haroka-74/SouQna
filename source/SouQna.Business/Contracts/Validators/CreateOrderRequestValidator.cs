using FluentValidation;
using SouQna.Business.Contracts.Requests;

namespace SouQna.Business.Contracts.Validators
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .MaximumLength(100).WithMessage("Full name must not exceed 100 characters");

            RuleFor(x => x.AddressLine)
                .NotEmpty().WithMessage("Address line is required")
                .MaximumLength(250).WithMessage("Address must not exceed 250 characters");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(50).WithMessage("City name must not exceed 50 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^(010|011|012|015)[0-9]{8}$")
                    .WithMessage("A valid mobile number is required (e.g., 01012345678)");
        }
    }
}