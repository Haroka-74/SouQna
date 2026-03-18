namespace SouQna.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Inventory Inventory { get; private set; }

        private readonly List<CartItem> _cartItems = [];
        public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

        private readonly List<OrderItem> _orderItems = [];
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            Image = string.Empty;
            Inventory = null!;
        }

        private Product(Guid id, string name, string description, string image, decimal price, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            Price = price;
            CreatedAt = createdAt;
            Inventory = null!;
        }
    }
}