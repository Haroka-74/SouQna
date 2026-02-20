using SouQna.Infrastructure.Enums;

namespace SouQna.Business.Contracts.Responses.Orders
{
    public record OrderSummaryResponse(
        Guid Id,
        string OrderNumber,
        OrderStatus Status,
        int ItemCount,
        decimal Total,
        DateTime CreatedAt
    );
}