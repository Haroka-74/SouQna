namespace SouQna.Application.Exceptions.Enums
{
    public enum AuthenticationErrorType
    {
        InvalidCredentials,
        EmailNotConfirmed,
        InvalidRefreshToken,
        TokenExpired,
        TokenRevoked
    }
}