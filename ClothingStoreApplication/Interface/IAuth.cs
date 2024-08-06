﻿using ClothingStoreApplication.Response;
using ClothingStoreDomain;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreApplication.Interface
{
    public interface IAuth
    {
        Task<SignUpResponse> SignUp(SignUp signUp);
        Task<LoginResponse> Login(Login login);
        Task<LoginResponse> RefreshToken(String accesstoken, String refreshToken);
    }
}
