using FluentValidation;
using SouQna.Business.Contracts.Requests;

namespace SouQna.Business.Contracts.Validators
{
    public class AddToCartRequestValidator : AbstractValidator<AddToCartRequest>
    {
        public AddToCartRequestValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required")
                .InclusiveBetween(1, 50).WithMessage("Quantity must be between 1 and 50");
        }
    }
}