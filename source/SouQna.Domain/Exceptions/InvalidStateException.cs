namespace SouQna.Domain.Exceptions
{
    public class InvalidStateException(
        string message
    ) : Exception(message);
}