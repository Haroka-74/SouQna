using FluentValidation;
using SouQna.Business.Contracts.Requests.Products;

namespace SouQna.Business.Contracts.Validators.Products
{
    public class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
    {
        private readonly string[] _allowedSortColumns = ["name", "price"];
        private readonly string[] _allowedSortOrders = ["asc", "desc"];

        public GetProductsRequestValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50).WithMessage("Page size must be between 1 and 50");

            RuleFor(x => x.SearchTerm)
                .MaximumLength(50).WithMessage("Search term must not exceed 50 characters");

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue)
                    .WithMessage("Minimum price cannot be negative");

            RuleFor(x => x.MaxPrice)
                .GreaterThanOrEqualTo(0).When(x => x.MaxPrice.HasValue)
                    .WithMessage("Maximum price cannot be negative")
                .GreaterThanOrEqualTo(x => x.MinPrice!.Value).When(x => x.MinPrice.HasValue && x.MaxPrice.HasValue)
                    .WithMessage("Maximum price must be greater than or equal to minimum price");

            RuleFor(x => x.SortColumn)
                .Must(x => string.IsNullOrEmpty(x) || _allowedSortColumns.Contains(x.ToLower()))
                    .WithMessage($"Sort column must be one of the following: {string.Join(", ", _allowedSortColumns)}");

            RuleFor(x => x.SortOrder)
                .Must(x => string.IsNullOrEmpty(x) || _allowedSortOrders.Contains(x.ToLower()))
                    .WithMessage("Sort order must be 'asc' or 'desc'");
        }
    }
}