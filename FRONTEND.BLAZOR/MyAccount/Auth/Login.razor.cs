using BAL.Services.Contracts;
using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace FRONTEND.BLAZOR.MyAccount.Auth
{
    public partial class Login
    {
        [Inject]
        ISuspendedUserService _suspendedUserService { get; set; }

        [Inject]
        ILogger<Login> _logger { get; set; }

        [Inject]
        SignInManager<ApplicationUser> _signInManager { get; set; }

        public string Email { get; set; }
        public string emailErrMessage { get; set; }

        public string Password { get; set; }
        public string passwordErrMessage { get; set; }

        public string errorMessage { get; set; }

        public bool RememberMe { get; set; }


        public async Task LoginUser(string returnUrl = null)
        {
            emailErrMessage = FieldValidator.requiredFieldMessage(Email, "email address.");
            passwordErrMessage = FieldValidator.requiredFieldMessage(Password, "password.");

            if (!string.IsNullOrEmpty(emailErrMessage) || !string.IsNullOrEmpty(passwordErrMessage))
            {
                return;
            }

            returnUrl = returnUrl ?? "/";

            var user = await userService.GetUserByUserNameOrEmail(Email);

            if (user != null)
            {
                if (await _suspendedUserService.IsUserSuspended(user.Id))
                {
                    navManager.NavigateTo("/Home/AccountSuspended");
                }
                else
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(Email, Password, RememberMe, lockoutOnFailure: true);
                    //userService.SignIn(Email, Password, RememberMe);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        navManager.NavigateTo(returnUrl);
                    }
                    else
                    {
                        errorMessage = "Invalid login attempt.";
                    }
                    if (result.RequiresTwoFactor)
                    {
                        navManager.NavigateTo($"./LoginWith2fa?ReturnUrl={returnUrl}&RememberMe={RememberMe}");
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        navManager.NavigateTo("./Lockout");
                    }
                }
            }
            else
            {
                errorMessage = "Invalid login attempt.";
            }
        }
    }
}