using MediatR;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.GetProduct
{
    public record GetProductRequest(
        Guid Id
    ) : IRequest<ProductDTO>;
}