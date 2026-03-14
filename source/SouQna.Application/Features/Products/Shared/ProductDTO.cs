namespace SouQna.Application.Features.Products.Shared
{
    public record ProductDTO(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Image
    );
}