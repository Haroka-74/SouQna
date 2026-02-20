using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using SouQna.Infrastructure.Entities;
using System.IdentityModel.Tokens.Jwt;
using SouQna.Infrastructure.Interfaces;
using SouQna.Infrastructure.Configurations.Settings;

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