using SouQna.Domain.Common;

namespace SouQna.Domain.Aggregates.UserAggregate
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string? EmailConfirmationToken { get; private set; }
        public DateTime? EmailConfirmationExpires { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public IEnumerable<RefreshToken> RefreshTokens { get; private set; }

        private User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            EmailConfirmed = false;
            RefreshTokens = [];
        }

        private User(
            Guid id,
            string firstName,
            string lastName,
            string email,
            string passwordHash,
            DateTime createdAt
        )
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            EmailConfirmed = false;
            EmailConfirmationToken = null;
            EmailConfirmationExpires = null;
            CreatedAt = createdAt;
            RefreshTokens = [];
        }

        public static User Create(string firstName, string lastName, string email, string passwordHash)
        {
            Guard.AgainstNullOrEmpty(firstName, nameof(firstName));
            Guard.AgainstNullOrEmpty(lastName, nameof(lastName));
            Guard.AgainstNullOrEmpty(email, nameof(email));
            Guard.AgainstNullOrEmpty(passwordHash, nameof(passwordHash));

            return new User(
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                passwordHash,
                DateTime.UtcNow
            );
        }
    }
}