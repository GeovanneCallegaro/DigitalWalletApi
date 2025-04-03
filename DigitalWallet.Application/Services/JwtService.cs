using DigitalWallet.Application.Interfaces;
using DigitalWallet.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalWallet.Application.Services
{
    public class JwtService(IOptions<AppSettings> appSettings) : IJwtService
    {
        private readonly JwtOptions _jwtOptions = appSettings.Value.Jwt;

        public string GenerateToken(string email, string userId)
        {
            var claims = new List<Claim>
            {
                new("userId", userId),
                new(JwtRegisteredClaimNames.Email, email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
