using SouQna.Domain.Common;
using SouQna.Domain.Aggregates.ProductAggregate;

namespace SouQna.Domain.Aggregates.CartAggregate
{
    public class CartItem
    {
        public Guid Id { get; private set; }
        public Guid CartId { get; private set; }
        public Guid ProductId { get; private set; }
        public decimal PriceAtAddition { get; private set; }
        public int Quantity { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Cart Cart { get; private set; }
        public Product Product { get; private set; }

        private CartItem()
        {
            Cart = null!;
            Product = null!;
        }

        private CartItem(
            Guid id,
            Guid cartId,
            Guid productId,
            decimal priceAtAddition,
            int quantity,
            DateTime createdAt
        )
        {
            Id = id;
            CartId = cartId;
            ProductId = productId;
            PriceAtAddition = priceAtAddition;
            Quantity = quantity;
            CreatedAt = createdAt;
            Cart = null!;
            Product = null!;
        }

        public static CartItem Create(
            Guid cartId,
            Guid productId,
            decimal priceAtAddition,
            int quantity
        )
        {
            Guard.AgainstNullOrEmpty(cartId.ToString(), nameof(cartId));
            Guard.AgainstNullOrEmpty(productId.ToString(), nameof(productId));
            Guard.AgainstNegativeOrZero(priceAtAddition, nameof(priceAtAddition));
            Guard.AgainstNegativeOrZero(quantity, nameof(quantity));

            return new CartItem(
                Guid.NewGuid(),
                cartId,
                productId,
                priceAtAddition,
                quantity,
                DateTime.UtcNow
            );
        }

        public void IncreaseQuantity(int amount)
        {
            Guard.AgainstNegativeOrZero(amount, nameof(amount));

            Quantity += amount;
        }

        public void DecreaseQuantity(int amount)
        {
            Guard.AgainstNegativeOrZero(amount, nameof(amount));
            Guard.AgainstNegative(Quantity - amount, nameof(Quantity));

            Quantity -= amount;
        }
    }
}