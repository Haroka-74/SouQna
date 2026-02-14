namespace SouQna.Business.Contracts.Requests
{
    public record AddToCartRequest(
        Guid ProductId,
        int Quantity
    );
}