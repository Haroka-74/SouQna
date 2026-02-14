namespace SouQna.Business.Contracts.Responses
{
    public record ProductResponse(
        Guid Id,
        string Name,
        string Description,
        decimal Price,
        string Image
    );
}