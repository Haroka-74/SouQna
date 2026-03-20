using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Inventories.AdjustStock
{
    public class AdjustStockRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<AdjustStockRequest>
    {
        public async Task Handle(
            AdjustStockRequest request,
            CancellationToken cancellationToken
        )
        {
            var inventory = await unitOfWork.Inventories.FindAsync(
                i => i.ProductId == request.ProductId,
                i => i.Product
            ) ?? throw new NotFoundException($"Inventory with (product_id: {request.ProductId}) was not found");

            inventory.AdjustStock(request.Quantity);
            await unitOfWork.SaveChangesAsync();
        }
    }
}