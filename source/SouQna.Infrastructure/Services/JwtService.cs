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

            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: settings.Issuer,
                audience: settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}