using FluentValidation;

namespace SouQna.Application.Features.Products.Commands.AddProduct
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .MaximumLength(200).WithMessage("Product name must not exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Product description is required")
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters");

            RuleFor(x => x.ImageStream)
                .NotNull().WithMessage("Product image is required");

            RuleFor(x => x.ImageFileName)
                .NotEmpty().WithMessage("Image file name is required");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category ID is required")
                .NotEqual(Guid.Empty).WithMessage("Category ID must not be an empty GUID");
        }
    }
}