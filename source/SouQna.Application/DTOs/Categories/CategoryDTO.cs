namespace SouQna.Application.DTOs.Categories
{
    public class CategoryDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public ICollection<string> Subcategories { get; init; } = [];
    }
}