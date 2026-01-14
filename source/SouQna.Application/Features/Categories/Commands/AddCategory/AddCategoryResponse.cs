namespace SouQna.Application.Features.Categories.Commands.AddCategory
{
    public class AddCategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}