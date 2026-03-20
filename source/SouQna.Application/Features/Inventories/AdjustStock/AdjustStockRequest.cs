using MediatR;

namespace SouQna.Application.Features.Inventories.AdjustStock
{
    public record AdjustStockRequest(
        Guid ProductId,
        int Quantity
    ) : IRequest;
}