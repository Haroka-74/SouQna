namespace SouQna.Application.Features.Carts.Customer.UpdateCartItem.DTOs
{
    public record CartDTO(
        int TotalItems,
        decimal TotalAmount,
        ICollection<CartItemDTO> Items
    );
}