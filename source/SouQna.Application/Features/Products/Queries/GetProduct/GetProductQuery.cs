using MediatR;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.Queries.GetProduct
{
    public record GetProductQuery(
        Guid Id
    ) : IRequest<ProductDTO>;
}