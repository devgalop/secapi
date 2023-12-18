using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecurityApi.Core.Security.Domain;
using SecurityApi.Core.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Core.Security.Services
{
    public class JwtFactoryService : IJwtFactoryService
    {
        private readonly IConfiguration _configuration;

        public JwtFactoryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token GenerateToken(User user) 
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]?? "NO_Key_Found");
            SymmetricSecurityKey key = new SymmetricSecurityKey(tokenKey);
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username!)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = credentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Token(tokenHandler.WriteToken(token),token.ValidTo.ToLocalTime());
        }
    }
}
