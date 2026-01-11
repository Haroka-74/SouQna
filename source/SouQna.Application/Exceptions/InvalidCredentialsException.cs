namespace SouQna.Application.Exceptions
{
    public class InvalidCredentialsException(
        string message
    ) : Exception(message) {}
}