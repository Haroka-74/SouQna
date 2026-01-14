namespace SouQna.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<string> Subcategories { get; set; } = [];
    }
}