using SouQna.Domain.Enums;
using SouQna.Domain.Exceptions;

namespace SouQna.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string OrderNumber { get; private set; }
        public string ShippingFullName { get; private set; }
        public string ShippingPhoneNumber { get; private set; }
        public string ShippingCity { get; private set; }
        public string ShippingAddressLine { get; private set; }
        public decimal Total { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ConfirmedAt { get; private set; }
        public DateTime? ShippedAt { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        public DateTime? CancelledAt { get; private set; }

        public User User { get; private set; }

        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private readonly List<Payment> _payments = [];
        public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

        private Order()
        {
            OrderNumber = string.Empty;
            ShippingFullName = string.Empty;
            ShippingPhoneNumber = string.Empty;
            ShippingCity = string.Empty;
            ShippingAddressLine = string.Empty;
            User = null!;
        }

        private Order(
            Guid id,
            Guid userId,
            string orderNumber,
            string shippingFullName,
            string shippingPhoneNumber,
            string shippingCity,
            string shippingAddressLine,
            decimal total,
            OrderStatus orderStatus,
            DateTime createdAt,
            DateTime? confirmedAt,
            DateTime? shippedAt,
            DateTime? deliveredAt,
            DateTime? cancelledAt
        )
        {
            Id = id;
            UserId = userId;
            OrderNumber = orderNumber;
            ShippingFullName = shippingFullName;
            ShippingPhoneNumber = shippingPhoneNumber;
            ShippingCity = shippingCity;
            ShippingAddressLine = shippingAddressLine;
            Total = total;
            OrderStatus = orderStatus;
            CreatedAt = createdAt;
            ConfirmedAt = confirmedAt;
            ShippedAt = shippedAt;
            DeliveredAt = deliveredAt;
            CancelledAt = cancelledAt;
            User = null!;
        }

        public bool IsPending => OrderStatus == OrderStatus.Pending;
        public bool IsConfirmed => OrderStatus == OrderStatus.Confirmed;
        public bool IsProcessing => OrderStatus == OrderStatus.Processing;

        public static Order Create(
            Guid userId,
            string shippingFullName,
            string shippingPhoneNumber,
            string shippingCity,
            string shippingAddressLine,
            decimal total
        )
        {
            return new Order(
                Guid.NewGuid(),
                userId,
                GenerateOrderNumber(),
                shippingFullName,
                shippingPhoneNumber,
                shippingCity,
                shippingAddressLine,
                total,
                OrderStatus.Pending,
                DateTime.UtcNow,
                null,
                null,
                null,
                null
            );
        }

        public void Confirm()
        {
            if (!IsPending)
                throw new InvalidStateException($"Cannot confirm order, status is {OrderStatus}");

            OrderStatus = OrderStatus.Confirmed;
            ConfirmedAt = DateTime.UtcNow;
        }

        public void Process()
        {
            if (!IsConfirmed)
                throw new InvalidStateException($"Cannot process order, status is {OrderStatus}");

            OrderStatus = OrderStatus.Processing;
        }

        public void Ship()
        {
            if (!IsProcessing)
                throw new InvalidStateException($"Cannot ship order, status is {OrderStatus}");

            OrderStatus = OrderStatus.Shipped;
            ShippedAt = DateTime.UtcNow;
        }

        private static string GenerateOrderNumber()
        {
            var date = DateTime.UtcNow.ToString("yyyyMMdd");
            var random = Guid.NewGuid().ToString("N")[..6].ToUpper();
            return $"ORD-{date}-{random}";
        }
    }
}