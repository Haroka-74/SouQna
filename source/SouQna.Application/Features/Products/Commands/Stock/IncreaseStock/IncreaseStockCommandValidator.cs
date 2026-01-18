using FluentValidation;

namespace SouQna.Application.Features.Products.Commands.Stock.IncreaseStock
{
    public class IncreaseStockRequestValidator : AbstractValidator<IncreaseStockCommand>
    {
        public IncreaseStockRequestValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("Product Id is required");

            RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity to increase must be positive");
        }
    }
}