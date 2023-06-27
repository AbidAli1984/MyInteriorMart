using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DAL.Repositories.Contracts;
using BAL.Services.Contracts;
using BOL.IDENTITY;
using Utils;
using BOL.IDENTITY.ViewModels;

namespace BAL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            this._userRepository = userRepository;
            this._userManager = userManager;
            //_emailService = new EmailService();
        }

        public async Task<ApplicationUser> GetUserByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return null;
            return await _userRepository.GetUserByUserName(userName);
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

                ApplicationUser user = await _userRepository.AdOrUpdateUser(userToAddOrUpdate);
                //var OTP = await _userManager.GenerateChangePhoneNumberTokenAsync(user, "Email");
                //var message = new Message(new string[] { user.Email! }, "OTP Confirmation", OTP);
                //_emailService.SendEmail(message);
                //string test = code.Result;
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

        public async Task<UserProfile> GetProfileByOwnerGuid(string ownerGuid)
        {
            return await _userRepository.GetProfileByOwnerGuid(ownerGuid);
        }
    }
}
