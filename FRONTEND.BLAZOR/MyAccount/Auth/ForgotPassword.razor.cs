using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Auth;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace FRONTEND.BLAZOR.MyAccount.Auth
{
    public partial class ForgotPassword
    {
        [Inject]
        private IUserService userService { get; set; }

        [Inject]
        NavigationManager navManager { get; set; }
        [Inject]
        Helper helper { get; set; }

        private UserRegisterVM UserRegisterVM { get; set; }

        public string message;
        public bool isError { get; set; }
        public bool isOtpGenerated;

        protected async override Task OnInitializedAsync()
        {
            UserRegisterVM = new UserRegisterVM();
        }

        public async Task VerifyAndUpdatePassword()
        {
            UserRegisterVM.NewPasswordErrMessage = FieldValidator.passwordErrorMessage(UserRegisterVM.NewPassword);
            UserRegisterVM.ConfirmPasswordErrMessage = FieldValidator.confirmPasswordErrMessage(UserRegisterVM.NewPassword, UserRegisterVM.ConfirmPassword);

            if (!string.IsNullOrEmpty(UserRegisterVM.NewPasswordErrMessage) || !string.IsNullOrEmpty(UserRegisterVM.ConfirmPasswordErrMessage))
            {
                return;
            }

            if (await userService.IsVerifiedAndPasswordChanged(UserRegisterVM))
            {
                navManager.NavigateTo("/Auth/Login");
                await helper.ShowNotification(_notice, AntDesign.NotificationType.Success, AntDesign.NotificationPlacement.BottomRight,
                    "Confirmation", "Password Change Successfully!");
            }
            else
            {
                isError = true;
                message = "Invalid OTP!";
            }
            StateHasChanged();
        }

        public async void GenerateOTP()
        {
            isOtpGenerated = await userService.IsOTUpdated(UserRegisterVM);
            if (isOtpGenerated)
            {
                StateHasChanged();
                await helper.ShowNotification(_notice, AntDesign.NotificationType.Info, AntDesign.NotificationPlacement.BottomRight,
                    "Confirmation", "Otp Sent Successfully to registered Mobile");
            }
            else
                UserRegisterVM.EmailErrMessage = $"No account found with given Email or Mobile No.";
            StateHasChanged();
        }
    }
}
