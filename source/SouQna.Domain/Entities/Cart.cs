namespace SouQna.Domain.Entities
{
    public class Cart
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }

        public User User { get; private set; }

        private readonly List<CartItem> _cartItems = [];
        public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

        private Cart() => User = null!;

        private Cart(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
            User = null!;
        }

        public static Cart Create(Guid userId)
        {
            return new Cart(
                Guid.NewGuid(),
                userId
            );
        }

        public void AddItem(Product product, int quantity)
        {
            var item = _cartItems.FirstOrDefault(ci => ci.ProductId == product.Id);

            if(item is null)
            {
                _cartItems.Add(
                    CartItem.Create(
                        Id,
                        product.Id,
                        quantity,
                        product.Price
                    )
                );
            }
            else
            {
                item.IncreaseQuantity(quantity);
            }
        }

        public void UpdateItemQuantity(Guid productId, int newQuantity)
        {
            var item = _cartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if(item is null)
                return;

            item.UpdateQuantity(newQuantity);
        }

        public void RemoveItem(Guid productId)
        {
            var item = _cartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if(item is not null)
                _cartItems.Remove(item);
        }
    }
}