using MediatR;
using AutoMapper;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;
using SouQna.Application.DTOs.Carts;

namespace SouQna.Application.Features.Carts.RemoveFromCart
{
    public class RemoveFromCartRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<RemoveFromCartRequest, CartDTO>
    {
        public async Task<CartDTO> Handle(
            RemoveFromCartRequest request,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == request.UserId,
                "CartItems.Product"
            ) ?? throw new NotFoundException($"Cart for user (id: {request.UserId}) was not found");

            cart.RemoveItem(request.ProductId);
            await unitOfWork.SaveChangesAsync();

            var items = mapper.Map<List<CartItemDTO>>(cart.CartItems);

            return new CartDTO(
                items.Sum(i => i.Quantity),
                items.Sum(i => i.Subtotal),
                items
            );
        }
    }
}