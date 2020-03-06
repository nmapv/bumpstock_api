using bumpstock_api.entity.Entity.Public;
using bumpstock_api.infrastructure.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace bumpstock_api.api.Authentication
{
    public static class TokenService
    {
        public static string GenerateToken(Person person)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Cryptography.Key;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, person.id.ToString()),
                    new Claim(ClaimTypes.Name, person.first_name ?? string.Empty),
                    new Claim(ClaimTypes.GivenName, person.last_name ?? string.Empty),
                    new Claim(ClaimTypes.Hash, person.hash),
                    new Claim(ClaimTypes.Role, person.role)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
