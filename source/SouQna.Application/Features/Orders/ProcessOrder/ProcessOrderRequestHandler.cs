using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Orders.ProcessOrder
{
    public class ProcessOrderRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<ProcessOrderRequest>
    {
        public async Task Handle(
            ProcessOrderRequest request,
            CancellationToken cancellationToken
        )
        {
            var order = await unitOfWork.Orders.FindAsync(
                o => o.Id == request.Id,
                o => o.OrderItems
            ) ?? throw new NotFoundException($"Order with (id: {request.Id}) was not found");

            order.Process();
            await unitOfWork.SaveChangesAsync();
        }
    }
}