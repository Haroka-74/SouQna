using MediatR;

namespace SouQna.Application.Features.Products.DeleteProduct
{
    public record DeleteProductRequest(
        Guid Id
    ) : IRequest;
}