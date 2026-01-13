using System.Text;
using System.Security.Claims;
using SouQna.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SouQna.Domain.Aggregates.UserAggregate;
using SouQna.Infrastructure.Configuration.Settings;

namespace SouQna.Infrastructure.Services.Authentication
{
    public class JwtTokenService(
        JwtSettings jwtSettings
    ) : IJwtTokenService
    {
        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims:
                [
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    // Add user roles here when create some roles!!!
                ],
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}