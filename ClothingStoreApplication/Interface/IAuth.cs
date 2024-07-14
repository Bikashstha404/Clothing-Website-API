using ClothingStoreDomain;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreApplication.Interface
{
    public interface IAuth
    {
        Task<string> SignUp(SignUp signUp);
        Task<string> Login(Login login);
    }
}
