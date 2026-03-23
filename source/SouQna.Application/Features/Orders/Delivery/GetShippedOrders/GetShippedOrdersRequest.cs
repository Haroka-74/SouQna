using MediatR;
using SouQna.Application.Common;

namespace SouQna.Application.Features.Orders.Delivery.GetShippedOrders
{
    public record GetShippedOrdersRequest(
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<Page<DTOs.OrderDTO>>;
}