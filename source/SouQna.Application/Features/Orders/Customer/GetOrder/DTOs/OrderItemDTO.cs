namespace SouQna.Application.Features.Orders.Customer.GetOrder.DTOs
{
    public record OrderItemDTO(
        Guid ProductId,
        string ItemName,
        string ItemImage,
        decimal ItemPrice,
        int ItemQuantity,
        decimal Subtotal,
        bool IsReviewed
    );
}