namespace SouQna.Application.Features.Orders.Shared
{
    public record OrderItemDTO(
        string ItemName,
        string ItemImage,
        decimal ItemPrice,
        int ItemQuantity,
        decimal Subtotal
    );
}