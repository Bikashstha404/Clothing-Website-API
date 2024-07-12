using ClothingStoreAPI.ViewModels.AuthVM;
using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISignUp iSignUp;

        public AuthController(ISignUp iSignUp)
        {
            this.iSignUp = iSignUp;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromForm] SignUpVM signUp)
        {
            SignUp signUpData = new SignUp
            {
                Name = signUp.Name,
                Email = signUp.Email,
                Password = signUp.Password,
                ConfirmPassword = signUp.ConfirmPassword,
            };
            var result = await iSignUp.SignUp(signUpData);
            if (result)
            {
                return Ok(new { Message = "User Created Succesfully." });
            }
            else
            {
                return BadRequest(new { Message = "Problem Occured during user creation" });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] LoginVM login)
        {
            var result = await iSignUp.Login(login.Email, login.Password);
            if (result)
            {
                return Ok(new { Message = "Login Successful." });
            }
            else
            {
                return BadRequest(new { Message = "Login Failed" });
            }
        }
    }
}
