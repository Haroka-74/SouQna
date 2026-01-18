using MediatR;

namespace SouQna.Application.Features.Products.Commands.Stock.DecreaseStock
{
    public record DecreaseStockCommand(
        Guid Id,
        int Quantity
    ) : IRequest<DecreaseStockResponse>;
}