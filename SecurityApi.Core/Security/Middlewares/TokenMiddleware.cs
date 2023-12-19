using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SecurityApi.Core.Security.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityApi.Core.Security.Middlewares
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="configuration"></param>
        public TokenMiddleware(RequestDelegate next, 
            IConfiguration configuration,
            IUserService userService)
        {
            _next = next;
            _configuration = configuration;
            _userService = userService;
        }

        /// <summary>
        /// Search for the header token and if it is null it returns an unauthorized, otherwise it validates the token
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await attachUserToContextAsync(context, token);
            }

            await _next(context);
        }


        /// <summary>
        /// Validate token parameters
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        private async Task attachUserToContextAsync(HttpContext context, string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                //LoginSesion currentSesion = await loginSesionService.GetSesion(token);

                //if (currentSesion == null)
                //{
                //    return;
                //}

                byte[] key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]?? "NO_Key_Found");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),

                    ValidateIssuer = false,
                    ValidIssuer = _configuration["JwtSettings:Issuer"],

                    ValidateAudience = false,
                    ValidAudience = _configuration["JwtSettings:Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                string username = (jwtToken.Claims.First(x => x.Type == "unique_name").Value);

                ////validate service!!!

                //// attach user to context on successful jwt validation

                context.Items["UserName"] = username;
                context.Items["User"] = await _userService.GetUser(username);
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
