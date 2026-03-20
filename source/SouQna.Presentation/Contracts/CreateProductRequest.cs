namespace SouQna.Presentation.Contracts
{
    public record CreateProductRequest(
        string Name,
        string Description,
        IFormFile Image,
        decimal Price,
        int Quantity
    );
}