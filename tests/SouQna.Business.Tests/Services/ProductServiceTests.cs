using Moq;
using FluentAssertions;
using System.Linq.Expressions;
using SouQna.Business.Services;
using SouQna.Business.Interfaces;
using SouQna.Business.Exceptions;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Business.Contracts.Requests.Products;
using Microsoft.AspNetCore.Http;

namespace SouQna.Business.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly IProductService _productService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IRepository<Product>> _productRepositoryMock;
        private readonly Mock<IValidationService> _validationServiceMock;

        public ProductServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productRepositoryMock = new Mock<IRepository<Product>>();
            _validationServiceMock = new Mock<IValidationService>();
            _unitOfWorkMock.Setup(u => u.Products).Returns(_productRepositoryMock.Object);
            _productService = new ProductService(
                _unitOfWorkMock.Object,
                _validationServiceMock.Object
            );
        }

        [Fact]
        public async Task GetProductById_WithExistingId_ReturnsProduct()
        {
            var id = Guid.NewGuid();

            _productRepositoryMock.Setup(
                r => r.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync(new Product
            {
                Id = id,
                Name = "HP Pavilion 14",
                Description = "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                Image = "4AAQSkZJRgABAQEASABIAAD2w",
                Price = 1899.99m,
                CreatedAt = DateTime.UtcNow
            });

            var result = await _productService.GetProductAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Name.Should().Be("HP Pavilion 14");
            result.Description.Should().Be("14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.");
            result.Image.Should().Be("4AAQSkZJRgABAQEASABIAAD2w");
            result.Price.Should().Be(1899.99m);
        }

        [Fact]
        public async Task GetProductById_WithNonExistentId_ThrowsNotFoundException()
        {
            var id = Guid.NewGuid();

            _productRepositoryMock.Setup(
                r => r.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync((Product) null!);

            var action = async () => await _productService.GetProductAsync(id);

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AddProduct_WithValidRequest_ReturnsCreatedProduct()
        {
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("Fake Image"));

            var request = new AddProductRequest(
                "HP Pavilion 14",
                "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                new FormFile(
                    stream,
                    0,
                    stream.Length,
                    "Image",
                    "test.png"
                )
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/png"
                },
                1899.99m
            );

            var result = await _productService.AddProductAsync(request);

            result.Should().NotBeNull();
            result.Name.Should().Be("HP Pavilion 14");
            result.Description.Should().Be("14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.");
            result.Price.Should().Be(1899.99m);

            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateProduct_WithValidRequest_ReturnsUpdatedProduct()
        {
            var id = Guid.NewGuid();

            var request = new UpdateProductRequest(
                "Dell UltraSharp U2723QE",
                "27-inch 4K USB-C Hub Monitor with IPS Black technology, 100% sRGB, and 90W Power Delivery.",
                579.99m
            );

            var existingProduct = new Product
            {
                Id = id,
                Name = "HP Pavilion 14",
                Description = "14-inch laptop with Intel Core i5, 8GB RAM, and 512GB SSD.",
                Image = "4AAQSkZJRgABAQEASABIAAD2w",
                Price = 1899.99m,
                CreatedAt = DateTime.UtcNow
            };

            _productRepositoryMock.Setup(
                r => r.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync(existingProduct);

            await _productService.UpdateProductAsync(id, request);

            existingProduct.Name.Should().Be(request.Name);
            existingProduct.Description.Should().Be(request.Description);
            existingProduct.Price.Should().Be(request.Price);
        }

        [Fact]
        public async Task UpdateProduct_WithNonExistentId_ThrowsNotFoundException()
        {
            var id = Guid.NewGuid();

            _productRepositoryMock.Setup(
                r => r.FindAsync(It.IsAny<Expression<Func<Product, bool>>>())
            ).ReturnsAsync((Product) null!);

            await _productService.UpdateProductAsync(id, new UpdateProductRequest(
                "Dell UltraSharp U2723QE",
                "27-inch 4K USB-C Hub Monitor with IPS Black technology, 100% sRGB, and 90W Power Delivery.",
                579.99m
            ));

            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}