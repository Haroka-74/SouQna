using System.Text;
using SouQna.Domain.Entities;
using System.Security.Claims;
using SouQna.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using SouQna.Infrastructure.Settings;
using System.IdentityModel.Tokens.Jwt;

namespace SouQna.Infrastructure.Services
{
    public class JwtService(JwtSettings settings) : IJwtService
    {
        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims:
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                ],
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}