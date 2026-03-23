using MediatR;

namespace SouQna.Application.Features.Inventories.Admin.AdjustStock
{
    public record AdjustStockRequest(
        Guid ProductId,
        int Quantity
    ) : IRequest;
}