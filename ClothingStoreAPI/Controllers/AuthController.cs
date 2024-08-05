﻿using ClothingStoreAPI.Dtos;
using ClothingStoreAPI.Mapper;
using ClothingStoreAPI.ViewModels;
using ClothingStoreApplication.Interface;
using ClothingStoreApplication.Response;
using ClothingStoreDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth iAuth;
        private readonly IAuthMapper iAuthMapper;

        public AuthController(IAuth iAuth, IAuthMapper iAuthMapper)
        {
            this.iAuth = iAuth;
            this.iAuthMapper = iAuthMapper;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpVM signUp)
        {
            SignUp signUpData = iAuthMapper.Register(signUp);
            SignUpResponse response = await iAuth.SignUp(signUpData);
            if (response.Success)
            {
                return Ok(new { Message = response.Message});
            }
            else
            {
                return BadRequest(new { Message = response.Message });
            }
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(LoginVM login)
        //{
        //    Login loginData = iAuthMapper.Login(login);
        //    var (accessToken, refreshToken, message) = await iAuth.Login(loginData);
        //    //string message = await messageTask;
        //    if(message == "Incorrect Password." || message == "No User found with this email address.")
        //    {
        //        return BadRequest(new { Message = message });
        //    }
        //    else
        //    {
        //        return Ok(new TokenApiDto()
        //        {
        //            AccessToken = "",
        //            RefreshToken = ""
        //        });
        //    }
        //}
    }
}
