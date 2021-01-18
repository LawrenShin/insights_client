﻿using DigitalInsights.API.SilverDashboard.DTO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalInsights.API.SilverDashboard.Services
{
    public class LoginService
    {
        const string SECRET = "4P1nkY4ndTh3Br41n";
        const string ISSUER = "https://digital-insights.com";
        const string AUDIENCE = "https://digital-insights.com";
        static readonly SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET));

        public AuthResponseDTO CreateToken(AuthInfoDTO authInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, authInfo.UserName),
                }),
                Expires = DateTime.UtcNow.AddDays(15),
                Issuer = ISSUER,
                Audience = AUDIENCE,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthResponseDTO()
            {
                Token = tokenHandler.WriteToken(token)
            };
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = ISSUER,
                    ValidAudience = AUDIENCE,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
