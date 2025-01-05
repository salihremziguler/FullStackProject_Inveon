using CourseSalesAPI.Application.Abstractions.Token;
using CourseSalesAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CourseSalesAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int second, AppUser user)
        {
            Application.DTOs.Token token = new();

            // Güvenlik anahtarı oluşturma
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            // İmzalama bilgileri
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // Token süresi
            token.Expiration = DateTime.UtcNow.AddSeconds(second);

            // Claim listesi (Adı özelleştirilmiş)
            var claims = new List<Claim>
    {
        new("name", user.UserName) // "http://schemas.xmlsoap.org/..." yerine sadece "name"
    };

            // JWT oluşturma
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
            );

            // Token oluşturucu sınıf
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            // Refresh token oluşturma
            token.RefreshToken = CreateRefreshToken();

            return token;
        }




        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}