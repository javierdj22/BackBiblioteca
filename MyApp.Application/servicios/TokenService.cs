using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyApp.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey = "estaEsUnaClaveSuperSecretaDeAlMenos32!";

        public TokenService(string secretKey)
        {
            _secretKey = secretKey;
        }

        public Task<string> GenerateJwtTokenAsync(LoginDto loginDto)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, loginDto.Username),
                new Claim(ClaimTypes.NameIdentifier, loginDto.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)); 
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourAppName",  
                audience: "YourAppName",
                claims: claims,
                expires: DateTime.Now.AddHours(1),  
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);  // Genera el JWT como cadena

            return Task.FromResult(tokenString);  // Devuelve el token como string
        }
    }
}
