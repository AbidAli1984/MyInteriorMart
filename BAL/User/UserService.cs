using BAL.Utils;
using DAL.CustomModel;
using DAL.UserRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.User
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public Task<bool> GenerateOTP(string mobileNumber)
        {
            ApplicationUser user = new ApplicationUser
            {
                PhoneNumber = mobileNumber,
                UserName = mobileNumber,
                Email = mobileNumber + "@gmail.com",
                Otp = Common.GetOTP()
            };

            var code = _userRepository.CreateUser(user);
            string test = code.Result;
            throw new NotImplementedException();
        }
    }
}
