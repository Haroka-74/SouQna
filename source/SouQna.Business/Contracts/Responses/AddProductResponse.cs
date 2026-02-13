namespace SouQna.Business.Contracts.Responses
{
    public record AddProductResponse(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Image
    );
}