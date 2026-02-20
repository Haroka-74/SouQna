using FluentValidation;
using SouQna.Business.Contracts.Requests.Carts;

namespace SouQna.Business.Contracts.Validators.Carts
{
    public class UpdateCartItemRequestValidator : AbstractValidator<UpdateCartItemRequest>
    {
        public UpdateCartItemRequestValidator()
        {
            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required")
                .InclusiveBetween(1, 50).WithMessage("Quantity must be between 1 and 50");
        }
    }
}