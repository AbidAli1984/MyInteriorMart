using BAL.Services.Contracts;
using BOL.IDENTITY.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace FRONTEND.BLAZOR.MyAccount.Auth
{
    public partial class Register
    {
        [Inject]
        private IUserService userService { get; set; }

        [Inject]
        NavigationManager navManager { get; set; }

        public string Email { get; set; }
        public string emailErrMessage { get; set; }

        public string Mobile { get; set; }
        public string mobileErrMessage { get; set; }

        public string Password { get; set; }
        public string passwordErrMessage { get; set; }

        public string ConfirmPassword { get; set; }
        public string confirmPasswordErrMessage { get; set; }

        public string message { get; set; }
        public bool isError { get; set; }

        public string otp { get; set; }
        public bool isOtpGenerated { get; set; }
        public bool isVendor { get; set; }
        public bool isTCAccepted { get; set; }

        public async Task VerifyOTP()
        {
            bool isVerified = await userService.VerifyOTP(Mobile, otp);
            if (isVerified)
            {
                //navigate to profile page
                navManager.NavigateTo("/MyAccount/UserProfileEdit");
            }
            else
            {
                isError = true;
                message = "Invalid OTP!";
            }
        }

        public async void RegisterUser()
        {
            emailErrMessage = FieldValidator.emailErrMessage(Email);
            mobileErrMessage = FieldValidator.mobileErrMessage(Mobile);
            passwordErrMessage = FieldValidator.passwordErrorMessage(Password);
            confirmPasswordErrMessage = FieldValidator.confirmPasswordErrMessage(Password, ConfirmPassword);

            if (!string.IsNullOrEmpty(emailErrMessage) || !string.IsNullOrEmpty(mobileErrMessage)
                || !string.IsNullOrEmpty(passwordErrMessage) || !string.IsNullOrEmpty(confirmPasswordErrMessage))
            {
                return;
            }

            var user = new UserRegisterViewModel { Email = Email.ToLower(), Mobile = Mobile, Password = Password, isVendor = isVendor };
            isOtpGenerated = true;
            message = $"Otp sent on your mobile number XXXXXX{Mobile.Substring(6)}.";
            IdentityResult result = await userService.Register(user);
        }
    }
}
