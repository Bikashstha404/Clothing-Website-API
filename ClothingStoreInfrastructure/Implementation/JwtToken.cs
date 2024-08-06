using ClothingStoreInfrastructure.Data;
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

namespace ClothingStoreInfrastructure.Implementation
{
    public class JwtToken
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtToken(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> CreateToken(ApplicationUser userData)
        {
            var roles = await _userManager.GetRolesAsync(userData);

            var authorizationClaims = new List<Claim>
                 {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", userData.Id.ToString()),
                    new Claim("Email", userData.Email.ToString()),
                    new Claim("Name", userData.UserName.ToString()),
                    new Claim("Gender", userData.Gender.ToString()),
                };
            foreach (var role in roles)
            {
                authorizationClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: authorizationClaims,
                    expires: DateTime.UtcNow.AddSeconds(10),
                    signingCredentials: signIn
                );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return accessToken;
        }

        public string CreateRefreshToken()
        {
            string refreshToken;
            do
            {
                var tokenBytes = RandomNumberGenerator.GetBytes(64);
                refreshToken = Convert.ToBase64String(tokenBytes);
            } while (_userManager.Users.Any(a => a.RefreshToken == refreshToken));

            return refreshToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredtoken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(_configuration["Jwt:Key"])), // Should match with security key given during creating jwt token
                ClockSkew = TimeSpan.Zero
            };

            var tokenHanlder = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHanlder.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("This is invalid Token");
            }
            return principal;
        }
    }
}
