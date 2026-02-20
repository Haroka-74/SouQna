using FluentValidation;
using SouQna.Business.Contracts.Requests.Orders;

namespace SouQna.Business.Contracts.Validators.Orders
{
    public class GetOrdersRequestValidator : AbstractValidator<GetOrdersRequest>
    {
        public GetOrdersRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50).WithMessage("Page size must be between 1 and 50");

            RuleFor(x => x.Status)
                .IsInEnum().When(x => x.Status.HasValue)
                    .WithMessage("The specified order status is invalid");
        }
    }
}