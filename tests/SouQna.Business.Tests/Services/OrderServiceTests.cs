using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using SouQna.Business.Services;
using SouQna.Business.Interfaces;
using SouQna.Business.Exceptions;
using SouQna.Infrastructure.Enums;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Business.Contracts.Requests.Orders;

namespace SouQna.Business.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly IOrderService _orderService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidationService> _validationServiceMock;

        public OrderServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validationServiceMock = new Mock<IValidationService>();
            _orderService = new OrderService(
                _unitOfWorkMock.Object,
                _validationServiceMock.Object
            );
        }

        [Fact]
        public async Task GetUserOrdersAsync_ShouldReturnPagedResult_WhenOrdersExist()
        {
            var userId = Guid.NewGuid();
            var orders = new List<Order>
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    OrderNumber = "ORD-2026-0001",
                    UserId = userId,
                    Total = 2450.75m,
                    Status = OrderStatus.Confirmed,
                    ShippingFullName = "Mohamed Mahmoud",
                    ShippingAddressLine = "15 Tahrir Street, Apt 3",
                    ShippingCity = "Cairo",
                    ShippingPhoneNumber = "01012345678",
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    OrderNumber = "ORD-2026-0002",
                    UserId = userId,
                    Total = 999.99m,
                    Status = OrderStatus.Shipped,
                    ShippingFullName = "Ahmed Ali",
                    ShippingAddressLine = "22 El Nasr Road",
                    ShippingCity = "Alexandria",
                    ShippingPhoneNumber = "01198765432",
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                }
            };

            _unitOfWorkMock.Setup(
                u => u.Orders.GetPagedAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Order, bool>>?>(),
                    It.IsAny<Func<IQueryable<Order>, IOrderedQueryable<Order>>?>(),
                    It.IsAny<string>()
                )
            ).ReturnsAsync((orders, 2));

            var request = new GetOrdersRequest(1, 10, null);

            var result = await _orderService.GetUserOrdersAsync(userId, request);

            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task GetUserOrdersAsync_ShouldReturnFilteredOrders_WhenStatusIsProvided()
        {
            var userId = Guid.NewGuid();
            var orders = new List<Order>
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    OrderNumber = "ORD-2026-0001",
                    UserId = userId,
                    Total = 2450.75m,
                    Status = OrderStatus.Pending,
                    ShippingFullName = "Mohamed Mahmoud",
                    ShippingAddressLine = "15 Tahrir Street, Apt 3",
                    ShippingCity = "Cairo",
                    ShippingPhoneNumber = "01012345678",
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    OrderNumber = "ORD-2026-0002",
                    UserId = userId,
                    Total = 999.99m,
                    Status = OrderStatus.Pending,
                    ShippingFullName = "Ahmed Ali",
                    ShippingAddressLine = "22 El Nasr Road",
                    ShippingCity = "Alexandria",
                    ShippingPhoneNumber = "01198765432",
                    CreatedAt = DateTime.UtcNow.AddDays(-5)
                }
            };

            _unitOfWorkMock.Setup(
                u => u.Orders.GetPagedAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Expression<Func<Order, bool>>?>(),
                    It.IsAny<Func<IQueryable<Order>, IOrderedQueryable<Order>>?>(),
                    It.IsAny<string>()
                )
            ).ReturnsAsync((orders, 2));

            var request = new GetOrdersRequest(1, 10, OrderStatus.Pending);

            var result = await _orderService.GetUserOrdersAsync(userId, request);

            result.Should().NotBeNull();
            result.Items.Should().HaveCount(2);
            result.Items.First().Status.Should().Be(OrderStatus.Pending);
            result.Items.Last().Status.Should().Be(OrderStatus.Pending);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task GetOrderAsync_ShouldReturnOrderDetail_WhenOrderExists()
        {
            var userId = Guid.NewGuid();
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                OrderNumber = "ORD-2026-0001",
                UserId = userId,
                Total = 2450.75m,
                Status = OrderStatus.Pending,
                ShippingFullName = "Mohamed Mahmoud",
                ShippingAddressLine = "15 Tahrir Street, Apt 3",
                ShippingCity = "Cairo",
                ShippingPhoneNumber = "01012345678",
                CreatedAt = DateTime.UtcNow,
            };
            order.OrderItems.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                ProductId = Guid.NewGuid(),
                ProductName = "HP Pavilion 14",
                ProductDescription = "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                ProductImage = "4AAQSkZJRgABAQEASABIAAD2w",
                Price = 1899.99m,
                Quantity = 1
            });

            _unitOfWorkMock.Setup(
                u => u.Orders.FindAsync(
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<Expression<Func<Order, object>>[]>()
                )
            ).ReturnsAsync(order);

            var result = await _orderService.GetOrderAsync(userId, orderId);

            result.Should().NotBeNull();
            result.Id.Should().Be(orderId);
            result.OrderNumber.Should().Be(order.OrderNumber);
            result.Status.Should().Be(order.Status);
            result.Total.Should().Be(order.Total);
            result.ShippingInfo.FullName.Should().Be(order.ShippingFullName);
            result.ShippingInfo.AddressLine.Should().Be(order.ShippingAddressLine);
            result.ShippingInfo.City.Should().Be(order.ShippingCity);
            result.ShippingInfo.PhoneNumber.Should().Be(order.ShippingPhoneNumber);
            result.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetOrderAsync_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
        {
            var userId = Guid.NewGuid();
            var orderId = Guid.NewGuid();

            _unitOfWorkMock.Setup(
                u => u.Orders.FindAsync(
                    It.IsAny<Expression<Func<Order, bool>>>(),
                    It.IsAny<Expression<Func<Order, object>>>()
                )
            ).ReturnsAsync((Order) null!);

            var action = async () => await _orderService.GetOrderAsync(userId, orderId);

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldCreateOrder_WhenCartHasItems()
        {
            var userId = Guid.NewGuid();
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "HP Pavilion 14",
                Description = "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                Image = "4AAQSkZJRgABAQEASABIAAD2w",
                Price = 1899.99m,
                CreatedAt = DateTime.UtcNow
            };
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            cart.CartItems.Add(
                new ()
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = 2,
                    PriceAtAddition = product.Price,
                    Product = product
                }
            );

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<string>()
                )
            ).ReturnsAsync(cart);

            _unitOfWorkMock.Setup(
                u => u.Orders
            ).Returns(new Mock<IRepository<Order>>().Object);

            _unitOfWorkMock.Setup(
                u => u.OrderItems
            ).Returns(new Mock<IRepository<OrderItem>>().Object);

            var request = new CreateOrderRequest(
                "Mohamed Mahmoud",
                "15 Tahrir Street, Apt 3",
                "Cairo",
                "01012345678"
            );

            var result = await _orderService.CreateOrderAsync(userId, request);

            result.Should().NotBeNull();
            result.OrderNumber.Should().StartWith("ORD-");
            result.Status.Should().Be(OrderStatus.Pending);
            result.Total.Should().Be(1899.99m * 2 * 1.10m);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(
                u => u.Orders.AddAsync(It.IsAny<Order>()),
                Times.Once
            );
            _unitOfWorkMock.Verify(
                u => u.OrderItems.AddAsync(It.IsAny<OrderItem>()),
                Times.Once
            );
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldThrowNotFoundException_WhenCartDoesNotExist()
        {
            var userId = Guid.NewGuid();

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<string>()
                )
            ).ReturnsAsync((Cart) null!);

            var request = new CreateOrderRequest(
                "Mohamed Mahmoud",
                "15 Tahrir Street, Apt 3",
                "Cairo",
                "01012345678"
            );

            var action = async () => await _orderService.CreateOrderAsync(userId, request);

            await action.Should().ThrowAsync<NotFoundException>();

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(
                u => u.Orders.AddAsync(It.IsAny<Order>()),
                Times.Never
            );
            _unitOfWorkMock.Verify(
                u => u.OrderItems.AddAsync(It.IsAny<OrderItem>()),
                Times.Never
            );
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldThrowConflictException_WhenCartIsEmpty()
        {
            var userId = Guid.NewGuid();
            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<string>()
                )
            ).ReturnsAsync(cart);

            var request = new CreateOrderRequest(
                "Mohamed Mahmoud",
                "15 Tahrir Street, Apt 3",
                "Cairo",
                "01012345678"
            );

            var action = async () => await _orderService.CreateOrderAsync(userId, request);

            await action.Should().ThrowAsync<ConflictException>();

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(
                u => u.Orders.AddAsync(It.IsAny<Order>()),
                Times.Never
            );
            _unitOfWorkMock.Verify(
                u => u.OrderItems.AddAsync(It.IsAny<OrderItem>()),
                Times.Never
            );
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}