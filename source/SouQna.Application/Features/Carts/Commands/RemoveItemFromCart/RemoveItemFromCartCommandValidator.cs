using FluentValidation;

namespace SouQna.Application.Features.Carts.Commands.RemoveItemFromCart
{
    public class RemoveItemFromCartCommandValidator : AbstractValidator<RemoveItemFromCartCommand>
    {
        public RemoveItemFromCartCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required");
        }
    }
}