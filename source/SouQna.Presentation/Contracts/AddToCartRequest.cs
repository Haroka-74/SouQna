namespace SouQna.Presentation.Contracts
{
    public record AddToCartRequest(
        Guid ProductId,
        int Quantity
    );
}