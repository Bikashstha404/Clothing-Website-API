using ClothingStoreDomain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreInfrastructure.Data
{
    public class ClothDbContext : IdentityDbContext
    {
        public ClothDbContext(DbContextOptions<ClothDbContext> options) : base(options)
        {

        }
        //public DbSet<SignUp> SignUp { get; set; }
    }
}
