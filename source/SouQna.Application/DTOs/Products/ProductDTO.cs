namespace SouQna.Application.DTOs.Products
{
    public record ProductDTO(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Image
    );
}