using SouQna.Business.Common;
using System.Linq.Expressions;
using SouQna.Business.Interfaces;
using SouQna.Business.Exceptions;
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
        public async Task<PagedResult<ProductResponse>> GetPagedProductsAsync(GetProductsRequest request)
        {
            await validationService.ValidateAsync(request);

            Expression<Func<Product, bool>> filter = p =>
                (string.IsNullOrEmpty(request.SearchTerm) || p.Name.Contains(request.SearchTerm.Trim())) &&
                (!request.MinPrice.HasValue || p.Price >= request.MinPrice) &&
                (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice);

            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = query =>
            {
                return request.SortColumn?.ToLower() switch
                {
                    "price" =>
                        request.SortOrder == "desc"
                        ? query.OrderByDescending(p => p.Price)
                        : query.OrderBy(p => p.Price),
                    "name" =>
                        request.SortOrder == "desc"
                        ? query.OrderByDescending(p => p.Name)
                        : query.OrderBy(p => p.Name),
                    _ =>
                        query.OrderByDescending(p => p.CreatedAt)
                };
            };

            var (items, totalCount) = await unitOfWork.Products.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                orderBy
            );

            var products = items.Select(item => new ProductResponse(
                item.Id,
                item.Name,
                item.Description,
                item.Price,
                item.Image
            )).ToList();

            return new PagedResult<ProductResponse>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                products.AsReadOnly()
            );
        }

        public async Task<ProductResponse> GetProductAsync(Guid id)
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == id
            ) ?? throw new NotFoundException($"Product with (id: {id}) was not found");

            return new ProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Image
            );
        }

        public async Task<ProductResponse> AddProductAsync(AddProductRequest request)
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

            return new ProductResponse(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Image
            );
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == id
            );

            if(product is null)
                return;

            await unitOfWork.Products.DeleteAsync(product);
            await unitOfWork.SaveChangesAsync();
        }
    }
}