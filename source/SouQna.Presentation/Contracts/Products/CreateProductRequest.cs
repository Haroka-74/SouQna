namespace SouQna.Presentation.Contracts.Products
{
    public record CreateProductRequest(
        string Name,
        string Description,
        IFormFile Image,
        decimal Price,
        int Quantity
    );
}