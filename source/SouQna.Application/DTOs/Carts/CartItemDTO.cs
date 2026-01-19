namespace SouQna.Application.DTOs.Carts
{
    public class CartItemDTO
    {
        public string ProductName { get; init; } = string.Empty;
        public string ProductImage { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int Quantity { get; init; }
        public decimal Subtotal { get; init; }
    }
}