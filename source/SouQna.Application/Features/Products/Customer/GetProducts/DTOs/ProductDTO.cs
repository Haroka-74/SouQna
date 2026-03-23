namespace SouQna.Application.Features.Products.Customer.GetProducts.DTOs
{
    public record ProductDTO(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Image
    );
}