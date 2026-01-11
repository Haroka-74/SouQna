namespace SouQna.Application.Exceptions
{
    public class EmailNotConfirmedException(
        string message
    ) : Exception(message) {}
}