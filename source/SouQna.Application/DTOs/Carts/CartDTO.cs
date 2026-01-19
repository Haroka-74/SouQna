namespace SouQna.Application.DTOs.Carts
{
    public class CartDTO
    {
        public decimal TotalAmount { get; init; }
        public int TotalItems { get; init; }
        public ICollection<CartItemDTO> Items { get; init; } = [];
    }
}