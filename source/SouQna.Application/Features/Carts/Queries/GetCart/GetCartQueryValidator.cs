using FluentValidation;

namespace SouQna.Application.Features.Carts.Queries.GetCart
{
    public class GetCartQueryValidator : AbstractValidator<GetCartQuery>
    {
        public GetCartQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required");
        }
    }
}