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

        public void SetEmailConfirmationToken(string token, int expirationHours)
        {
            Guard.AgainstNullOrEmpty(token, nameof(token));

            EmailConfirmationToken = token;
            EmailConfirmationExpires = DateTime.UtcNow.AddHours(expirationHours);
        }

        public void ConfirmEmail(string token)
        {
            Guard.AgainstNullOrEmpty(token, nameof(token));

            if(EmailConfirmed)
                throw new InvalidOperationException("Email is already confirmed");

            if(string.IsNullOrEmpty(EmailConfirmationToken))
                throw new InvalidOperationException("No confirmation token has been generated");

            if (EmailConfirmationToken != token)
                throw new InvalidOperationException("Invalid confirmation token");

            if (EmailConfirmationExpires == null || EmailConfirmationExpires < DateTime.UtcNow)
                throw new InvalidOperationException("Confirmation token has expired");

            EmailConfirmed = true;
        }
    }
}