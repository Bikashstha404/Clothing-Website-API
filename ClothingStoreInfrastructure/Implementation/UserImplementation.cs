using ClothingStoreApplication.Interface;
using ClothingStoreDomain.Models;
using ClothingStoreInfrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreInfrastructure.Implementation
{
    public class UserImplementation : IUser
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserImplementation(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = new List<UserModel>();
            var applicationUsers = await userManager.Users.ToListAsync();
            foreach(var applicationUser in applicationUsers)
            {
                var role = await userManager.GetRolesAsync(applicationUser);
                UserModel user = new UserModel
                {
                    Id = applicationUser.Id,
                    Name = applicationUser.UserName,
                    Email = applicationUser.Email,
                    Gender = applicationUser.Gender,
                    Role = role.ToList()
                };
                users.Add(user);
            }
            return users;
        }
    }
}
