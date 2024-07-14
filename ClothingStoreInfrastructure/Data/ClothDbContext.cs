using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreInfrastructure.Data
{
    public class ClothDbContext : IdentityDbContext<ApplicationUser>
    {
        public ClothDbContext(DbContextOptions<ClothDbContext> options) : base(options)
        {

        }

    }
}
