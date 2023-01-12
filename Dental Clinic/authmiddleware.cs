using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Dental_Clinic
{
    public class Authmiddleware
    {
        private readonly RequestDelegate _next;
        public Authmiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if(authHeader!=null)
            {
                var token = authHeader.Split(" ").Last();
                var TokenHandler = new JwtSecurityTokenHandler();
                var validParam = new TokenValidationParameters();

                TokenHandler.ValidateToken(token, validParam,out var validateToken);
            }

            await _next(context);
        }
    }
}
