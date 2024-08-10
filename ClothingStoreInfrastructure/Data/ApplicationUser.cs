using ClothingStoreDomain.Enums;
using Microsoft.AspNetCore.Identity;

namespace ClothingStoreInfrastructure.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Gender Gender { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordExpiryTime { get; set; }

    }
}
