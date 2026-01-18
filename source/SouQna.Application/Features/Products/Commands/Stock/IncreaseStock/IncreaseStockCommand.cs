using MediatR;

namespace SouQna.Application.Features.Products.Commands.Stock.IncreaseStock
{
    public record IncreaseStockCommand(
        Guid Id,
        int Quantity
    ) : IRequest<IncreaseStockResponse>;
}