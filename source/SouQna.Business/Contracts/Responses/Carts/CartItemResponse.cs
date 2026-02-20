namespace SouQna.Business.Contracts.Responses.Carts
{
    public record CartItemResponse(
        Guid Id,
        Guid ProductId,
        string ProductName,
        string ProductImage,
        decimal Price,
        int Quantity,
        decimal Subtotal
    );
}