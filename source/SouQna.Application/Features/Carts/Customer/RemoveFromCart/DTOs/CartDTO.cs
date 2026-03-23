namespace SouQna.Application.Features.Carts.Customer.RemoveFromCart.DTOs
{
    public record CartDTO(
        int TotalItems,
        decimal TotalAmount,
        ICollection<CartItemDTO> Items
    );
}