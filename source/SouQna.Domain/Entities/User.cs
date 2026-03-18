namespace SouQna.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Cart Cart { get; private set; }

        private readonly List<Order> _orders = [];
        public IReadOnlyCollection<Order> Orders => _orders.AsReadOnly();

        private User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            Cart = null!;
        }

        private User(Guid id, string firstName, string lastName, string email, string passwordHash, DateTime createdAt)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = createdAt;
            Cart = null!;
        }

        public static User Create(string firstName, string lastName, string email, string passwordHash)
        {
            return new User(
                Guid.NewGuid(),
                firstName.Trim(),
                lastName.Trim(),
                email.Trim().ToLowerInvariant(),
                passwordHash,
                DateTime.UtcNow
            );
        }
    }
}