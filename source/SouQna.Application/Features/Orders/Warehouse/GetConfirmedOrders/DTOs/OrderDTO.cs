namespace SouQna.Application.Features.Orders.Warehouse.GetConfirmedOrders.DTOs
{
    public record OrderDTO(
        Guid Id,
        string OrderNumber,
        decimal Total,
        DateTime CreatedAt
    );
}