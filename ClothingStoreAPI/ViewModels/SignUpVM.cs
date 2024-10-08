﻿using ClothingStoreDomain.Entities;
using ClothingStoreDomain.Enums;

namespace ClothingStoreAPI.ViewModels
{
    public class SignUpVM
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
