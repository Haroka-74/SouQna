namespace SouQna.Application.DTOs.Products
{
    public class ProductDTO
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public bool IsActive { get; init; }
        public string CategoryName { get; init; } = string.Empty;
    }
}