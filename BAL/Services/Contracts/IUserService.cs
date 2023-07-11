using BOL.ComponentModels.MyAccount.Auth;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUserByUserName(string userName);
        Task<string> GetUserIdByUserName(string userName);
        Task<ApplicationUser> GetRegisterdUserByMobileNoOrEmail(string mobileNumber);
        Task<bool> IsMobileNoAlreadyRegistered(string mobileNumber);
        Task<IdentityResult> Register(UserRegisterVM user);
        Task<UserRegisterVM> VerifyOTP(string phoneNumber, string otp);
        Task<ApplicationUser> GetUserById(string id);
        Task<IList<string>> GetRolesByUser(ApplicationUser user);
        string GetUserEmailById(string userGuid);
        Task<string> SignIn(string emailOrMobile, string password, bool rememberMe, Guid key);
    }
}
