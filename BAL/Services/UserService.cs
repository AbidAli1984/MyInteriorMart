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
            userRegisterVM.OTP = user.Otp;
            var result = await _userManager.CreateAsync(user, userRegisterVM.Password);
            //if (result.Succeeded)
            //    _notificationService.SendSMS(userRegisterViewModel.Mobile, user.Otp);
            return result;
        }

        public async Task<bool> IsOTPVerifiedAndRegComplete(UserRegisterVM userRegisterVM)
        {
            userRegisterVM.ConfirmPassword = string.Empty;
            userRegisterVM.OTP = string.Empty;

            ApplicationUser userVerified = await _userRepository.GetUserByMobileNo(userRegisterVM.Mobile);
            if (userVerified.Otp == userRegisterVM.ConfOTP)
            {
                userVerified.Otp = null;
                userVerified.IsRegistrationCompleted = true;
                userVerified.PhoneNumberConfirmed = true;
                //userVerified.LockoutEnabled = false;
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
                return "Login failed. Check your password.";
            }
            return "Your account is blocked";
        }

        public async Task<SignInResult> SignIn(ApplicationUser user, string password, bool rememberMe = false)
        {
            return await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: true);
        }
    }
}
