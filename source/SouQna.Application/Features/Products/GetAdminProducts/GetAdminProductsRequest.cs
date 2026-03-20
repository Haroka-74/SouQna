using MediatR;
using SouQna.Application.Common;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.GetAdminProducts
{
    public record GetAdminProductsRequest(
        int PageNumber = 1,
        int PageSize = 10,
        string? SearchTerm = null,
        int? MaxStockThreshold = null
    ) : IRequest<Page<AdminProductDTO>>;
}