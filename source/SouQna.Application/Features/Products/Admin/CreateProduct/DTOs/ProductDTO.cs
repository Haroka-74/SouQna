namespace SouQna.Application.Features.Products.Admin.CreateProduct.DTOs
{
    public record ProductDTO(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Image,
        int Quantity,
        DateTime CreatedAt
    );
}