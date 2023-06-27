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

        public string errorMessage { get; set; }

        public string otp { get; set; }
        public bool isOtpGenerated { get; set; }
        public bool isOtpVerified { get; set; }

        public async Task GenerateOTP()
        {
            var isUserWithMobileExists = await userService.IsMobileNoAlreadyRegistered(Mobile);
            if(isUserWithMobileExists)
            {
                mobileErrMessage = $"Mobile number {Mobile} is already registered";
                return;
            }

            mobileErrMessage = FieldValidator.mobileErrMessage(Mobile);
            if (!string.IsNullOrEmpty(mobileErrMessage))
            {
                return;
            }

            await userService.GenerateOTP(Mobile);
            isOtpGenerated = true;// await userService.GenerateOTP(phoneNumber);
            mobileErrMessage = "Otp sent successfully! to your mobile number " + Mobile;
        }

        public async Task VerifyOTP()
        {
            isOtpVerified = await userService.VerifyOTP(Mobile, otp);

            mobileErrMessage = isOtpVerified ? "Mobile number verified successfully!" : "Invalid OTP";
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

            var user = new UserRegisterViewModel { Email = Email.ToLower(), Mobile = Mobile, Password = Password };
            IdentityResult result = await userService.Register(user);
            if (result.Succeeded)
            {
                navManager.NavigateTo("/Auth/Login");
            }
            else
            {
                errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
            }
        }
    }
}
