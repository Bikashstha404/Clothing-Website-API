﻿using ClothingStoreApplication.Interface;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthImplementation(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<SignUpResponse> SignUp(SignUp signUp)
        {
            var userData = await _userManager.FindByEmailAsync(signUp.Email);
            var username = await _userManager.FindByNameAsync(signUp.Name);
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

            var result = await _userManager.CreateAsync(user, signUp.Password);
            if (result.Succeeded)
            {
                if(signUp.Email == _configuration["AdminEmail"])
                {
                    var adminExists = await _userManager.GetUsersInRoleAsync("Admin");
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
                        await _userManager.AddToRoleAsync(user, "Admin");
                        return new SignUpResponse
                        {
                            Success = true,
                            Message = "An Admin created successfully"
                        };
                    }
                }
                await _userManager.AddToRoleAsync(user, "User");
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

        public async Task<LoginResponse> Login(Login login)
        {
            var userData = await _userManager.FindByEmailAsync(login.Email);
            if (userData == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "No User found with this email address."
                };
            }
            var user = await _signInManager.PasswordSignInAsync(userData.UserName, login.Password, false, false);
            if (user.Succeeded)
            {
                JwtToken jwt = new JwtToken(_configuration, _userManager);
                var accessToken = await jwt.CreateToken(userData);
                var refreshToken = jwt.CreateRefreshToken();
                userData.RefreshToken = refreshToken;
                userData.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                var result = await _userManager.UpdateAsync(userData);

                if(result.Succeeded)
                {
                    return new LoginResponse
                    {
                        Success = true,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        Message = "Login Successfull"
                    };
                }
                else
                {
                    return new LoginResponse
                    {
                        Success = false,
                        AccessToken = null,
                        RefreshToken = null,
                        Message = $"Error while updating Data. Error: {string.Join(", ", result.Errors.Select(e => e.Description))}",
                    };
                }
            }
            else
            {
                return new LoginResponse
                {
                    Success = false,
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "Incorrect password."
                };
            }
        }

        public async Task<LoginResponse> RefreshToken(string accessToken, string refreshToken)
        {
            JwtToken jwt = new JwtToken(_configuration, _userManager);
            var principle = jwt.GetPrincipalFromExpiredtoken(accessToken);
            var identity = principle.Identity as ClaimsIdentity;
            string email = "";
            if(identity != null)
            {
                var emailClaim = identity.Claims.FirstOrDefault(c => c.Type == "Email");
                if(emailClaim != null)
                {
                    email = emailClaim.Value;
                }
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new LoginResponse
                {
                    Success = false,
                    AccessToken = null,
                    RefreshToken = null,
                    Message = "Invalid Request."
                };
            }

            var newAccessToken = await jwt.CreateToken(user);
            var newRefreshToken = jwt.CreateRefreshToken();

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new LoginResponse
                {
                    Success = true,
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                    Message = "Token Refreshed Successfully"
                };
            }
            else
            {
                return new LoginResponse
                {
                    Success = false,
                    AccessToken = null,
                    RefreshToken = null,
                    Message = $"Error while updating Data. Error: {string.Join(", ", result.Errors.Select(e => e.Description))}",
                };
            }
        }
    }
}
