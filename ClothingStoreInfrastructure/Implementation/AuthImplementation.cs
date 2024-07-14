using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
using ClothingStoreInfrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreInfrastructure.Implementation
{
    public class AuthImplementation : IAuth
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AuthImplementation(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
                return "Name: " + userData.UserName + " Gender: " + userData.Gender.ToString();
            }
            else
            {
                return "Login Failed";
            }
        }
    }
}
