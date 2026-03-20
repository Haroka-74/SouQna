using MediatR;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.CreateProduct
{
    public record CreateProductRequest(
        string Name,
        string Description,
        Stream ImageStream,
        decimal Price,
        int Quantity
    ) : IRequest<AdminProductDTO>;
}