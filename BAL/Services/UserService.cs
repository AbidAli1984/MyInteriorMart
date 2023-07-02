using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DAL.Repositories.Contracts;
using BAL.Services.Contracts;
using BOL.IDENTITY;
using Utils;
using BOL.IDENTITY.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BAL.Messaging.Contracts;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public INotificationService _notificationService;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager,
            INotificationService notificationService, SignInManager<ApplicationUser> signInManager)
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

        public async Task<ApplicationUser> GetUserByUserNameOrEmail(string userNameOrEmail)
        {
            if (string.IsNullOrEmpty(userNameOrEmail))
                return null;
            return await _userRepository.GetUserByUserNameOrEmail(userNameOrEmail);
        }

        public async Task<ApplicationUser> GetUserByMobileNumber(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
                return null;
            return await _userRepository.GetRegisterdUserByMobileNo(mobileNumber);
        }

        public async Task<bool> IsMobileNoAlreadyRegistered(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
                return false;
            var registeredUser = await GetUserByMobileNumber(mobileNumber);
            return registeredUser != null;
        }

        public async Task<IdentityResult> Register(UserRegisterViewModel userRegisterViewModel)
        {
            var user = new ApplicationUser
            {
                UserName = userRegisterViewModel.Email,
                Email = userRegisterViewModel.Email,
                PhoneNumber = userRegisterViewModel.Mobile,
                IsVendor = userRegisterViewModel.isVendor,
                Otp = Helper.GetOTP(),
                PhoneNumberConfirmed = false
            };
            var result = await _userManager.CreateAsync(user, userRegisterViewModel.Password);
            //if (result.Succeeded)
            //    _notificationService.SendSMS(userRegisterViewModel.Mobile, user.Otp);
            return result;
        }

        public async Task<bool> VerifyOTP(string mobileNumber, string otp)
        {
            ApplicationUser userVerified = await _userRepository.GetUserByMobileNo(mobileNumber);
            bool isVerified = userVerified.Otp == otp;
            if (isVerified)
            {
                userVerified.Otp = null;
                userVerified.IsRegistrationCompleted = true;
                userVerified.PhoneNumberConfirmed = true;
                await _userRepository.UpdateUser(userVerified);
            }
            return isVerified;
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

        public async Task<string> SignIn(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            string email, string password, bool rememberMe = false)
        {
            var usr = await _userManager.FindByEmailAsync(email);
            if (usr == null)
            {
                return "User not found";
            }


            if (await _signInManager.CanSignInAsync(usr))
            {
                var result = await _signInManager.CheckPasswordSignInAsync(usr, password, true);
                if (result == SignInResult.Success)
                {
                    return string.Empty;
                    //Guid key = Guid.NewGuid();
                    //BlazorCookieLoginMiddleware.Logins[key] = new LoginInfo { Email = Email, Password = Password };
                    //navManager.NavigateTo($"/login?key={key}", true);
                }
                else
                {
                    return "Login failed. Check your password.";
                }
            }
            else
            {
                return "Your account is blocked";
            }
        }

        public async Task<SignInResult> SignIn(ApplicationUser user, string password, bool rememberMe = false)
        {
            return await _signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: true);
        }
    }
}
