using BAL;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Auth;
using BOL.SHARED;
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

        private UserRegisterVM UserRegisterVM { get; set; } = new UserRegisterVM();
        public ErrorResponse errorResponse { get; set; } = new ErrorResponse();

        public string message;
        public bool isError { get; set; }
        public bool isOtpGenerated;
        public bool isTCAccepted { get; set; }

        public async Task VerifyOTP()
        {
            if (await userService.IsOTPVerifiedAndRegComplete(UserRegisterVM))
            {
                Guid key = Guid.NewGuid();
                errorResponse = await userService.SignIn(UserRegisterVM.Email, UserRegisterVM.Password, false, key);
                UserRegisterVM.Password = string.Empty;
                if (errorResponse.StatusCode == Constants.Success)
                    navManager.NavigateTo($"{errorResponse.RedirectToUrl}?key={key}", true);
            }
            else
            {
                isError = true;
                message = "Invalid OTP!";
            }
        }

        public async void RegisterUser()
        {
            UserRegisterVM.EmailErrMessage = FieldValidator.emailErrMessage(UserRegisterVM.Email);
            UserRegisterVM.MobileErrMessage = FieldValidator.mobileErrMessage(UserRegisterVM.Mobile);
            UserRegisterVM.PasswordErrMessage = FieldValidator.passwordErrorMessage(UserRegisterVM.Password);
            UserRegisterVM.ConfirmPasswordErrMessage = FieldValidator.confirmPasswordErrMessage(UserRegisterVM.Password, UserRegisterVM.ConfirmPassword);

            if (!string.IsNullOrEmpty(UserRegisterVM.EmailErrMessage) || !string.IsNullOrEmpty(UserRegisterVM.MobileErrMessage)
                || !string.IsNullOrEmpty(UserRegisterVM.PasswordErrMessage) || !string.IsNullOrEmpty(UserRegisterVM.ConfirmPasswordErrMessage))
            {
                return;
            }

            var userExist = await userService.GetUserByMobileNoOrEmail(UserRegisterVM.Mobile);
            if(userExist != null)
                message = $"Mobile number is already taken.";
            else
            {
                IdentityResult result = await userService.Register(UserRegisterVM);
                if (result.Succeeded)
                {
                    isOtpGenerated = true;
                    message = $"Otp sent on your mobile number XXXXXX{UserRegisterVM.Mobile.Substring(6)}.";
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
