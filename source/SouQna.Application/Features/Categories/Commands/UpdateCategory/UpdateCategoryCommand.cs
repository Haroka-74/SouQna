using MediatR;

namespace SouQna.Application.Features.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(
        Guid Id,
        string Name,
        string Description
    ) : IRequest<UpdateCategoryResponse>;
}