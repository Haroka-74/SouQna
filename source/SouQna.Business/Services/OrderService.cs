using SouQna.Business.Exceptions;
using SouQna.Business.Interfaces;
using SouQna.Infrastructure.Enums;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Services
{
    public class OrderService(
        IUnitOfWork unitOfWork,
        IValidationService validationService
    ) : IOrderService
    {
        public async Task<CreateOrderResponse> CreateOrderAsync(Guid userId, CreateOrderRequest request)
        {
            await validationService.ValidateAsync(request);

            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == userId,
                "CartItems.Product"
            ) ?? throw new NotFoundException("Cart not found for this user");

            if (cart.CartItems == null || cart.CartItems.Count == 0)
                throw new ConflictException("Cannot create order from an empty cart");

            var subtotal = cart.CartItems.Sum(ci => ci.PriceAtAddition * ci.Quantity);
            var total = subtotal * 1.10m;

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OrderNumber = GenerateOrderNumber(),
                UserId = userId,
                Status = OrderStatus.Pending,
                Total = total,
                ShippingFullName = request.FullName,
                ShippingAddressLine = request.AddressLine,
                ShippingCity = request.City,
                ShippingPhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.Orders.AddAsync(order);

            foreach(var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    ProductName = cartItem.Product.Name,
                    ProductDescription = cartItem.Product.Description,
                    ProductImage = cartItem.Product.Image,
                    Price = cartItem.PriceAtAddition,
                    Quantity = cartItem.Quantity
                };

                await unitOfWork.OrderItems.AddAsync(orderItem);
            };

            await unitOfWork.SaveChangesAsync();
            return new CreateOrderResponse(
                order.Id,
                order.OrderNumber,
                order.Status,
                order.Total
            );
        }

        private static string GenerateOrderNumber()
        {
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var random = Guid.NewGuid().ToString("N")[..6].ToUpper();
            return $"ORD-{date}-{random}";
        }
    }
}