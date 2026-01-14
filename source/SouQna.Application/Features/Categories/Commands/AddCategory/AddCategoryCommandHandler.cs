using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;
using SouQna.Domain.Aggregates.CategoryAggregate;

namespace SouQna.Application.Features.Categories.Commands.AddCategory
{
    public class AddCategoryCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<AddCategoryCommand, AddCategoryResponse>
    {
        public async Task<AddCategoryResponse> Handle(
            AddCategoryCommand command,
            CancellationToken cancellationToken
        )
        {
            var category = await unitOfWork.Categories.FindAsync(c => c.Name == command.Name);

            if(category is not null)
                throw new ConflictException($"A category with name '{command.Name}' already exists.");

            if(command.ParentId.HasValue)
                _ = await unitOfWork.Categories.FindAsync(
                    c => c.Id == command.ParentId.Value
                ) ?? throw new NotFoundException($"Parent category with ID {command.ParentId.Value} not found");

            category = Category.Create(
                command.ParentId,
                command.Name,
                command.Description
            );

            await unitOfWork.Categories.AddAsync(category);
            await unitOfWork.SaveChangesAsync();

            return new AddCategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ParentId = category.ParentId,
                CreatedAt = category.CreatedAt
            };
        }
    }
}