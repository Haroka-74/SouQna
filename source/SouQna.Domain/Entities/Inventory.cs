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

        public void DecrementStock(int quantity)
        {
            if (Quantity < quantity)
                throw new InsufficientStockException(Product.Name);

            Quantity -= quantity;
        }
    }
}