namespace SouQna.Application.Features.Orders.Customer.GetOrder.DTOs
{
    public record OrderItemDTO(
        string ItemName,
        string ItemImage,
        decimal ItemPrice,
        int ItemQuantity,
        decimal Subtotal
    );
}