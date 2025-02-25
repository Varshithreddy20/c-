﻿using CRAVENEST.Model;

namespace CRAVENEST.Repository.Interface
{
    public interface IUserRepository
    {
        int SignUp(User user); // Registers a new user
        (bool, string, Login) Login(string email, string password); // Logs in a user
        int UpdateSignup(UpdateProfileModel user); // Updates user profile and optionally password
        bool ConfirmPassword(string email, string password); // Confirms the user's password
    }
}
