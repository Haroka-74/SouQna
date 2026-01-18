namespace SouQna.Application.Features.Products.Commands.Stock.IncreaseStock
{
    public class IncreaseStockResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public int Quantity { get; init; }
    }
}