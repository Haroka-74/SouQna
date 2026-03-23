using MediatR;

namespace SouQna.Application.Features.Products.Admin.CreateProduct
{
    public record CreateProductRequest(
        string Name,
        string Description,
        Stream ImageStream,
        decimal Price,
        int Quantity
    ) : IRequest<DTOs.ProductDTO>;
}