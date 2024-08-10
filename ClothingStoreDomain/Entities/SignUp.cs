using ClothingStoreDomain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ClothingStoreDomain.Entities
{
    public class SignUp
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
