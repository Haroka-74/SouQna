namespace SouQna.Domain.Entities
{
    public class OrderItem
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ItemName { get; private set; }
        public string ItemImage { get; private set; }
        public decimal ItemPrice { get; private set; }
        public int ItemQuantity { get; private set; }

        public Order Order { get; private set; }
        public Product Product { get; private set; }

        private OrderItem()
        {
            ItemName = string.Empty;
            ItemImage = string.Empty;
            Order = null!;
            Product = null!;
        }

        private OrderItem(
            Guid id,
            Guid orderId,
            Guid productId,
            string itemName,
            string itemImage,
            decimal itemPrice,
            int itemQuantity
        )
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            ItemName = itemName;
            ItemImage = itemImage;
            ItemPrice = itemPrice;
            ItemQuantity = itemQuantity;
            Order = null!;
            Product = null!;
        }

        public static OrderItem Create(
            Guid orderId,
            Guid productId,
            string itemName,
            string itemImage,
            decimal itemPrice,
            int itemQuantity
        )
        {
            return new OrderItem(
                Guid.NewGuid(),
                orderId,
                productId,
                itemName,
                itemImage,
                itemPrice,
                itemQuantity
            );
        }
    }
}