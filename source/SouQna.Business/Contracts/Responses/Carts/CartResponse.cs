namespace SouQna.Business.Contracts.Responses.Carts
{
    public record CartResponse(
        decimal TotalAmount,
        int TotalItems,
        ICollection<CartItemResponse> Items
    );
}