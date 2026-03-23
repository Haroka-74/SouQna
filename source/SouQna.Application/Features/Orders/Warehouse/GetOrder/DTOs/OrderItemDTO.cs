namespace SouQna.Application.Features.Orders.Warehouse.GetOrder.DTOs
{
    public record OrderItemDTO(
        string ItemName,
        string ItemImage,
        decimal ItemPrice,
        int ItemQuantity,
        decimal Subtotal
    );
}