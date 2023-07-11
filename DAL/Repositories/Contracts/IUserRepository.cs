using BOL.IDENTITY;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<ApplicationUser> UpdateUser(ApplicationUser user);
        Task DeleteUserByPhoneNumberOrEmail(ApplicationUser user);
        Task<ApplicationUser> GetUserByMobileNo(string mobileNo);
        Task<ApplicationUser> GetRegisterdUserByMobileNoOrEmail(string mobileNoOrEmail);
        Task<ApplicationUser> GetUserByUserName(string userName);
        Task<bool> VerifyOTP(string phoneNumber, string otp);
    }
}
