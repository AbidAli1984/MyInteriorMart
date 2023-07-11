using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Auth;
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

        private UserRegisterVM UserRegisterVM { get; set; }

        public string message;
        public bool isError { get; set; }
        public bool isOtpGenerated;
        public bool isTCAccepted { get; set; }

        protected async override Task OnInitializedAsync()
        {
            UserRegisterVM = new UserRegisterVM();
        }

        public async Task VerifyOTP()
        {
            if (UserRegisterVM.ConfOTP == UserRegisterVM.OTP)
            {
                Guid key = Guid.NewGuid();
                string errorMessage = await userService.SignIn(UserRegisterVM.Email, UserRegisterVM.Password, false, key);
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
            UserRegisterVM.emailErrMessage = FieldValidator.emailErrMessage(UserRegisterVM.Email);
            UserRegisterVM.mobileErrMessage = FieldValidator.mobileErrMessage(UserRegisterVM.Mobile);
            UserRegisterVM.passwordErrMessage = FieldValidator.passwordErrorMessage(UserRegisterVM.Password);
            UserRegisterVM.confirmPasswordErrMessage = FieldValidator.confirmPasswordErrMessage(UserRegisterVM.Password, UserRegisterVM.ConfirmPassword);

            if (!string.IsNullOrEmpty(UserRegisterVM.emailErrMessage) || !string.IsNullOrEmpty(UserRegisterVM.mobileErrMessage)
                || !string.IsNullOrEmpty(UserRegisterVM.passwordErrMessage) || !string.IsNullOrEmpty(UserRegisterVM.confirmPasswordErrMessage))
            {
                return;
            }

            var userExist = await userService.GetRegisterdUserByMobileNoOrEmail(UserRegisterVM.Mobile);
            if(userExist != null)
                message = $"Mobile number is already taken.";
            else
            {
                var user = new UserRegisterVM { Email = UserRegisterVM.Email.ToLower(), Mobile = UserRegisterVM.Mobile, 
                    Password = UserRegisterVM.Password, isVendor = UserRegisterVM.isVendor };
                IdentityResult result = await userService.Register(user);
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
