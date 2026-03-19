using SouQna.Domain.Enums;

namespace SouQna.Application.DTOs.Orders
{
    public record OrderDetailDTO(
        string OrderNumber,
        ShippingInfoDTO ShippingInfo,
        decimal Total,
        OrderStatus OrderStatus,
        DateTime CreatedAt,
        DateTime? ConfirmedAt,
        DateTime? ShippedAt,
        DateTime? DeliveredAt,
        DateTime? CancelledAt,
        ICollection<OrderItemDTO> Items
    );
}