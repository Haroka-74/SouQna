using MediatR;
using AutoMapper;
using SouQna.Domain.Entities;
using System.Linq.Expressions;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.GetAdminProducts
{
    public class GetAdminProductsRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetAdminProductsRequest, Page<AdminProductDTO>>
    {
        public async Task<Page<AdminProductDTO>> Handle(
            GetAdminProductsRequest request,
            CancellationToken cancellationToken
        )
        {

            Expression<Func<Product, bool>> filter = p =>
                (string.IsNullOrEmpty(request.SearchTerm) || p.Name.Contains(request.SearchTerm.Trim())) &&
                (!request.MaxStockThreshold.HasValue || p.Inventory.Quantity <= request.MaxStockThreshold);

            var (items, totalCount) = await unitOfWork.Products.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                q => q.OrderByDescending(p => p.CreatedAt),
                p => p.Inventory
            );

            var products = mapper.Map<List<AdminProductDTO>>(items);

            return new Page<AdminProductDTO>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                products.AsReadOnly()
            );
        }
    }
}