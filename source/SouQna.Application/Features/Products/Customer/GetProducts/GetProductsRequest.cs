using MediatR;
using SouQna.Application.Common;

namespace SouQna.Application.Features.Products.Customer.GetProducts
{
    public record GetProductsRequest(
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchTerm = null,
        decimal? MinPrice = null,
        decimal? MaxPrice = null,
        string? SortBy = null,
        bool IsDescending = false
    ) : IRequest<Page<DTOs.ProductDTO>>;
}