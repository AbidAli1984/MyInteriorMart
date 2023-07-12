using BAL.Services.Contracts;
using BOL.IDENTITY;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Utils;

namespace FRONTEND.BLAZOR.MyAccount.Auth
{
    public partial class Login
    {
        public string EmailOrMobile { get; set; }
        public string emailErrMessage { get; set; }

        public string Password { get; set; }
        public string passwordErrMessage { get; set; }

        public string errorMessage { get; set; }

        public bool RememberMe { get; set; }

        public async Task LoginUser(string returnUrl = null)
        {
            emailErrMessage = FieldValidator.requiredFieldMessage(EmailOrMobile, "email address or mobile no.");
            passwordErrMessage = FieldValidator.requiredFieldMessage(Password, "password.");

            if (!string.IsNullOrEmpty(emailErrMessage) || !string.IsNullOrEmpty(passwordErrMessage))
            {
                return;
            }

            returnUrl = returnUrl ?? "/MyAccount/UserProfile";

            Guid key = Guid.NewGuid();
            errorMessage = await userService.SignIn(EmailOrMobile, Password, RememberMe, key);
            if (string.IsNullOrEmpty(errorMessage))
                navManager.NavigateTo($"{returnUrl}?key={key}", true);

            StateHasChanged();
        }
    }
}