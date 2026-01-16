using MediatR;

namespace SouQna.Application.Features.Products.Commands.AddProduct
{
    public record AddProductCommand(
        string Name,
        string Description,
        Stream ImageStream,
        string ImageFileName,
        decimal Price,
        int Quantity,
        bool IsActive,
        Guid CategoryId
    ) : IRequest<AddProductResponse>;
}