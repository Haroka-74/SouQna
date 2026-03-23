namespace SouQna.Application.Features.Products.Admin.UpdateProduct.DTOs
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