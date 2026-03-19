using SouQna.Domain.Enums;
using SouQna.Domain.Exceptions;

namespace SouQna.Domain.Entities
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public long IntentionOrderId { get; private set; }
        public string CheckoutUrl { get; private set; }
        public int RetryCount { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public Order Order { get; private set; }

        private Payment()
        {
            CheckoutUrl = string.Empty;
            Order = null!;
        }

        private Payment(
            Guid id,
            Guid orderId,
            long intentionOrderId,
            string checkoutUrl,
            int retryCount,
            PaymentStatus paymentStatus,
            DateTime createdAt,
            DateTime expiresAt
        )
        {
            Id = id;
            OrderId = orderId;
            IntentionOrderId = intentionOrderId;
            CheckoutUrl = checkoutUrl;
            RetryCount = retryCount;
            PaymentStatus = paymentStatus;
            CreatedAt = createdAt;
            ExpiresAt = expiresAt;
            Order = null!;
        }

        public bool IsPending => PaymentStatus == PaymentStatus.Pending;
        public bool IsExhausted => RetryCount == 3;
        public bool IsExpired => DateTime.UtcNow > ExpiresAt;

        public static Payment Create(
            Guid orderId,
            long intentionOrderId,
            string checkoutUrl,
            DateTime createdAt
        )
        {
            return new Payment(
                Guid.NewGuid(),
                orderId,
                intentionOrderId,
                checkoutUrl,
                0,
                PaymentStatus.Pending,
                createdAt,
                createdAt.AddSeconds(900)
            );
        }

        public void MarkAsExpired()
        {
            if(!IsPending)
                throw new InvalidStateException($"Cannot expire payment, status is {PaymentStatus}");

            PaymentStatus = PaymentStatus.Expired;
        }

        public void RegisterFailedAttempt()
        {
            if(PaymentStatus != PaymentStatus.Pending)
                return;

            RetryCount++;

            if(RetryCount >= 3)
                PaymentStatus = PaymentStatus.Exhausted;
        }

        public void MarkAsPaid()
        {
            PaymentStatus = PaymentStatus.Paid;
        }
    }
}