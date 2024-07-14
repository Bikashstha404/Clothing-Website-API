using System.ComponentModel.DataAnnotations;

namespace ClothingStoreDomain
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
    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2,
    }
}
