using SouQna.Domain.Common;
using SouQna.Domain.Exceptions;
using SouQna.Domain.Aggregates.UserAggregate;
using SouQna.Domain.Aggregates.ProductAggregate;

namespace SouQna.Domain.Aggregates.CartAggregate
{
    public class Cart
    {
        private readonly List<CartItem> _cartItems = [];

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User User { get; private set; }
        public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

        private Cart() => User = null!;

        private Cart(Guid id, Guid userId, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            CreatedAt = createdAt;
            User = null!;
        }

        public static Cart Create(Guid userId)
        {
            Guard.AgainstNullOrEmpty(userId.ToString(), nameof(userId));

            return new Cart(
                Guid.NewGuid(),
                userId,
                DateTime.UtcNow
            );
        }

        public void AddItem(Product product, int quantity)
        {
            Guard.AgainstNull(product, nameof(product));
            Guard.AgainstNegativeOrZero(quantity, nameof(quantity));

            if(!product.HasSufficientStock(quantity))
                throw new InsufficientStockException(product.Quantity, quantity);

            var item = _cartItems.FirstOrDefault(ci => ci.ProductId == product.Id);

            if(item is not null)
            {
                if(!product.HasSufficientStock(item.Quantity + quantity))
                    throw new InsufficientStockException(product.Quantity, item.Quantity + quantity);
                item.IncreaseQuantity(quantity);
            }
            else
            {
                item = CartItem.Create(Id, product.Id, product.Price, quantity);
                _cartItems.Add(item);
            }
        }
    }
}