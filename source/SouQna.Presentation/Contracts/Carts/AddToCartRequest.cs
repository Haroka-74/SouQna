namespace SouQna.Presentation.Contracts.Carts
{
    public record AddToCartRequest(
        Guid ProductId,
        int Quantity
    );
}