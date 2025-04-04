// <copyright file="JwtService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DigitalWallet.Application.Services
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using DigitalWallet.Application.Interfaces;
    using DigitalWallet.Infrastructure.Configuration;

    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class JwtService(IOptions<AppSettings> appSettings): IJwtService
    {
        private readonly JwtOptions jwtOptions = appSettings.Value.Jwt;

        public string GenerateToken(string email, string userId)
        {
            var claims = new List<Claim>
            {
                new ("userId", userId),
                new (JwtRegisteredClaimNames.Email, email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: this.jwtOptions.Issuer,
                audience: this.jwtOptions.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
