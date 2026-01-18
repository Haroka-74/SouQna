using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Products.Commands.Stock.DecreaseStock
{
    public class DecreaseStockCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<DecreaseStockCommand, DecreaseStockResponse>
    {
        public async Task<DecreaseStockResponse> Handle(
            DecreaseStockCommand command,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == command.Id
            ) ?? throw new NotFoundException($"Product with ID {command.Id} not found");

            product.DecreaseStock(command.Quantity);
            await unitOfWork.SaveChangesAsync();

            return new DecreaseStockResponse
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity
            };
        }
    }
}