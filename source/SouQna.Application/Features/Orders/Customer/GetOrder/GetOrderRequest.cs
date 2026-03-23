using MediatR;

namespace SouQna.Application.Features.Orders.Customer.GetOrder
{
    public record GetOrderRequest(
        Guid UserId,
        Guid OrderId
    ) : IRequest<DTOs.OrderDTO>;
}