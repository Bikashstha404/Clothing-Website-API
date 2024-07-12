using ClothingStoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreApplication.Interface
{
    public interface ISignUp
    {
        Task<bool> SignUp(SignUp signUp);

        Task<bool> Login(string email, string password);
    }
}
