namespace SouQna.Domain.Exceptions
{
    public class InsufficientStockException(
        string productName
    ) : Exception($"Insufficient stock for product '{productName}'") {}
}