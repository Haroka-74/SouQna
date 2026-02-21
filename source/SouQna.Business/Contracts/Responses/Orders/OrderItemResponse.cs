namespace SouQna.Business.Contracts.Responses.Orders
{
    public record OrderItemResponse(
        Guid Id,
        Guid ProductId,
        string ProductName,
        string ProductDescription,
        string ProductImage,
        decimal Price,
        int Quantity,
        decimal Subtotal
    );
}