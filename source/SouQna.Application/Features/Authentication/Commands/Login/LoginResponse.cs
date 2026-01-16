namespace SouQna.Application.Features.Authentication.Commands.Login
{
    public class LoginResponse
    {
        public string AccessToken { get; init; } = string.Empty;
        public string RefreshToken { get; init; } = string.Empty;
        public DateTime RefreshTokenExpiration { get; init; }
    }
}