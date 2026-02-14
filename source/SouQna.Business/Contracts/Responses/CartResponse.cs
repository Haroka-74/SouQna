namespace SouQna.Business.Contracts.Responses
{
    public record CartResponse(
        decimal TotalAmount,
        int TotalItems,
        ICollection<CartItemResponse> Items
    );
}