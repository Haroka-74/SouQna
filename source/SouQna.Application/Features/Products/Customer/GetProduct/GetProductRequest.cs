using MediatR;

namespace SouQna.Application.Features.Products.Customer.GetProduct
{
    public record GetProductRequest(
        Guid Id
    ) : IRequest<DTOs.ProductDTO>;
}