namespace SouQna.Application.DTOs.Carts
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