using SouQna.Domain.Enums;

namespace SouQna.Presentation.Contracts
{
    public record GetUserOrdersRequest(
        int PageNumber = 1,
        int PageSize = 10,
        OrderStatus? OrderStatus = null
    );
}