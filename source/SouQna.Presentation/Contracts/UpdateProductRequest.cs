namespace SouQna.Presentation.Contracts
{
    public record UpdateProductRequest(
        string Name,
        string Description,
        IFormFile? Image,
        decimal Price
    );
}