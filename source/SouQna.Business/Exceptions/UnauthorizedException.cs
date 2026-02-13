namespace SouQna.Business.Exceptions
{
    public class UnauthorizedException(
        string message
    ) : Exception(message);
}