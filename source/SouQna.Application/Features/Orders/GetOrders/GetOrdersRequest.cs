using MediatR;
using SouQna.Domain.Enums;
using SouQna.Application.Common;
using SouQna.Application.DTOs.Orders;

namespace SouQna.Application.Features.Orders.GetOrders
{
    public record GetOrdersRequest(
        int PageNumber = 1,
        int PageSize = 10,
        OrderStatus OrderStatus = OrderStatus.Pending
    ) : IRequest<Page<OrderDTO>>;
}
