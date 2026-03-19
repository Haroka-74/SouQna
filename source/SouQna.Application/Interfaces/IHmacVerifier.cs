namespace SouQna.Application.Interfaces
{
    public interface IHmacVerifier
    {
        bool Verify(string json, string hmac);
    }
}