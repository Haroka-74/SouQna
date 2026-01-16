namespace SouQna.Application.Features.Products.Commands.AddProduct
{
    public class AddProductResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Image { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public bool IsActive { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}