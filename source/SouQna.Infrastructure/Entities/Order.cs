using SouQna.Infrastructure.Enums;

namespace SouQna.Infrastructure.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public Guid UserId { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }
        public string ShippingFullName { get; set; } = string.Empty;
        public string ShippingAddressLine { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;
        public string ShippingPhoneNumber { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public DateTime? ShippedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? CancelledAt { get; set; }

        public User User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; } = [];
    }
}