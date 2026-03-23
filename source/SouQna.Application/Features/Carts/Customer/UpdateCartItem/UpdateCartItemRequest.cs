using MediatR;

namespace SouQna.Application.Features.Carts.Customer.UpdateCartItem
{
    public record UpdateCartItemRequest(
        Guid UserId,
        Guid ProductId,
        int Quantity
    ) : IRequest<DTOs.CartDTO>;
}