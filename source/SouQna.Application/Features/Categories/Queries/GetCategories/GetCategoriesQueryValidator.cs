using FluentValidation;

namespace SouQna.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryValidator : AbstractValidator<GetCategoriesQuery>
    {
        private const int MaxPageSize = 10;
        public GetCategoriesQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, MaxPageSize).WithMessage("Page size must be between 1 and 10");
        }
    }
}