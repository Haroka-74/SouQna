namespace SouQna.Business.Exceptions
{
    public class ConflictException(
        string message
    ) : Exception(message) {}
}