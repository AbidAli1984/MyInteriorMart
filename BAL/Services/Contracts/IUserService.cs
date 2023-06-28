using BOL.IDENTITY;
using BOL.IDENTITY.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IUserService
    {
        Task<ApplicationUser> GetUserByUserNameOrEmail(string userName);
        Task<bool> IsMobileNoAlreadyRegistered(string mobileNumber);
        Task<IdentityResult> Register(UserRegisterViewModel user);
        Task<SignInResult> SignIn(string email, string password, bool rememberMe);
        Task<bool> GenerateOTP(string mobileNumber);
        Task<bool> VerifyOTP(string phoneNumber, string otp);
        Task<UserProfile> GetProfileByOwnerGuid(string ownerGuid);
        string GetUserEmailById(string userGuid);
    }
}
