using ClothingStoreAPI.Dtos;
using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
using ClothingStoreInfrastructure.Data;
using ClothingStoreInfrastructure.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace ClothingStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _iUser;

        public UserController(IUser iUser)
        {
            _iUser = iUser;
        }

        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _iUser.GetAllUsers();
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
