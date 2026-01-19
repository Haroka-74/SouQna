using FluentValidation;

namespace SouQna.Application.Features.Carts.Commands.UpdateCartItemQuantity
{
    public class UpdateCartItemQuantityCommandValidator : AbstractValidator<UpdateCartItemQuantityCommand>
    {
        public UpdateCartItemQuantityCommandValidator()
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