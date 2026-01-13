using SouQna.Application.Exceptions.Enums;

namespace SouQna.Application.Exceptions
{
    public class AuthenticationException(
        AuthenticationErrorType type,
        string message
    ) : Exception(message)
    {
        public AuthenticationErrorType ErrorType { get; } = type;
    }
}