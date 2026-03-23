using MediatR;

namespace SouQna.Application.Features.Orders.Delivery.DeliverOrder
{
    public record DeliverOrderRequest(
        Guid Id
    ) : IRequest;
}