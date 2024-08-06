using ClothingStoreAPI.Dtos;
using ClothingStoreAPI.Mapper;
using ClothingStoreAPI.ViewModels;
using ClothingStoreApplication.Interface;
using ClothingStoreApplication.Response;
using ClothingStoreDomain;
using ClothingStoreInfrastructure.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
                return BadRequest(new { Message = response.Message });
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
                    AccessToken = accessToken,
                    RefreshToken = refreshToken,
                });
            }
            else
            {
                return BadRequest(response.Message);    
            }
        }
    }
}
