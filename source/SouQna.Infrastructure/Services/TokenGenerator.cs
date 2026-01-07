using System.Security.Cryptography;
using SouQna.Application.Interfaces;

namespace SouQna.Infrastructure.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        public string Generate() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}