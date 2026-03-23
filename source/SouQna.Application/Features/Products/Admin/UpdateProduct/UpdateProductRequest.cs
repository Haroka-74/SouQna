using MediatR;

namespace SouQna.Application.Features.Products.Admin.UpdateProduct
{
    public record UpdateProductRequest(
        Guid Id,
        string Name,
        string Description,
        Stream? ImageStream,
        decimal Price
    ) : IRequest<DTOs.ProductDTO>;
}