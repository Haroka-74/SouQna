using FluentValidation;

namespace SouQna.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
    {
        public GetCategoriesQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .NotEmpty().WithMessage("Page number is required")
                .GreaterThan(0).WithMessage("Page number must be greater than 0");

            RuleFor(x => x.PageSize)
                .NotEmpty().WithMessage("Page size is required")
                .GreaterThan(0).WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(50).WithMessage("Page size must be between 1 and 50");
        }
    }
}