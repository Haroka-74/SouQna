using SouQna.Domain.Enums;

namespace SouQna.Presentation.Contracts.Orders
{
    public record GetOrdersRequest(
        int PageNumber = 1,
        int PageSize = 10,
        OrderStatus? OrderStatus = null
    );
}