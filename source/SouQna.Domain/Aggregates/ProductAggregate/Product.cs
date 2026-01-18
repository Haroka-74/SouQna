using SouQna.Domain.Common;
using SouQna.Domain.Aggregates.CategoryAggregate;
using SouQna.Domain.Exceptions;

namespace SouQna.Domain.Aggregates.ProductAggregate
{
    public class Product
    {
        public Guid Id { get; private set; }
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Category Category { get; private set; }

        private Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            Image = string.Empty;
            Category = null!;
        }

        private Product(
            Guid id,
            Guid categoryId,
            string name,
            string description,
            string image,
            decimal price,
            int quantity,
            bool isActive,
            DateTime createdAt
        )
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            Description = description;
            Image = image;
            Price = price;
            Quantity = quantity;
            IsActive = isActive;
            CreatedAt = createdAt;
            Category = null!;
        }

        public static Product Create(
            Guid categoryId,
            string name,
            string description,
            string image,
            decimal price,
            int quantity,
            bool isActive
        )
        {
            Guard.AgainstNullOrEmpty(categoryId.ToString(), nameof(categoryId));
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(description, nameof(description));
            Guard.AgainstNullOrEmpty(image, nameof(image));
            Guard.AgainstOutOfRange(price, 0m, decimal.MaxValue, nameof(price));
            Guard.AgainstOutOfRange(quantity, 0, int.MaxValue, nameof(quantity));

            return new Product(
                Guid.NewGuid(),
                categoryId,
                name,
                description,
                image,
                price,
                quantity,
                isActive,
                DateTime.UtcNow
            );
        }

        public void UpdateDetails(string name, string description, decimal price)
        {
            Guard.AgainstNullOrEmpty(name, nameof(name));
            Guard.AgainstNullOrEmpty(description, nameof(description));
            Guard.AgainstOutOfRange(price, 0m, decimal.MaxValue, nameof(price));

            Name = name;
            Description = description;
            Price = price;
        }

        public void IncreaseStock(int quantity)
        {
            Guard.AgainstNegativeOrZero(quantity, nameof(quantity));

            Quantity += quantity;
        }

        public void DecreaseStock(int quantity)
        {
            Guard.AgainstNegativeOrZero(quantity, nameof(quantity));

            if (Quantity < quantity)
                throw new InsufficientStockException(Quantity, quantity);

            Quantity -= quantity;
        }
    }
}