using SouQna.Infrastructure.Enums;

namespace SouQna.Business.Contracts.Requests.Orders
{
    public record GetOrdersRequest(
        int PageNumber = 1,
        int PageSize = 10,
        OrderStatus? Status = null
    );
}