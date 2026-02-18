using SouQna.Infrastructure.Enums;

namespace SouQna.Infrastructure.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }
        public string? TransactionId { get; set; }
        public string IntentId { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public Order Order { get; set; } = null!;
    }
}