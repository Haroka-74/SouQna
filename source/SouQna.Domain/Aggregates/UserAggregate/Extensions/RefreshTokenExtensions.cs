namespace SouQna.Domain.Aggregates.UserAggregate.Extensions
{
    public static class RefreshTokenExtensions
    {
        public static bool IsExpired(this RefreshToken token)
            => DateTime.UtcNow >= token.ExpiresAt;

        public static bool IsValid(this RefreshToken token)
            => !token.IsRevoked && !token.IsExpired();
    }
}