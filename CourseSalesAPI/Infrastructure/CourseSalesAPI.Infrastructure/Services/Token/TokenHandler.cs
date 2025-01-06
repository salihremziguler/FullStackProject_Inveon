using CourseSalesAPI.Application.Abstractions.Token;
using CourseSalesAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
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
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<Application.DTOs.Token> CreateAccessToken(int second, AppUser user)
        {
            Application.DTOs.Token token = new();

       
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

      
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

     
            token.Expiration = DateTime.UtcNow.AddSeconds(second);

          
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new("name", user.UserName),
                new("userId",user.Id)

            };

            claims.AddRange(roles.Select(role => new Claim("role", role)));

            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
            );


            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);


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