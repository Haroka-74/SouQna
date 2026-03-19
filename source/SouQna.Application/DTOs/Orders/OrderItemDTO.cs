namespace SouQna.Application.DTOs.Orders
{
    public record OrderItemDTO(
        string ItemName,
        string ItemImage,
        decimal ItemPrice,
        int ItemQuantity,
        decimal Subtotal
    );
}