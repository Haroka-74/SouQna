namespace SouQna.Domain.Entities
{
    public class CartItem
    {
        public Guid Id { get; private set; }
        public Guid CartId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal PriceSnapshot { get; private set; }

        public Cart Cart { get; private set; }
        public Product Product { get; private set; }

        private CartItem()
        {
            Cart = null!;
            Product = null!;
        }

        private CartItem(Guid id, Guid cartId, Guid productId, int quantity, decimal priceSnapshot)
        {
            Id = id;
            CartId = cartId;
            ProductId = productId;
            Quantity = quantity;
            PriceSnapshot = priceSnapshot;
            Cart = null!;
            Product = null!;
        }

        public static CartItem Create(Guid cartId, Guid productId, int quantity, decimal priceSnapshot)
        {
            return new CartItem(
                Guid.NewGuid(),
                cartId,
                productId,
                quantity,
                priceSnapshot
            );
        }

        public void IncreaseQuantity(int amount)
        {
            Quantity += amount;
        }

        public void UpdateQuantity(int newQuantity)
        {
            Quantity = newQuantity;
        }
    }
}