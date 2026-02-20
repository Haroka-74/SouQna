using SouQna.Infrastructure.Enums;

namespace SouQna.Business.Contracts.Responses.Orders
{
    public record CreateOrderResponse(
        Guid Id,
        string OrderNumber,
        OrderStatus Status,
        decimal Total
    );
}