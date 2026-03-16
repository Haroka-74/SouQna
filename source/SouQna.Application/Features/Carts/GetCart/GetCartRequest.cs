using MediatR;
using SouQna.Application.Features.Carts.Shared;

namespace SouQna.Application.Features.Carts.GetCart
{
    public record GetCartRequest(
        Guid UserId
    ) : IRequest<CartDTO>;
}