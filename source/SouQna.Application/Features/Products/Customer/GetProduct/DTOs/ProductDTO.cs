namespace SouQna.Application.Features.Products.Customer.GetProduct.DTOs
{
    public record ProductDTO(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Image
    );
}