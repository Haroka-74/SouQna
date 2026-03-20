using MediatR;

namespace SouQna.Application.Features.Orders.DeliverOrder
{
    public record DeliverOrderRequest(
        Guid Id
    ) : IRequest;
}