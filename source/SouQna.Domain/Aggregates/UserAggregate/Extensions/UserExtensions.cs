namespace SouQna.Domain.Aggregates.UserAggregate.Extensions
{
    public static class UserExtensions
    {
        public static void CleanupInvalidRefreshTokens(this User user)
        {
            var refreshTokensToRemove = user.RefreshTokens.Where(t => !t.IsValid()).ToList();

            foreach (var token in refreshTokensToRemove)
                user.RemoveRefreshToken(token);
        }
    }
}