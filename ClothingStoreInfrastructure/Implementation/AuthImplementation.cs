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
            var username = await userManager.FindByNameAsync(signUp.Name);
            if (userData != null)
            {
                return "A user with this email already exist";
            }
            if (username != null)
            {
                return "Username already exist.";
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
                if(signUp.Email == configuration["AdminEmail"])
                {
                    var adminExists = await userManager.GetUsersInRoleAsync("Admin");
                    if (adminExists.Any())
                    {
                        return "An Admin already exists";
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                        return "An Admin created successfully";
                    }
                }

                await userManager.AddToRoleAsync(user, "User");
                return "User Registration Successful";
            }
            else
            {
                return $"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}";
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

                return tokenValue;
            }
            else
            {
                return "Incorrect Password.";
            }
        }
    }
}
