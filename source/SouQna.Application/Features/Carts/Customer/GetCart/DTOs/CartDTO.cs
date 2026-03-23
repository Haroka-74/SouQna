namespace SouQna.Application.Features.Carts.Customer.GetCart.DTOs
{
    public record CartDTO(
        int TotalItems,
        decimal TotalAmount,
        ICollection<CartItemDTO> Items
    );
}