using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Services
{
    public class Hasher : IHasher
    {
        public string Hash(string plainText)
            => BCrypt.Net.BCrypt.HashPassword(plainText);

        public bool Verify(string plainText, string hash)
            => BCrypt.Net.BCrypt.Verify(plainText, hash);
    }
}