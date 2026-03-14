using MediatR;
using SouQna.Application.Features.Products.Shared;

namespace SouQna.Application.Features.Products.GetProduct
{
    public record GetProductRequest(
        Guid Id
    ) : IRequest<ProductDTO>;
}