using MediatR;
using SouQna.Application.Common;
using SouQna.Application.Features.Products.Shared;

namespace SouQna.Application.Features.Products.GetProducts
{
    public record GetProductsRequest(
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchTerm = null,
        decimal? MinPrice = null,
        decimal? MaxPrice = null,
        string? SortBy = null,
        bool IsDescending = false
    ) : IRequest<Page<ProductDTO>>;
}