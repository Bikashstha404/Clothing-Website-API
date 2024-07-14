using ClothingStoreDomain;
using Microsoft.AspNetCore.Identity;

namespace ClothingStoreInfrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Gender Gender { get; set; }
    }
}
