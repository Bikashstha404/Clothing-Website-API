using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreDomain
{
    public class SignUp
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    //public enum Gender 
    //{
    //    Male,
    //    Female,
    //    Other
    //}
}
