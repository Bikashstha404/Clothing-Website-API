using ClothingStoreDomain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreInfrastrucutre.Data
{
    public class ClothDbContext: IdentityDbContext
    {
        public ClothDbContext(DbContextOptions<ClothDbContext> options): base(options)
        {
            
        }
        public DbSet<Cloth> Clothes { get; set; }
    }
}
