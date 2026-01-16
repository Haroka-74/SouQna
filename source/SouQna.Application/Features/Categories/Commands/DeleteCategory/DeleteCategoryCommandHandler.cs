using MediatR;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteCategoryCommand>
    {
        public async Task Handle(
            DeleteCategoryCommand command,
            CancellationToken cancellationToken
        )
        {
            var category = await unitOfWork.Categories.FindAsync(
                c => c.Id == command.Id,
                c => c.Subcategories
            );

            if(category is null)
                return;

            if(category.HasSubcategories())
                throw new InvalidOperationException("Cannot delete category that has subcategories");

            await unitOfWork.Categories.DeleteAsync(category);
            await unitOfWork.SaveChangesAsync();
        }
    }
}