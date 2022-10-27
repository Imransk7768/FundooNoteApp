﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserEntity Registration(UserRegistrationModel userRegistrationModel);

        public string Login(UserLoginModel userLoginModel);

        public string ForgetPassword(string email);
        public UserEntity ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
