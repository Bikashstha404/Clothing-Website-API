using ClothingStoreAPI.ViewModels;
using ClothingStoreDomain.Entities;

namespace ClothingStoreAPI.Mapper
{
    public class AuthMapper : IAuthMapper
    {
        public Login Login(LoginVM login)
        {
            Login user = new Login
            {
                Email = login.Email,
                Password = login.Password,
            };
            return user;
        }

        public SignUp Register(SignUpVM signUp)
        {
            SignUp user = new SignUp
            {
                Name = signUp.Name,
                Gender = signUp.Gender,
                Email = signUp.Email,
                Password = signUp.Password,
                ConfirmPassword = signUp.ConfirmPassword,
            };

            return user;
        }
    }
}
