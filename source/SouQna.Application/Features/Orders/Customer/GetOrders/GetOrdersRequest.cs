using MediatR;
using SouQna.Domain.Enums;
using SouQna.Application.Common;

namespace SouQna.Application.Features.Orders.Customer.GetOrders
{
    public record GetOrdersRequest(
        Guid UserId,
        int PageNumber = 1,
        int PageSize = 10,
        OrderStatus? OrderStatus = null
    ) : IRequest<Page<DTOs.OrderDTO>>;
}