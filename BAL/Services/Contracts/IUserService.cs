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
        #region Users
        Task<List<ApplicationUser>> GetUsers();
        Task<ApplicationUser> GetUserByUserName(string userName);
        Task<string> GetUserIdByUserName(string userName);
        Task<ApplicationUser> GetUserByMobileNoOrEmail(string mobileNumber);
        Task<bool> IsMobileNoAlreadyRegistered(string mobileNumber);
        Task<IdentityResult> Register(UserRegisterVM user);
        Task<bool> IsOTPVerifiedAndRegComplete(UserRegisterVM userRegisterVM);
        Task<ApplicationUser> GetUserById(string id);
        Task<IList<string>> GetRolesByUser(ApplicationUser user);
        string GetUserEmailById(string userGuid);
        Task<string> SignIn(string emailOrMobile, string password, bool rememberMe, Guid key);
        #endregion

        #region ForgotOrChangePassword
        Task<bool> IsOTUpdated(UserRegisterVM userRegisterVM);
        Task<bool> IsVerifiedAndPasswordChanged(UserRegisterVM userRegisterVM, bool verifyUsingPassword = false);
        #endregion

    }
}
