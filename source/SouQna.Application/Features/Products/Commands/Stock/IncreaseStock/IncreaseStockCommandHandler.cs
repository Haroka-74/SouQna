using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Products.Commands.Stock.IncreaseStock
{
    public class IncreaseStockHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<IncreaseStockCommand, IncreaseStockResponse>
    {
        public async Task<IncreaseStockResponse> Handle(
            IncreaseStockCommand command,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == command.Id
            ) ?? throw new NotFoundException($"Product with ID {command.Id} not found");

            product.IncreaseStock(command.Quantity);
            await unitOfWork.SaveChangesAsync();

            return new IncreaseStockResponse
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity
            };
        }
    }
}