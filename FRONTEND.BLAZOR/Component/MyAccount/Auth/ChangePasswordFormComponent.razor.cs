using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace FRONTEND.BLAZOR.Component.MyAccount.Auth
{
    public partial class ChangePasswordFormComponent
    {
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] private IUserService userService { get; set; }
        [Inject] Helper helper { get; set; }

        private UserRegisterVM UserRegisterVM { get; set; }

        public string message;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                UserRegisterVM = new UserRegisterVM();
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    UserRegisterVM.Email = applicationUser.Email;
                }
            }
            catch (Exception exc)
            {

            }
        }

        public async Task VerifyAndChangePassword()
        {
            UserRegisterVM.PasswordErrMessage = FieldValidator.passwordErrorMessage(UserRegisterVM.Password);
            UserRegisterVM.NewPasswordErrMessage = FieldValidator.passwordErrorMessage(UserRegisterVM.NewPassword);
            UserRegisterVM.ConfirmPasswordErrMessage = FieldValidator.confirmPasswordErrMessage(UserRegisterVM.NewPassword, UserRegisterVM.ConfirmPassword);

            if (!string.IsNullOrEmpty(UserRegisterVM.NewPasswordErrMessage) || !string.IsNullOrEmpty(UserRegisterVM.PasswordErrMessage) ||
                !string.IsNullOrEmpty(UserRegisterVM.ConfirmPasswordErrMessage))
            {
                return;
            }

            if (await userService.IsVerifiedAndPasswordChanged(UserRegisterVM, true))
            {
                UserRegisterVM.Password = string.Empty;
                UserRegisterVM.NewPassword = string.Empty;
                UserRegisterVM.ConfirmPassword = string.Empty;
                message = string.Empty;
                helper.ShowNotification(_notice, "Password Change Successfully!");
            }
            else
            {
                message = "Invalid Current Password!";
            }
            StateHasChanged();
        }
    }
}
