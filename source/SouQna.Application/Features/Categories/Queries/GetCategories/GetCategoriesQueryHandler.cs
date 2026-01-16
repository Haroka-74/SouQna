using MediatR;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;
using SouQna.Application.DTOs.Categories;

namespace SouQna.Application.Features.Categories.Queries.GetCategories
{
    public class GetCategoriesQueryHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCategoriesQuery, PagedResult<CategoryDTO>>
    {
        public async Task<PagedResult<CategoryDTO>> Handle(
            GetCategoriesQuery query,
            CancellationToken cancellationToken
        )
        {
            var (items, totalCount) = await unitOfWork.Categories.GetPagedAsync(
                query.PageNumber,
                query.PageSize,
                query.OrderBy,
                query.IsDescending,
                null,
                c => c.Subcategories
            );

            var categories = items.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Subcategories = [.. c.Subcategories.Select(c => c.Name)]
            }).ToList();

            return new PagedResult<CategoryDTO>(
                query.PageNumber,
                query.PageSize,
                totalCount,
                categories.AsReadOnly()
            );
        }
    }
}