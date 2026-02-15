using SouQna.Infrastructure.Enums;

namespace SouQna.Business.Contracts.Requests
{
    public record GetOrdersRequest(
        int PageNumber = 1,
        int PageSize = 10,
        OrderStatus? Status = null
    );
}