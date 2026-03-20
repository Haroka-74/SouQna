using MediatR;
using SouQna.Application.DTOs.Orders;

namespace SouQna.Application.Features.Orders.GetOrder
{
    public record GetOrderRequest(
        Guid OrderId,
        Guid? UserId = null
    ) : IRequest<OrderDetailDTO>;
}