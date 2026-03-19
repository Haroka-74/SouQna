using MediatR;
using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;
using SouQna.Application.DTOs.Carts;

namespace SouQna.Application.Features.Carts.AddToCart
{
    public class AddToCartRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<AddToCartRequest, CartDTO>
    {
        public async Task<CartDTO> Handle(
            AddToCartRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == request.ProductId
            ) ?? throw new NotFoundException($"Product with (id: {request.ProductId}) was not found");

            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == request.UserId,
                "CartItems.Product"
            );

            if(cart is null)
            {
                cart = Cart.Create(request.UserId);
                await unitOfWork.Carts.AddAsync(cart);
            }

            cart.AddItem(product, request.Quantity);
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