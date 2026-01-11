using SouQna.Domain.Common;

namespace SouQna.Domain.Aggregates.UserAggregate
{
    public class User
    {
        private readonly List<RefreshToken> _refreshTokens = [];

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string? EmailConfirmationToken { get; private set; }
        public DateTime? EmailConfirmationExpires { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        private User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            EmailConfirmed = false;
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
            Guard.AgainstNegativeOrZero(expirationHours, nameof(expirationHours));

            EmailConfirmationToken = token;
            EmailConfirmationExpires = DateTime.UtcNow.AddHours(expirationHours);
        }

        public void ConfirmEmail(string token)
        {
            Guard.AgainstNullOrEmpty(token, nameof(token));

            Ensure.Not(EmailConfirmed, "Email is already confirmed");
            Ensure.NotNullOrEmpty(EmailConfirmationToken, "No confirmation token has been generated");
            Ensure.That(EmailConfirmationToken == token, "Invalid confirmation token");
            Ensure.That(
                EmailConfirmationExpires.HasValue && EmailConfirmationExpires >= DateTime.UtcNow,
                "Confirmation token has expired"
            );

            EmailConfirmed = true;
            EmailConfirmationToken = null;
            EmailConfirmationExpires = null;
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            Guard.AgainstNull(refreshToken, nameof(refreshToken));

            _refreshTokens.Add(refreshToken);
        }

        public void RemoveRefreshToken(RefreshToken refreshToken)
        {
            Guard.AgainstNull(refreshToken, nameof(refreshToken));

            _refreshTokens.Remove(refreshToken);
        }
    }
}