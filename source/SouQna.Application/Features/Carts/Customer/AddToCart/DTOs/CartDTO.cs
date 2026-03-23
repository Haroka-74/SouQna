namespace SouQna.Application.Features.Carts.Customer.AddToCart.DTOs
{
    public record CartDTO(
        int TotalItems,
        decimal TotalAmount,
        ICollection<CartItemDTO> Items
    );
}