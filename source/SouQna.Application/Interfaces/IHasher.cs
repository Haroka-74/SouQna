namespace SouQna.Application.Interfaces
{
    public interface IHasher
    {
        string Hash(string plain);
        bool Verify(string plain, string hash);
    }
}