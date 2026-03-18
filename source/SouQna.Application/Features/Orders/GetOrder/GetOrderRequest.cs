using MediatR;
using SouQna.Application.Features.Orders.Shared;

namespace SouQna.Application.Features.Orders.GetOrder
{
    public record GetOrderRequest(
        Guid UserId,
        Guid OrderId
    ) : IRequest<OrderDetailDTO>;
}