namespace SouQna.Application.Features.Orders.Warehouse.GetOrder.DTOs
{
    public record OrderDTO(
        string OrderNumber,
        ShippingInfoDTO ShippingInfo,
        decimal Total,
        ICollection<OrderItemDTO> Items
    );
}