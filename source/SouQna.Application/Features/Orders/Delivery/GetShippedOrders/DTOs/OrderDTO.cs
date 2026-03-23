namespace SouQna.Application.Features.Orders.Delivery.GetShippedOrders.DTOs
{
    public record OrderDTO(
        Guid Id,
        string OrderNumber,
        decimal Total,
        DateTime CreatedAt
    );
}