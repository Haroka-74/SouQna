using SouQna.Domain.Exceptions;

namespace SouQna.Domain.Entities
{
    public class Inventory
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public Product Product { get; private set; }

        private Inventory() => Product = null!;

        private Inventory(Guid id, Guid productId, int quantity)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            Product = null!;
        }

        public static Inventory Create(Guid productId, int quantity)
        {
            return new Inventory(
                Guid.NewGuid(),
                productId,
                quantity
            );
        }

        public void DecrementStock(int quantity)
        {
            if (Quantity < quantity)
                throw new InsufficientStockException(Product.Name);

            Quantity -= quantity;
        }

        public void AdjustStock(int adjustment)
        {
            var newQuantity = Quantity + adjustment;

            if(newQuantity < 0)
                throw new InsufficientStockException(Product.Name);

            Quantity = newQuantity;
        }
    }
}