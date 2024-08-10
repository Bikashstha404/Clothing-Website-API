using ClothingStoreApplication.Response;
using ClothingStoreDomain.Entities;
using ClothingStoreDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreApplication.Interface
{
    public interface IAuth
    {
        Task<SignUpResponse> SignUp(SignUp signUp);
        Task<LoginResponse> Login(Login login);
        Task<LoginResponse> RefreshToken(String accesstoken, String refreshToken);
        Task<SendEmailResponse> SendEmailModel(String email);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordModel resetPasswordModel);
    }
}
