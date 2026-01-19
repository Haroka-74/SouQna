using MediatR;
using SouQna.Application.DTOs.Carts;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Carts.Queries.GetCart
{
    public class GetCartQueryHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCartQuery, CartDTO>
    {
        public async Task<CartDTO> Handle(
            GetCartQuery query,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == query.UserId,
                c => c.CartItems
            );

            if(cart is null)
            {
                return new CartDTO
                {
                    TotalAmount = 0,
                    TotalItems = 0,
                    Items = []
                };
            }

            var productIds = cart.CartItems.Select(ci => ci.ProductId).ToList();

            var products = await unitOfWork.Products.FindAllAsync(
                p => productIds.Contains(p.Id)
            );

            var productDict = products.ToDictionary(p => p.Id);

            var items = cart.CartItems.Select(item =>
            {
                var product = productDict.GetValueOrDefault(item.ProductId);

                return new CartItemDTO
                {
                    ProductName = product?.Name ?? "Product not found",
                    ProductImage = product?.Image ?? string.Empty,
                    Price = item.PriceAtAddition,
                    Quantity = item.Quantity,
                    Subtotal = item.Subtotal
                };
            }).ToList();

            return new CartDTO
            {
                TotalAmount = cart.TotalAmount,
                TotalItems = cart.TotalItems,
                Items = items
            };
        }
    }
}