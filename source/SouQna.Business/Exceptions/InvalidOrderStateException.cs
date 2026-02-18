namespace SouQna.Business.Exceptions
{
    public class InvalidOrderStateException(
        string message
    ) : Exception(message);
}