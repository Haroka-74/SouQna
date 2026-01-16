namespace SouQna.Application.Features.Categories.Commands.AddCategory
{
    public class AddCategoryResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public Guid? ParentId { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}