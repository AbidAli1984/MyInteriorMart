using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DAL.Repositories.Contracts;
using BAL.Services.Contracts;
using BOL.IDENTITY;
using Utils;

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
            return await _userRepository.GetUserByUserName(userName);
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
                    Otp = Helper.GetOTP(),
                    TwoFactorEnabled = true
                };

                ApplicationUser user = await _userRepository.AdOrUpdateUser(userToAddOrUpdate);
                var OTP = await _userManager.GenerateChangePhoneNumberTokenAsync(user, "Email");
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

        public async Task<bool> VerifyOTP(string phoneNumber, string otp)
        {
            ApplicationUser user = await _userRepository.GetUserByMobileNo(phoneNumber);
            return user.Otp == otp;
        }

        public async Task<UserProfile> GetProfileByOwnerGuid(string ownerGuid)
        {
            return await _userRepository.GetProfileByOwnerGuid(ownerGuid);
        }
    }
}
