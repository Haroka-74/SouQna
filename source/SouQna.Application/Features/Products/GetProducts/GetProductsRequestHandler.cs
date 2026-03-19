using MediatR;
using AutoMapper;
using SouQna.Domain.Entities;
using System.Linq.Expressions;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.GetProducts
{
    public class GetProductsRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetProductsRequest, Page<ProductDTO>>
    {
        public async Task<Page<ProductDTO>> Handle(
            GetProductsRequest request,
            CancellationToken cancellationToken
        )
        {
            Expression<Func<Product, bool>> filter = p =>
                (string.IsNullOrEmpty(request.SearchTerm) || p.Name.Contains(request.SearchTerm.Trim())) &&
                (!request.MinPrice.HasValue || p.Price >= request.MinPrice) &&
                (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice);

            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = q =>
                request.SortBy?.ToLower() switch
                {
                    "price" => request.IsDescending ? q.OrderByDescending(p => p.Price) : q.OrderBy(p => p.Price),
                    _ => q.OrderByDescending(p => p.CreatedAt)
                };

            var (items, totalCount) = await unitOfWork.Products.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                orderBy
            );

            var products = mapper.Map<List<ProductDTO>>(items);

            return new Page<ProductDTO>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                products.AsReadOnly()
            );
        }
    }
}