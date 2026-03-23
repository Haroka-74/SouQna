using MediatR;

namespace SouQna.Application.Features.Products.Admin.DeleteProduct
{
    public record DeleteProductRequest(
        Guid Id
    ) : IRequest;
}