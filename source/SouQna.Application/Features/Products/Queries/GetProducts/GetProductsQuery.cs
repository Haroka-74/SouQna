using MediatR;
using SouQna.Application.Common;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery(
        int PageNumber = 1,
        int PageSize = 10,
        Guid? CategoryId = null,
        string? SearchTerm = null,
        bool? IsActive = null,
        string? OrderBy = null,
        bool IsDescending = false
    ) : IRequest<PagedResult<ProductDTO>>;
}