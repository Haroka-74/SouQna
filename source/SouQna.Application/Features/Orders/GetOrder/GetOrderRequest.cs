using MediatR;
using SouQna.Application.DTOs.Orders;

namespace SouQna.Application.Features.Orders.GetOrder
{
    public record GetOrderRequest(
        Guid UserId,
        Guid OrderId
    ) : IRequest<OrderDetailDTO>;
}