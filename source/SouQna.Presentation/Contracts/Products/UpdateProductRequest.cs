namespace SouQna.Presentation.Contracts.Products
{
    public record UpdateProductRequest(
        string Name,
        string Description,
        IFormFile? Image,
        decimal Price
    );
}