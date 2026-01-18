using MediatR;

namespace SouQna.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        string Description,
        decimal Price
    ) : IRequest<UpdateProductResponse>;
}