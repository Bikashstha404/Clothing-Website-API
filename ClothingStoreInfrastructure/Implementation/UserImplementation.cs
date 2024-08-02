using ClothingStoreApplication.Interface;
using ClothingStoreDomain;
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

        public async Task<List<User>> GetAllUsers()
        {
            var users = new List<User>();
            var applicationUsers = await userManager.Users.ToListAsync();
            foreach(var applicationUser in applicationUsers)
            {
                User user = new User
                {
                    Id = applicationUser.Id,
                    Name = applicationUser.UserName,
                    Email = applicationUser.Email,
                    Gender = applicationUser.Gender
                };
                users.Add(user);
            }
            return users;
        }
    }
}
