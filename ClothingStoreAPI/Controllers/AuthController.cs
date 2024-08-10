using ClothingStoreAPI.Dtos;
using ClothingStoreAPI.Mapper;
using ClothingStoreAPI.ViewModels;
using ClothingStoreApplication.Interface;
using ClothingStoreApplication.Response;
using ClothingStoreDomain.Entities;
using ClothingStoreDomain.Models;
using ClothingStoreInfrastructure.Data;
using ClothingStoreInfrastructure.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ClothingStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuth iAuth;
        private readonly IAuthMapper iAuthMapper;
        private readonly IEmailService iEmailService;

        public AuthController(UserManager<ApplicationUser> userManager, IAuth iAuth, IAuthMapper iAuthMapper, IEmailService iEmailService)
        {
            this.userManager = userManager;
            this.iAuth = iAuth;
            this.iAuthMapper = iAuthMapper;
            this.iEmailService = iEmailService;
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM login)
        {
            Login loginData = iAuthMapper.Login(login);
            LoginResponse response = await iAuth.Login(loginData);
            if(response.Success)
            {
                return Ok(new TokenApiDto()
                {
                    AccessToken = response.AccessToken,
                    RefreshToken = response.RefreshToken
                });
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(TokenApiDto tokenApiDto)
        {
            if (tokenApiDto is null)
            {
                return BadRequest("Invalid Client Request");
            }
            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;

            var response = await iAuth.RefreshToken(accessToken, refreshToken);
            if (response.Success)
            {
                return Ok(new TokenApiDto
                {
                    AccessToken = response.AccessToken,
                    RefreshToken = response.RefreshToken,
                });
            }
            else
            {
                return BadRequest(response.Message);    
            }
        }

        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var response = await iAuth.SendEmailModel(email);
            EmailModel emailModel;
            if (response.Success)
            {
                emailModel = response.EmailModel;
            }
            else
            {
                return BadRequest(response.Message);
            }

            iEmailService.SendEmail(emailModel);

            return Ok(new
            {
                StatusCode = 200,
                Message = "Email Sent!"
            });

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            ResetPasswordResponse response = await iAuth.ResetPassword(resetPasswordModel);
            if (response.Success)
            {
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Password Reset Successfully."
                });
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
