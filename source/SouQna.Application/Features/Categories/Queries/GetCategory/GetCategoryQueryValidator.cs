using FluentValidation;

namespace SouQna.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
    {
        public GetCategoryQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Category ID is required");
        }
    }
}