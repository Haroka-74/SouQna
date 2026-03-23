using MediatR;
using SouQna.Application.Common;

namespace SouQna.Application.Features.Products.Admin.GetProducts
{
    public record GetProductsRequest(
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchTerm = null,
        int? MaxStockThreshold = null
    ) : IRequest<Page<DTOs.ProductDTO>>;
}