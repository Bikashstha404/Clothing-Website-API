using ClothingStoreAPI.ViewModels;
using ClothingStoreDomain;

namespace ClothingStoreAPI.Mapper
{
    public interface IAuthMapper
    {
        SignUp Register(SignUpVM signUp);
        Login Login(LoginVM login);
    }
}
