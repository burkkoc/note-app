﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteApp.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Business.Services
{
    public class JWTService
    {
        private readonly IConfiguration _configuration;
        private readonly JWTOption _options;

        public JWTService(IConfiguration configuration, IOptions<JWTOption> options)
        {
            _configuration = configuration;
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public string GenerateToken(IdentityUser user)
        {

            var securityKey = Encoding.UTF8.GetBytes(_options.Key);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };



            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal? GetPrinciples(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        public ClaimsPrincipal ValidateToken(IHttpContextAccessor httpContextAccessor)
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
                throw new Exception("Invalid or missing Authorization header");

            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var principal = GetPrinciples(token);
            if (principal == null)
                throw new Exception("Invalid token.");

            return principal;
        }

        public string GetUsername(ClaimsPrincipal principal)
        {
            var username = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (username == null)
                throw new Exception("User ID not found in token.");
            return username;
        }
    }
}
