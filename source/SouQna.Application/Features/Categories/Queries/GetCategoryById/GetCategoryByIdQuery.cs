using MediatR;

namespace SouQna.Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(
        Guid Id
    ) : IRequest<GetCategoryByIdResponse>;
}