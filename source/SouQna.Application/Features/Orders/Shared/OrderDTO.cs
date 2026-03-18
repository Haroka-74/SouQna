using SouQna.Domain.Enums;

namespace SouQna.Application.Features.Orders.Shared
{
    public record OrderDTO(
        Guid Id,
        string OrderNumber,
        decimal Total,
        OrderStatus OrderStatus,
        DateTime CreatedAt
    );
}