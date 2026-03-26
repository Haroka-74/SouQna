using SouQna.Domain.Exceptions;

namespace SouQna.Domain.Entities
{
    public class Review
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Rating { get; private set; }
        public string Body { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User User { get; private set; }
        public Product Product { get; private set; }

        private Review()
        {
            Body = string.Empty;
            User = null!;
            Product = null!;
        }

        private Review(Guid id, Guid userId, Guid productId, int rating, string body, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            ProductId = productId;
            Rating = rating;
            Body = body;
            CreatedAt = createdAt;
            User = null!;
            Product = null!;
        }

        public static Review Create(Guid userId, Guid productId, int rating, string body)
        {
            if (rating < 1 || rating > 5)
                throw new InvalidStateException("Rating must be between 1 and 5");

            return new Review(
                Guid.NewGuid(),
                userId,
                productId,
                rating,
                body,
                DateTime.UtcNow
            );
        }
    }
}