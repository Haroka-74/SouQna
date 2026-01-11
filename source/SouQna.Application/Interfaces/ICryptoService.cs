namespace SouQna.Application.Interfaces
{
    public interface ICryptoService
    {
        string Hash(string password);
        bool Verify(string password, string passwordHash);
    }
}