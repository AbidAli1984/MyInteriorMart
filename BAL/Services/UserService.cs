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

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
            this._signInManager = signInManager;
            //_emailService = new EmailService();
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

        public async Task<bool> IsMobileNoAlreadyRegistered(string mobileNumber)
        {
            if (string.IsNullOrEmpty(mobileNumber))
                return false;
            var registeredUser = await _userRepository.GetRegisterdUserByMobileNo(mobileNumber);
            return registeredUser != null;
        }

        public async Task<IdentityResult> Register(UserRegisterViewModel userRegisterViewModel)
        {
            var user = new ApplicationUser { 
                UserName = userRegisterViewModel.Email,
                Email = userRegisterViewModel.Email, 
                PhoneNumber = userRegisterViewModel.Mobile, 
                PhoneNumberConfirmed=true,
                IsRegistrationCompleted = true
            };
            var result = await _userManager.CreateAsync(user, userRegisterViewModel.Password);
            return result;
        }

        public async Task<bool> GenerateOTP(string mobileNumber)
        {
            try
            {
                ApplicationUser userToAddOrUpdate = new ApplicationUser
                {
                    PhoneNumber = mobileNumber,
                    UserName = mobileNumber,
                    Email = "sayyed.abid2003@gmail.com",
                    Otp = Helper.GetOTP()
                };

                ApplicationUser user = await _userRepository.AddOrUpdateUser(userToAddOrUpdate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> VerifyOTP(string mobileNumber, string otp)
        {
            ApplicationUser user = await _userRepository.GetUserByMobileNo(mobileNumber);
            bool isVerified = user.Otp == otp;
            if (isVerified)
            {
                await _userRepository.DeleteUserByPhoneNumberOrEmail(user);
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

        public async Task<SignInResult> SignIn(string email, string password, bool rememberMe = false)
        {
            return await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: true);
        }
    }
}
