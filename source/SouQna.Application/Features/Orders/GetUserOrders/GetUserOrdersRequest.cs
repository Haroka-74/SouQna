using MediatR;
using SouQna.Domain.Enums;
using SouQna.Application.Common;
using SouQna.Application.DTOs.Orders;

namespace SouQna.Application.Features.Orders.GetUserOrders
{
    public record GetUserOrdersRequest(
        Guid UserId,
        int PageNumber = 1,
        int PageSize = 10,
        OrderStatus? OrderStatus = null
    ) : IRequest<Page<OrderDTO>>;
}