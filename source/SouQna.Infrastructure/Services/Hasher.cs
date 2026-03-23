using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Services
{
    public class Hasher : IHasher
    {
        public string Hash(string plain)
            => BCrypt.Net.BCrypt.HashPassword(plain);

        public bool Verify(string plain, string hash)
            => BCrypt.Net.BCrypt.Verify(plain, hash);
    }
}