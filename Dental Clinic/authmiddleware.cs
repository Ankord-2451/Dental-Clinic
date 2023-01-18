﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental_Clinic
{
    public class Authmiddleware
    {
        private readonly RequestDelegate _next;
        public static IConfiguration _configuration { get; set; }
        public Authmiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].First();

            if(authHeader!=null)
            {
                var key = Encoding.UTF8.GetBytes(_configuration["JWT:key"]);
                var token = authHeader.Split(" ").Last().ToString();
                var TokenHandler = new JwtSecurityTokenHandler();
                var validParam = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };

                TokenHandler.ValidateToken(token, validParam,out var validateToken);
            }

            await _next(context);
        }
    }
}