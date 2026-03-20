using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Orders.DeliverOrder
{
    public class DeliverOrderRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<DeliverOrderRequest>
    {
        public async Task Handle(
            DeliverOrderRequest request,
            CancellationToken cancellationToken
        )
        {
            var order = await unitOfWork.Orders.FindAsync(
                o => o.Id == request.Id,
                o => o.OrderItems
            ) ?? throw new NotFoundException($"Order with (id: {request.Id}) was not found");

            order.Deliver();
            await unitOfWork.SaveChangesAsync();
        }
    }
}