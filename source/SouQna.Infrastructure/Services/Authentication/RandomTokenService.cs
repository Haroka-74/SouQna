using System.Security.Cryptography;
using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Services.Authentication
{
    public class RandomTokenService : IRandomTokenService
    {
        public string Generate(int length)
            => Convert.ToBase64String(RandomNumberGenerator.GetBytes(length));
    }
}