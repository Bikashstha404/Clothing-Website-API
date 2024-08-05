using ClothingStoreApplication.Interface;
using ClothingStoreApplication.Response;
using ClothingStoreDomain;
using ClothingStoreInfrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
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
        private ClothDbContext clothDbContext;

        public AuthImplementation(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, ClothDbContext clothDbContext)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<SignUpResponse> SignUp(SignUp signUp)
        {
            var userData = await userManager.FindByEmailAsync(signUp.Email);
            var username = await userManager.FindByNameAsync(signUp.Name);
            if (userData != null)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = "A user with this email already exists."
                };
            }
            if (username != null)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = "Username already exists."
                };
            }
            if (signUp.Password != signUp.ConfirmPassword)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = "Both Password and ConfirmPassword should be the same."
                };
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
                        return new SignUpResponse
                        {
                            Success = false,
                            Message = "An Admin already exists"
                        };
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, "Admin");
                        return new SignUpResponse
                        {
                            Success = true,
                            Message = "An Admin created successfully"
                        };
                    }
                }
                await userManager.AddToRoleAsync(user, "User");
                return new SignUpResponse
                {
                    Success = true,
                    Message = "User Registration Successful"
                };
            }
            else
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = $"Registration failed: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                };
            }
        }

        //public async Task<(string, string, string)> Login(Login login)
        //{
        //    var userData = await userManager.FindByEmailAsync(login.Email);
        //    if (userData == null)
        //    {
        //        return (null, null, "No User found with this email address.");
        //    }
        //    var user = await signInManager.PasswordSignInAsync(userData.UserName, login.Password, false, false);
        //    if (user.Succeeded)
        //    {
        //        JwtToken jwt = new JwtToken(configuration, clothDbContext);
        //        var roles =await userManager.GetRolesAsync(userData);
        //        var tokenValue = jwt.CreateToken(userData, roles);
        //        var refreshToken = "hlw";

        //        return (tokenValue, refreshToken, null);
        //    }
        //    else
        //    {
        //        return (null, null, "incorrect password.");
        //    }
        //}
    }
}
