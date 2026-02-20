namespace SouQna.Business.Contracts.Requests.Carts
{
    public record AddToCartRequest(
        Guid ProductId,
        int Quantity
    );
}