using SouQna.Infrastructure.Enums;

namespace SouQna.Business.Contracts.Responses.Orders
{
    public record OrderDetailResponse(
        Guid Id,
        string OrderNumber,
        OrderStatus Status,
        decimal Total,
        ShippingInfoResponse ShippingInfo,
        ICollection<OrderItemResponse> Items,
        DateTime CreatedAt,
        DateTime? ConfirmedAt,
        DateTime? ShippedAt,
        DateTime? DeliveredAt,
        DateTime? CancelledAt
    );
}