using FluentValidation;

namespace SouQna.Application.Features.Categories.Commands.AddCategory
{
    public class AddCategoryCommandValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(200).WithMessage("Category name must not exceed 200 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Category description is required")
                .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

            RuleFor(x => x.ParentId)
                .NotEqual(Guid.Empty).WithMessage("Parent ID must not be an empty GUID")
                    .When(x => x.ParentId.HasValue);
        }
    }
}