namespace SouQna.Application.Features.Authentication.Commands.RefreshToken
{
    public class RefreshTokenResponse
    {
        public string AccessToken { get; init; } = string.Empty;
        public string RefreshToken { get; init; } = string.Empty;
        public DateTime RefreshTokenExpiration { get; init; }
    }
}