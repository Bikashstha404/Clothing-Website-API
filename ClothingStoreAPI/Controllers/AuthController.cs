using ClothingStoreAPI.Mapper;
using ClothingStoreAPI.ViewModels;
using ClothingStoreApplication.Interface;
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
            Task<string> messageTask = iAuth.SignUp(signUpData);
            string message = await messageTask;
            if (message == "Registration Successful")
            {
                return Ok(new { Message = "Registration Successful"});
            }
            else
            {
                return BadRequest(new { Message = message });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM login)
        {
            Login loginData = iAuthMapper.Login(login);
            Task<string> messageTask = iAuth.Login(loginData);
            string message = await messageTask;
            if(message == "Login Failed" || message == "No User found with this email address.")
            {
                return BadRequest(new { Message = message });
            }
            else
            {
                return Ok(new { Message = message });
            }
        }

        [Authorize]
        [HttpGet("Hello")]
        public IActionResult Test() {
            return Ok("Hello sir you token is verified");
        }
    }
}
