using FluentValidation;

namespace SouQna.Application.Features.Products.Commands.Stock.DecreaseStock
{
    public class DecreaseStockRequestValidator : AbstractValidator<DecreaseStockCommand>
    {
        public DecreaseStockRequestValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Product Id is required");

            RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity to decrease must be positive");
        }
    }
}