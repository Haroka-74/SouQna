namespace SouQna.Application.Features.Orders.Warehouse.GetProcessingOrders.DTOs
{
    public record OrderDTO(
        Guid Id,
        string OrderNumber,
        decimal Total,
        DateTime CreatedAt
    );
}