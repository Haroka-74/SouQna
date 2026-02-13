using SouQna.Business.Interfaces;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Services
{
    public class ProductService(
        IUnitOfWork unitOfWork,
        IValidationService validationService
    ) : IProductService
    {
        public async Task<AddProductResponse> AddProductAsync(AddProductRequest request)
        {
            await validationService.ValidateAsync(request);

            using var memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name.Trim(),
                Description = request.Description.Trim(),
                Image = Convert.ToBase64String(memoryStream.ToArray()),
                Price = request.Price,
                CreatedAt = DateTime.UtcNow
            };

            await unitOfWork.Products.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return new AddProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Image
            );
        }
    }
}