using DigitalInsights.API.SilverDashboard.DTO;
using DigitalInsights.API.SilverDashboard.Security;
using DigitalInsights.DB.Silver;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace DigitalInsights.API.SilverDashboard.Services
{
    public class LoginService
    {
        SilverContext silverContext;

        const string SECRET = "4P1nkY4ndTh3Br41n";
        static readonly SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET));

        public LoginService(SilverContext context)
        {
            this.silverContext = context;
        }

        public AuthResponseDTO CreateToken(AuthInfoDTO authInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, authInfo.UserName),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = silverContext.Users.Where(x => x.UserName == authInfo.UserName).FirstOrDefault();

            if(user == null)
            {
                throw new SecurityTokenValidationException("User not found");
            }

            if (PasswordHelper.VerifyHashedPassword(user.Password, authInfo.Password) == PasswordVerificationResult.Success)
            {
                return new AuthResponseDTO()
                {
                    Token = tokenHandler.WriteToken(token)
                };
            }
            throw new SecurityTokenValidationException("Unauthorized");
        }

        public bool ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
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
