using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClothingStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser iUser;

        public UserController(IUser iUser)
        {
            this.iUser = iUser;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await iUser.GetAllUsers();
            var userDtos = users.Select(user => new
            {
                Email = user.Email,
                Name = user.Name,
                Gender = ((Gender)user.Gender).ToString(),
                Role = user.Role

            }).ToList();

            return Ok(userDtos);
        }
    }
}
