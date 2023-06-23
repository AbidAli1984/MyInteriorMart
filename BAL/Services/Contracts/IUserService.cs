using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> GenerateOTP(string mobileNumber);
        Task<bool> VerifyOTP(string phoneNumber, string otp);
    }
}
