using MediatR;
using SouQna.Application.DTOs.Carts;

namespace SouQna.Application.Features.Carts.Queries.GetCart
{
    public record GetCartQuery(
        Guid UserId
    ) : IRequest<CartDTO>;
}