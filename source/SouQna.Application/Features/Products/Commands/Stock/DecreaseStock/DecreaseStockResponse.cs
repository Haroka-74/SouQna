namespace SouQna.Application.Features.Products.Commands.Stock.DecreaseStock
{
    public class DecreaseStockResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Quantity { get; init; }
    }
}