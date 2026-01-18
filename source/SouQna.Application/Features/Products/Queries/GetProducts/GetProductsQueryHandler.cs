using MediatR;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetProductsQuery, PagedResult<ProductDTO>>
    {
        public async Task<PagedResult<ProductDTO>> Handle(
            GetProductsQuery query,
            CancellationToken cancellationToken
        )
        {
            var hasFilter =
                query.CategoryId.HasValue ||
                !string.IsNullOrEmpty(query.SearchTerm) ||
                query.IsActive.HasValue;

            var (items, totalCount) = await unitOfWork.Products.GetPagedAsync(
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.IsDescending,
                hasFilter
                ? p =>
                    (!query.CategoryId.HasValue || p.CategoryId == query.CategoryId.Value) &&
                    (
                        string.IsNullOrEmpty(query.SearchTerm) ||
                        p.Name.Contains(query.SearchTerm) ||
                        p.Description.Contains(query.SearchTerm)
                    ) &&
                    (!query.IsActive.HasValue || p.IsActive == query.IsActive.Value)
                : null,
                p => p.Category
            );

            var products = items.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Image = p.Image,
                Price = p.Price,
                Quantity = p.Quantity,
                IsActive = p.IsActive,
                CategoryName = p.Category.Name
            }).ToList();

            return new PagedResult<ProductDTO>(
                query.PageNumber,
                query.PageSize,
                totalCount,
                products.AsReadOnly()
            );
        }
    }
}