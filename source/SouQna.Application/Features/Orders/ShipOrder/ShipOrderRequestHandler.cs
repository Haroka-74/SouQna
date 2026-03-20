using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Orders.ShipOrder
{
    public class ShipOrderRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<ShipOrderRequest>
    {
        public async Task Handle(
            ShipOrderRequest request,
            CancellationToken cancellationToken
        )
        {
            var order = await unitOfWork.Orders.FindAsync(
                o => o.Id == request.Id,
                o => o.OrderItems
            ) ?? throw new NotFoundException($"Order with (id: {request.Id}) was not found");

            order.Ship();
            await unitOfWork.SaveChangesAsync();
        }
    }
}