using ClothingStoreAPI.ViewModels;
using ClothingStoreDomain.Entities;

namespace ClothingStoreAPI.Mapper
{
    public interface IAuthMapper
    {
        SignUp Register(SignUpVM signUp);
        Login Login(LoginVM login);
    }
}
