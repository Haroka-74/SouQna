using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Services
{
    public class CryptoService : ICryptoService
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    }
}