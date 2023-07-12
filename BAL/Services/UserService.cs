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

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public IMessageMailService _notificationService;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager,
            IMessageMailService notificationService, SignInManager<ApplicationUser> signInManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            this._notificationService = notificationService;
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
                IsVendor = userRegisterVM.isVendor,
                Otp = Helper.GetOTP(),
                PhoneNumberConfirmed = false
            };
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

        public async Task<string> SignIn(string emailOrMobile, string password, bool rememberMe, Guid key)
        {
            var usr = await _userRepository.GetUserByMobileNoOrEmail(emailOrMobile);
            if (usr == null)
            {
                return "User not found";
            }
                
            if (await _signInManager.CanSignInAsync(usr))
            {
                var result = await _signInManager.CheckPasswordSignInAsync(usr, password, true);
                if (result == SignInResult.Success)
                {
                    BlazorCookieLoginMiddleware.Logins[key] = new LoginInfo { Email = usr.Email, Password = password };
                    return string.Empty;
                }
                return "Invalid details please check your Email ID or Password";
            }
            return "Your account is blocked";
        }
        #endregion

        #region ForgotOrChangePassword
        public async Task<bool> IsOTUpdated(UserRegisterVM userRegisterVM)
        {
            var userToUpdate = await GetUserByMobileNoOrEmail(userRegisterVM.Email);
            if (userToUpdate == null)
                return false;

            userToUpdate.Otp = Helper.GetOTP();
            await _userRepository.UpdateUser(userToUpdate);
            //_notificationService.SendSMS(userRegisterViewModel.Mobile, user.Otp);
            return true;
        }

        public async Task<bool> IsVerifiedAndPasswordChanged(UserRegisterVM userRegisterVM, bool verifyUsingPassword)
        {
            ApplicationUser userToVerify = await _userRepository.GetUserByMobileNoOrEmail(userRegisterVM.Email);

            if (verifyUsingPassword)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(userToVerify, userRegisterVM.Password, true);
                if (result == SignInResult.Success)
                {
                    userToVerify.PasswordHash = _userManager.PasswordHasher.HashPassword(userToVerify, userRegisterVM.Password);
                    userToVerify.Otp = null;
                    await _userRepository.UpdateUser(userToVerify);
                    return true;
                }
                return false;
            }


            if (userToVerify.Otp == userRegisterVM.UserOtp)
            {
                userToVerify.PasswordHash = _userManager.PasswordHasher.HashPassword(userToVerify, userRegisterVM.Password);
                userToVerify.Otp = null;
                await _userRepository.UpdateUser(userToVerify);
                //var result = await _userManager.ResetPasswordAsync(userVerified, otp, "Abid@1111");
                return true;
            }
            return false;
        }
        #endregion
    }
}
