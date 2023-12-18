using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SecurityApi.Core.Security.Interfaces;
using SecurityApi.Core.Security.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Core.Security.Extensions
{
    public static class SecurityExtensions
    {
        public static void AddJwtSecurity(this IServiceCollection services, IConfiguration configuration) 
        {
            string jwtKey = configuration["JwtSettings:Key"] ?? "NO_Key_Found";

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IJwtFactoryService, JwtFactoryService>();
            services.AddAuthorization();
            services.AddAuthentication("Bearer")
                .AddJwtBearer(opt =>
                {
                    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
                    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);

                    opt.RequireHttpsMetadata = false; //Remove this line for production environments

                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false, //Change this for production environments
                        ValidateIssuer = false, //Change this for production environments
                        IssuerSigningKey = signingKey
                    };
                });
        }
    }
}
