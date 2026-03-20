namespace SouQna.Application.DTOs.Products
{
    public record AdminProductDTO(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Image,
        DateTime CreatedAt,
        int Quantity
    );
}