namespace SouQna.Application.Features.Carts.Customer.UpdateCartItem.DTOs
{
    public record CartItemDTO(
        Guid Id,
        Guid ProductId,
        string ProductName,
        string ProductImage,
        int Quantity,
        decimal PriceSnapshot,
        decimal Subtotal
    );
}