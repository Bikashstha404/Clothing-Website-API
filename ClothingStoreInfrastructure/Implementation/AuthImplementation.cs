using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
using ClothingStoreInfrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClothingStoreInfrastructure.Implementation
{
    public class AuthImplementation : IAuth
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;

        public AuthImplementation(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<string> SignUp(SignUp signUp)
        {
            var userData = await userManager.FindByEmailAsync(signUp.Email);
            if (userData != null)
            {
                return "A user with this email already exist";
            }
            if (signUp.Password != signUp.ConfirmPassword)
            {
                return "Both Password and ConfirmPassword should be same.";
            }

            ApplicationUser user = new ApplicationUser
            {
                UserName = signUp.Name,
                Gender = signUp.Gender,
                Email = signUp.Email,
            };

            var result = await userManager.CreateAsync(user, signUp.Password);
            if (result.Succeeded)
            {
                return "Registration Successful";
            }
            else
            {
                return "Error Occured During Registration";
            }
        }

        public async Task<string> Login(Login login)
        {
            var userData = await userManager.FindByEmailAsync(login.Email);
            if (userData == null) 
            {
                return "No User found with this email address.";
            }
            var user = await signInManager.PasswordSignInAsync(userData.UserName, login.Password, false, false);
            if (user.Succeeded)
            {
                JwtToken jwt = new JwtToken(configuration);
                var tokenValue = jwt.CreateToken(userData);

                return "User: " + userData + ", Token: " + tokenValue;
            }
            else
            {
                return "Login Failed";
            }
        }
    }
}
