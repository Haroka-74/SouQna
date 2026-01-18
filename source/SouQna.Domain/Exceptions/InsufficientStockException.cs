namespace SouQna.Domain.Exceptions
{
    public class InsufficientStockException(
        int available,
        int requested)
    : Exception($"Insufficient stock. Available: {available}, Requested: {requested}")
    {
        public int Available { get; } = available;
        public int Requested { get; } = requested;
    }
}