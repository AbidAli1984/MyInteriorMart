using BAL;
using BAL.Services.Contracts;
using BOL.IDENTITY;
using BOL.SHARED;
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

        public ErrorResponse errorResponse { get; set; } = new ErrorResponse();

        public bool RememberMe { get; set; }

        public async Task LoginUser(string returnUrl = null)
        {
            emailErrMessage = FieldValidator.requiredFieldMessage(EmailOrMobile, "email address or mobile no.");
            passwordErrMessage = FieldValidator.requiredFieldMessage(Password, "password.");

            if (!string.IsNullOrEmpty(emailErrMessage) || !string.IsNullOrEmpty(passwordErrMessage))
            {
                return;
            }

            Guid key = Guid.NewGuid();
            errorResponse = await userService.SignIn(EmailOrMobile, Password, RememberMe, key);
            errorResponse.Message = errorResponse.Message;
            returnUrl = returnUrl ?? errorResponse.RedirectToUrl;
            if (errorResponse.StatusCode == Constants.Success)
                navManager.NavigateTo($"{returnUrl}?key={key}", true);

            StateHasChanged();
        }
    }
}