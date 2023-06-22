using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.Auth
{
    public partial class Login
    {
        // Begin: Check if record exisit with listingId
        public string currentPage = "nav-address";

        public string phoneNumber { get; set; }
        public string otp { get; set; }
        public bool isOtpGenerated = false;

        public async Task GenerateOTP()
        {
            isOtpGenerated = await userService.GenerateOTP(phoneNumber);
        }

        public async Task VerifyOTP()
        {
            bool isUserVerified = await userService.VerifyOTP(phoneNumber, otp);

            if (isUserVerified)
            {
                navManager.NavigateTo("/");
            }
        }
    }
}