using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreInfrastructure.Implementation
{
    public class SignUpImplementation : ISignUp
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public SignUpImplementation(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<bool> Login(string email, string password)
        {
            var userData = await userManager.FindByEmailAsync(email);
            if (userData == null) 
            {
                return false;
            }
            var user = await signInManager.PasswordSignInAsync(userData.UserName, password, false, false);
            if (user.Succeeded) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SignUp(SignUp signUp)
        {
            var existingUser = await userManager.FindByEmailAsync(signUp.Email);
            if(existingUser != null)
            {
                return false;
            }
            if(signUp.ConfirmPassword != signUp.Password)
            {
                return false;
            }
            
            IdentityUser user = new IdentityUser
            {
                UserName = signUp.Name,
                Email = signUp.Email,
                PasswordHash = signUp.Password,
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

       
    }
}
