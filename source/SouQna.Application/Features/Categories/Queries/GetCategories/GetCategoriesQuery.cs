using MediatR;
using SouQna.Application.Common;
using SouQna.Application.DTOs.Categories;

namespace SouQna.Application.Features.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery(
        int PageNumber,
        int PageSize,
        string? OrderBy = null,
        bool IsDescending = false
    ) : IRequest<PagedResult<CategoryDTO>>;
}