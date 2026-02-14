namespace SouQna.Business.Contracts.Responses
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