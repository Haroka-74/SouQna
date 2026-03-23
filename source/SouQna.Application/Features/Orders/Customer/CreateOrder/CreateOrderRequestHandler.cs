using MediatR;
using SouQna.Domain.Entities;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Orders.Customer.CreateOrder
{
    public class CreateOrderRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateOrderRequest, DTOs.OrderDTO>
    {
        public async Task<DTOs.OrderDTO> Handle(
            CreateOrderRequest request,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == request.UserId,
                "CartItems.Product.Inventory"
            ) ?? throw new NotFoundException($"Cart for user (id: {request.UserId}) was not found");

            if (cart.CartItems == null || cart.CartItems.Count == 0)
                throw new ConflictException("Your cart is empty. Please add items before placing an order");

            var subtotal = cart.CartItems.Sum(ci => ci.PriceSnapshot * ci.Quantity);
            var total = subtotal * 1.10m;

            var order = Order.Create(
                request.UserId,
                request.ShippingFullName,
                request.ShippingPhoneNumber,
                request.ShippingCity,
                request.ShippingAddressLine,
                total
            );

            await unitOfWork.Orders.AddAsync(order);

            foreach(var ci in cart.CartItems)
            {
                ci.Product.Inventory.DecrementStock(ci.Quantity);

                var orderItem = OrderItem.Create(
                    order.Id,
                    ci.ProductId,
                    ci.Product.Name,
                    ci.Product.Image,
                    ci.PriceSnapshot,
                    ci.Quantity
                );

                await unitOfWork.OrderItems.AddAsync(orderItem);
            }

            await unitOfWork.SaveChangesAsync();

            return new DTOs.OrderDTO(
                order.Id,
                order.OrderNumber,
                order.Total,
                order.OrderStatus,
                order.CreatedAt
            );
        }
    }
}