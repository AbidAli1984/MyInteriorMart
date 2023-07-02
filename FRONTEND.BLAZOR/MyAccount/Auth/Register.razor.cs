using BAL.Services.Contracts;
using BOL.IDENTITY.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
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

        public string message;
        public bool isError { get; set; }

        public string otp { get; set; }
        public bool isOtpGenerated;
        public bool isVendor { get; set; }
        public bool isTCAccepted { get; set; }

        public async Task VerifyOTP()
        {
            UserRegisterViewModel userRegisterViewModel = await userService.VerifyOTP(Mobile, otp);
            if (userRegisterViewModel != null)
            {
                Guid key = Guid.NewGuid();
                string errorMessage = await userService.SignIn(Email, Password, false, key);
                if (string.IsNullOrEmpty(errorMessage))
                    navManager.NavigateTo($"/MyAccount/UserProfile?key={key}", true);
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

            var userExist = await userService.GetUserByMobileNumber(Mobile);
            if(userExist != null)
                message = $"Mobile number is already taken.";
            else
            {
                var user = new UserRegisterViewModel { Email = Email.ToLower(), Mobile = Mobile, Password = Password, isVendor = isVendor };
                IdentityResult result = await userService.Register(user);
                if (result.Succeeded)
                {
                    isOtpGenerated = true;
                    message = $"Otp sent on your mobile number XXXXXX{Mobile.Substring(6)}.";
                }
                else
                {
                    message = result.Errors.ToList()[0].Description;
                }
            }

            StateHasChanged();
        }
    }
}
