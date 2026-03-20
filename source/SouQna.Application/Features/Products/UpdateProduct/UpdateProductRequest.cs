using MediatR;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.UpdateProduct
{
    public record UpdateProductRequest(
        Guid Id,
        string Name,
        string Description,
        Stream? ImageStream,
        decimal Price
    ) : IRequest<AdminProductDTO>;
}