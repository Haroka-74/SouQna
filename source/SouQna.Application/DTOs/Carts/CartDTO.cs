namespace SouQna.Application.DTOs.Carts
{
    public record CartDTO(
        int TotalItems,
        decimal TotalAmount,
        ICollection<CartItemDTO> Items
    );
}