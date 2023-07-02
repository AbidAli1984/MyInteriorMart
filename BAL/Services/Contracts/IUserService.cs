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
        Task<List<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUserByUserNameOrEmail(string userName);
        Task<ApplicationUser> GetUserByMobileNumber(string mobileNumber);
        Task<bool> IsMobileNoAlreadyRegistered(string mobileNumber);
        Task<IdentityResult> Register(UserRegisterViewModel user);
        Task<UserRegisterViewModel> VerifyOTP(string phoneNumber, string otp);
        Task<ApplicationUser> GetUserById(string id);
        Task<IList<string>> GetRolesByUser(ApplicationUser user);
        string GetUserEmailById(string userGuid);
        Task<string> SignIn(string email, string password, bool rememberMe, Guid key);
    }
}
