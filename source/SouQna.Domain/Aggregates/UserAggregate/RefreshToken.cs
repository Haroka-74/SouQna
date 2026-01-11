using SouQna.Domain.Common;

namespace SouQna.Domain.Aggregates.UserAggregate
{
    public class RefreshToken
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Token { get; private set; }
        public bool IsRevoked { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User User { get; private set; }

        private RefreshToken()
        {
            Token = string.Empty;
            User = null!;
        }

        private RefreshToken(
            Guid id,
            Guid userId,
            string token,
            bool isRevoked,
            DateTime expiresAt,
            DateTime createdAt
        )
        {
            Id = id;
            UserId = userId;
            Token = token;
            IsRevoked = isRevoked;
            ExpiresAt = expiresAt;
            CreatedAt = createdAt;
            User = null!;
        }

        public static RefreshToken Create(Guid userId, string token, int expiryInDays)
        {
            Guard.AgainstNullOrEmpty(userId.ToString(), nameof(userId));
            Guard.AgainstNullOrEmpty(token, nameof(token));
            Guard.AgainstNegativeOrZero(expiryInDays, nameof(expiryInDays));

            return new RefreshToken(
                Guid.NewGuid(),
                userId,
                token,
                false,
                DateTime.UtcNow.AddDays(expiryInDays),
                DateTime.UtcNow
            );
        }

        public void Revoke()
        {
            Ensure.Not(IsRevoked, "Token is already revoked");

            IsRevoked = true;
        }
    }
}