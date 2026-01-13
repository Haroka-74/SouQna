using SouQna.Domain.Aggregates.UserAggregate;

namespace SouQna.Domain.Extensions
{
    public static class UserExtensions
    {
        public static void CleanupInvalidRefreshTokens(this User user)
        {
            var refreshTokens = user.RefreshTokens.Where(t => !t.IsValid).ToList();

            foreach (var token in refreshTokens)
                user.RemoveRefreshToken(token);
        }
    }
}