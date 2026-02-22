using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using SouQna.Business.Services;
using SouQna.Business.Interfaces;
using SouQna.Business.Exceptions;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Business.Contracts.Requests.Products;

namespace SouQna.Business.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly IProductService _productService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IValidationService> _validationServiceMock;

        public ProductServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _validationServiceMock = new Mock<IValidationService>();
            _productService = new ProductService(
                _unitOfWorkMock.Object,
                _validationServiceMock.Object
            );
        }

        [Fact]
        public async Task GetProductsAsync_ShouldReturnPagedResult_WhenRequestIsValid()
        {
            var request = new GetProductsRequest(1, 10, "Phone");
            var products = new List<Product>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "HP Pavilion 14",
                    Description = "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                    Image = "4AAQSkZJRgABAQEASABIAAD2w",
                    Price = 1899.99m,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Dell Inspiron 15",
                    Description = "15.6-inch laptop with Intel Core i7, 16GB RAM, and 1TB SSD.",
                    Image = "4AAQSkZJRgABAQEASABIAAD3x",
                    Price = 2499.50m,
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Lenovo ThinkPad X13",
                    Description = "13.3-inch business laptop with AMD Ryzen 7, 16GB RAM, and 512GB SSD.",
                    Image = "4AAQSkZJRgABAQEASABIAAD4y",
                    Price = 2799.00m,
                    CreatedAt = DateTime.UtcNow
                }
            };

            _unitOfWorkMock.Setup(
                u => u.Products.GetPagedAsync(
                    request.PageNumber,
                    request.PageSize,
                    It.IsAny<Expression<Func<Product, bool>>>(),
                    It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>()
                )
            ).ReturnsAsync((products, 3));

            var result = await _productService.GetProductsAsync(request);

            result.Should().NotBeNull();
            result.Items.Should().HaveCount(3);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
        }

        [Fact]
        public async Task GetProductAsync_ShouldReturnProduct_WhenProductExists()
        {
            var id = Guid.NewGuid();
            var product = new Product
            {
                Id = id,
                Name = "HP Pavilion 14",
                Description = "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                Image = "4AAQSkZJRgABAQEASABIAAD2w",
                Price = 1899.99m,
                CreatedAt = DateTime.UtcNow
            };

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync(product);

            var result = await _productService.GetProductAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Name.Should().Be(product.Name);
            result.Description.Should().Be(product.Description);
            result.Price.Should().Be(product.Price);
            result.Image.Should().Be(product.Image);
        }

        [Fact]
        public async Task GetProductAsync_ShouldThrowNotFoundException_WhenProductDoesNotExist()
        {
            var id = Guid.NewGuid();

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync((Product) null!);

            var action = async () => await _productService.GetProductAsync(id);

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct_WhenProductExists()
        {
            var id = Guid.NewGuid();
            var existingProduct = new Product
            {
                Id = id,
                Name = "HP Pavilion 14",
                Description = "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                Image = "4AAQSkZJRgABAQEASABIAAD2w",
                Price = 1899.99m,
                CreatedAt = DateTime.UtcNow
            };

            var request = new UpdateProductRequest(
                "Dell Inspiron 15",
                "15.6-inch laptop with Intel Core i7, 16GB RAM, and 1TB SSD.",
                2499.99m
            );

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync(existingProduct);

            await _productService.UpdateProductAsync(id, request);

            existingProduct.Name.Should().Be(request.Name);
            existingProduct.Description.Should().Be(request.Description);
            existingProduct.Price.Should().Be(request.Price);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldReturn_WhenProductDoesNotExist()
        {
            var id = Guid.NewGuid();

            var request = new UpdateProductRequest(
                "Dell Inspiron 15",
                "15.6-inch laptop with Intel Core i7, 16GB RAM, and 1TB SSD.",
                2499.99m
            );

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync((Product) null!);

            await _productService.UpdateProductAsync(id, request);

            _validationServiceMock.Verify(v => v.ValidateAsync(request), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldDelete_WhenProductExists()
        {
            var id = Guid.NewGuid();
            var product = new Product
            {
                Id = id,
                Name = "HP Pavilion 14",
                Description = "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                Image = "4AAQSkZJRgABAQEASABIAAD2w",
                Price = 1899.99m,
                CreatedAt = DateTime.UtcNow
            };

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync(product);

            await _productService.DeleteProductAsync(id);

            _unitOfWorkMock.Verify(u => u.Products.DeleteAsync(product), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturn_WhenProductDoesNotExist()
        {
            var id = Guid.NewGuid();

            _unitOfWorkMock.Setup(
                u => u.Products.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync((Product) null!);

            await _productService.DeleteProductAsync(id);

            _unitOfWorkMock.Verify(
                u => u.Products.DeleteAsync(It.IsAny<Product>()),
                Times.Never
            );
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}