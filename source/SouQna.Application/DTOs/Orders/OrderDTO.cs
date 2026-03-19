using SouQna.Domain.Enums;

namespace SouQna.Application.DTOs.Orders
{
    public record OrderDTO(
        Guid Id,
        string OrderNumber,
        decimal Total,
        OrderStatus OrderStatus,
        DateTime CreatedAt
    );
}