using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using SouQna.Business.Services;
using SouQna.Business.Interfaces;
using SouQna.Business.Exceptions;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Business.Contracts.Requests.Carts;

namespace SouQna.Business.Tests.Services
{
    public class CartServiceTests
    {
        private readonly ICartService _cartService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidationService> _validationServiceMock;

        public CartServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validationServiceMock = new Mock<IValidationService>();
            _cartService = new CartService(
                _unitOfWorkMock.Object,
                _validationServiceMock.Object
            );
        }

        [Fact]
        public async Task GetCartAsync_ShouldReturnEmptyCart_WhenCartDoesNotExist()
        {
            var userId = Guid.NewGuid();

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(It.IsAny<Expression<Func<Cart, bool>>>())
            ).ReturnsAsync((Cart) null!);

            var result = await _cartService.GetCartAsync(userId);

            result.Should().NotBeNull();
            result.TotalAmount.Should().Be(0);
            result.TotalItems.Should().Be(0);
            result.Items.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCartAsync_ShouldReturnCartWithItems_WhenCartExists()
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

            var result = await _cartService.GetCartAsync(userId);

            result.Should().NotBeNull();
            result.TotalAmount.Should().Be(2 * product.Price);
            result.TotalItems.Should().Be(2);
            result.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task AddToCartAsync_ShouldCreateCartAndAddItem_WhenCartDoesNotExist()
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

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync(product);

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<Expression<Func<Cart, object>>[]>()
                )
            ).ReturnsAsync((Cart) null!);

            _unitOfWorkMock.Setup(
                u => u.CartItems
            ).Returns(new Mock<IRepository<CartItem>>().Object);

            var request = new AddToCartRequest(product.Id, 1);

            await _cartService.AddToCartAsync(userId, request);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(
                u => u.Carts.AddAsync(It.IsAny<Cart>()),
                Times.Once
            );
            _unitOfWorkMock.Verify(
                u => u.CartItems.AddAsync(It.IsAny<CartItem>()),
                Times.Once
            );
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddToCartAsync_ShouldAddNewItem_WhenCartExistsAndItemIsNotInCart()
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

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync(product);

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<Expression<Func<Cart, object>>[]>()
                )
            ).ReturnsAsync(cart);

            _unitOfWorkMock.Setup(
                u => u.CartItems
            ).Returns(new Mock<IRepository<CartItem>>().Object);

            var request = new AddToCartRequest(product.Id, 1);

            await _cartService.AddToCartAsync(userId, request);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(
                u => u.Carts.AddAsync(It.IsAny<Cart>()),
                Times.Never
            );
            _unitOfWorkMock.Verify(
                u => u.CartItems.AddAsync(It.IsAny<CartItem>()),
                Times.Once
            );
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddToCartAsync_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            var userId = Guid.NewGuid();

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync((Product) null!);

            var request = new AddToCartRequest(Guid.NewGuid(), 1);

            var action = async () => await _cartService.AddToCartAsync(userId, request);

            await action.Should().ThrowAsync<NotFoundException>();

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task AddToCartAsync_ShouldIncrementQuantity_WhenItemAlreadyExistsInCart()
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
            var item = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = 2,
                PriceAtAddition = product.Price,
                Product = product
            };
            cart.CartItems.Add(item);

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync(product);

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<Expression<Func<Cart, object>>[]>()
                )
            ).ReturnsAsync(cart);

            var request = new AddToCartRequest(product.Id, 3);

            await _cartService.AddToCartAsync(userId, request);

            item.Quantity.Should().Be(5);
            item.PriceAtAddition.Should().Be(product.Price);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateCartItemAsync_ShouldUpdateQuantity_WhenCartAndItemExist()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
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
            var item = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = 2,
                PriceAtAddition = product.Price,
                Product = product
            };
            cart.CartItems.Add(item);

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<Expression<Func<Cart, object>>[]>()
                )
            ).ReturnsAsync(cart);

            var request = new UpdateCartItemRequest(5);

            await _cartService.UpdateCartItemAsync(userId, productId, request);

            item.Quantity.Should().Be(5);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateCartItemAsync_ShouldReturn_WhenCartDoesNotExist()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<Expression<Func<Cart, object>>[]>()
                )
            ).ReturnsAsync((Cart) null!);

            var request = new UpdateCartItemRequest(5);

            await _cartService.UpdateCartItemAsync(userId, productId, request);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task UpdateCartItemAsync_ShouldReturn_WhenItemDoesNotExistInCart()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
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
                    It.IsAny<Expression<Func<Cart, object>>[]>()
                )
            ).ReturnsAsync(cart);

            var request = new UpdateCartItemRequest(5);

            await _cartService.UpdateCartItemAsync(userId, productId, request);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RemoveFromCartAsync_ShouldDeleteItem_WhenCartAndItemExist()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var product = new Product
            {
                Id = productId,
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
            var item = new CartItem
            {
                Id = Guid.NewGuid(),
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = 2,
                PriceAtAddition = product.Price,
                Product = product
            };
            cart.CartItems.Add(item);

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<Expression<Func<Cart, object>>[]>()
                )
            ).ReturnsAsync(cart);

            _unitOfWorkMock.Setup(
                u => u.CartItems
            ).Returns(new Mock<IRepository<CartItem>>().Object);

            await _cartService.RemoveFromCartAsync(userId, productId);

            _unitOfWorkMock.Verify(v => v.CartItems.DeleteAsync(item), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RemoveFromCartAsync_ShouldReturn_WhenCartDoesNotExist()
        {
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            _unitOfWorkMock.Setup(
                u => u.Carts.FindAsync(
                    It.IsAny<Expression<Func<Cart, bool>>>(),
                    It.IsAny<Expression<Func<Cart, object>>[]>()
                )
            ).ReturnsAsync((Cart) null!);

            await _cartService.RemoveFromCartAsync(userId, productId);

            _unitOfWorkMock.Verify(v => v.CartItems.DeleteAsync(It.IsAny<CartItem>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task RemoveFromCartAsync_ShouldReturn_WhenItemDoesNotExistInCart()
        {

            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
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
                    It.IsAny<Expression<Func<Cart, object>>[]>()
                )
            ).ReturnsAsync(cart);

            await _cartService.RemoveFromCartAsync(userId, productId);

            _unitOfWorkMock.Verify(v => v.CartItems.DeleteAsync(It.IsAny<CartItem>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}