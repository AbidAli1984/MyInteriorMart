using BOL.ComponentModels.MyAccount.Auth;
using BOL.SHARED;
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
        Task<bool> IsValidOTP(UserRegisterVM userRegisterVM);
        Task<ApplicationUser> GetUserById(string id);
        Task<IList<string>> GetRolesByUser(ApplicationUser user);
        string GetUserEmailById(string userGuid);
        Task<ErrorResponse> SignIn(string emailOrMobile, string password, bool rememberMe, Guid key);
        #endregion

        #region ForgotOrChangePassword
        Task<bool> IsOTUpdated(UserRegisterVM userRegisterVM);
        Task<bool> IsVerifiedAndPasswordChanged(UserRegisterVM userRegisterVM, bool verifyUsingPassword = false);
        Task<bool> IsOTUpdatedIfMobileOrEmailValidForTheListing(int listingId, UserRegisterVM userRegisterVM);
        #endregion

    }
}
