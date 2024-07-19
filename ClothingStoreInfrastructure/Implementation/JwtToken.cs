using ClothingStoreInfrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreInfrastructure.Implementation
{
    public class JwtToken
    {
        private readonly IConfiguration configuration;

        public JwtToken(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string CreateToken(ApplicationUser userData)
        {
            
            var authorizationClaims = new[]
                 {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userData.Id.ToString()),
                    new Claim("Email", userData.Email.ToString()),
                    new Claim("UserName", userData.UserName.ToString()),
                    new Claim("Gender", userData.Gender.ToString()),
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: authorizationClaims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn
                );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
