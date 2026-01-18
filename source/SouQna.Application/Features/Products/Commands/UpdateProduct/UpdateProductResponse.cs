namespace SouQna.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public decimal Price { get; init; }
    }
}