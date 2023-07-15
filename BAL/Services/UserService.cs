using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DAL.Repositories.Contracts;
using BAL.Services.Contracts;
using BOL.IDENTITY;
using Utils;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BAL.Messaging.Contracts;
using BAL.Middleware;
using BOL.ComponentModels.MyAccount.Auth;
using BOL.SHARED;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IMessageMailService _notificationService;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager,
            IMessageMailService notificationService, SignInManager<ApplicationUser> signInManager,
            IUserProfileRepository userProfileRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _notificationService = notificationService;
            _userProfileRepository = userProfileRepository;
        }

        #region Users
        public async Task<List<ApplicationUser>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
            return await _userRepository.GetUserByUserName(userName);
        }
        public async Task<string> GetUserIdByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return string.Empty;
            ApplicationUser user = await _userRepository.GetUserByUserName(userName);
            return user.Id;
        }

        public async Task<ApplicationUser> GetUserByMobileNoOrEmail(string mobileNumberOrEmail)
        {
            if (string.IsNullOrEmpty(mobileNumberOrEmail))
                return null;
            return await _userRepository.GetUserByMobileNoOrEmail(mobileNumberOrEmail);
        }

        public async Task<bool> IsMobileNoAlreadyRegistered(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
                return false;
            var registeredUser = await _userRepository.GetUserByMobileNoOrEmail(mobileNumber);
            return registeredUser != null;
        }

        public async Task<IdentityResult> Register(UserRegisterVM userRegisterVM)
        {
            var user = new ApplicationUser
            {
                UserName = userRegisterVM.Email,
                Email = userRegisterVM.Email,
                PhoneNumber = userRegisterVM.Mobile,
                IsVendor = userRegisterVM.IsVendor,
                Otp = Helper.GetOTP(),
                PhoneNumberConfirmed = false
            };
            userRegisterVM.DisplayOtp = user.Otp;
            var result = await _userManager.CreateAsync(user, userRegisterVM.Password);
            //if (result.Succeeded)
            //    _notificationService.SendSMS(userRegisterViewModel.Mobile, user.Otp);
            return result;
        }

        public async Task<bool> IsOTPVerifiedAndRegComplete(UserRegisterVM userRegisterVM)
        {
            userRegisterVM.ConfirmPassword = string.Empty;

            ApplicationUser userVerified = await _userRepository.GetUserByMobileNo(userRegisterVM.Mobile);
            if (userVerified.Otp == userRegisterVM.UserOtp)
            {
                userVerified.Otp = null;
                userVerified.IsRegistrationCompleted = true;
                userVerified.PhoneNumberConfirmed = true;
                userVerified.EmailConfirmed = true;
                await _userRepository.UpdateUser(userVerified);
                return true;
            }

            return false;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IList<string>> GetRolesByUser(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public string GetUserEmailById(string userGuid)
        {
            var user = _userManager.FindByIdAsync(userGuid).GetAwaiter().GetResult();
            return user.Email;
        }

        public async Task<ErrorResponse> SignIn(string emailOrMobile, string password, bool rememberMe, Guid key)
        {
            ErrorResponse errorViewModel = new ErrorResponse();
            var usr = await _userRepository.GetUserByMobileNoOrEmail(emailOrMobile);
            if (usr == null)
            {
                errorViewModel.Message = "Invalid Email ID or Password";
            }
            else if (await _signInManager.CanSignInAsync(usr))
            {
                var result = await _signInManager.CheckPasswordSignInAsync(usr, password, false);
                if (result == SignInResult.Success)
                {
                    var userProfile = await _userProfileRepository.GetProfileByOwnerGuid(usr.Id);
                    errorViewModel.RedirectToUrl = userProfile == null ? "/MyAccount/UserProfile" :
                        !userProfile.IsProfileCompleted ? "/MyAccount/UserAddress" : "/";
                    BlazorCookieLoginMiddleware.Logins[key] = new LoginInfo { Email = usr.Email, Password = password };
                }
                else
                    errorViewModel.Message = "Invalid Email ID or Password";
            }
            else
                errorViewModel.Message = "Your account is blocked";

            errorViewModel.StatusCode = string.IsNullOrEmpty(errorViewModel.Message) ? Constants.Success : Constants.Unauthorized;
            return errorViewModel;
        }
        #endregion

        #region ForgotOrChangePassword
        public async Task<bool> IsOTUpdated(UserRegisterVM userRegisterVM)
        {
            var userToUpdate = await GetUserByMobileNoOrEmail(userRegisterVM.Email);
            if (userToUpdate == null)
                return false;

            userToUpdate.Otp = Helper.GetOTP();
            userRegisterVM.DisplayOtp = userToUpdate.Otp;
            await _userRepository.UpdateUser(userToUpdate);
            _notificationService.SendSMS(userToUpdate.PhoneNumber, userToUpdate.Otp);
            return true;
        }

        public async Task<bool> IsVerifiedAndPasswordChanged(UserRegisterVM userRegisterVM, bool verifyUsingPassword)
        {
            ApplicationUser userToVerify = await _userRepository.GetUserByMobileNoOrEmail(userRegisterVM.Email);

            if (verifyUsingPassword)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(userToVerify, userRegisterVM.Password, false);
                return await UpdatePassword(result == SignInResult.Success, userToVerify, userRegisterVM.NewPassword);
            }

            return await UpdatePassword(userToVerify.Otp == userRegisterVM.UserOtp, userToVerify, userRegisterVM.NewPassword);
        }

        private async Task<bool> UpdatePassword(bool isTrue, ApplicationUser userToVerify, string newPassword)
        {
            if (isTrue)
            {
                userToVerify.PasswordHash = _userManager.PasswordHasher.HashPassword(userToVerify, newPassword);
                userToVerify.Otp = null;
                await _userRepository.UpdateUser(userToVerify);
                return true;
            }
            return false;
        }
        #endregion
    }
}
