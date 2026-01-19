using FluentValidation;

namespace SouQna.Application.Features.Carts.Commands.AddItemToCart
{
    public class AddItemToCartCommandValidator : AbstractValidator<AddItemToCartCommand>
    {
        public AddItemToCartCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required")
                .InclusiveBetween(1, 100).WithMessage("Quantity must be between 1 and 100");
        }
    }
}