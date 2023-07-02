using BAL.Services.Contracts;
using BOL.IDENTITY;
using DAL.Models;
using FRONTEND.BLAZOR.Middleware;
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
        [Inject]
        ISuspendedUserService _suspendedUserService { get; set; }

        [Inject]
        ILogger<Login> _logger { get; set; }

        [Inject]
        UserManager<ApplicationUser> _userManager { get; set; }

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

            returnUrl = returnUrl ?? "/MyAccount/UserProfile";

            try
            {
                errorMessage = await userService.SignIn(_userManager, _signInManager, Email, Password, RememberMe);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    Guid key = Guid.NewGuid();
                    BlazorCookieLoginMiddleware.Logins[key] = new LoginInfo { Email = Email, Password = Password };
                    navManager.NavigateTo($"/login?key={key}", true);
                    navManager.NavigateTo($"/MyAccount/UserProfile", true);
                }




                //var usr = await _userManager.FindByEmailAsync(Email);
                //if (usr == null)
                //{
                //    errorMessage = "User not found";
                //    return;
                //}


                //if (await _signInManager.CanSignInAsync(usr))
                //{
                //    var result = await _signInManager.CheckPasswordSignInAsync(usr, Password, true);
                //    if (result == SignInResult.Success)
                //    {
                //        Guid key = Guid.NewGuid();
                //        BlazorCookieLoginMiddleware.Logins[key] = new LoginInfo { Email = Email, Password = Password };
                //        navManager.NavigateTo($"/login?key={key}", true);
                //    }
                //    else
                //    {
                //        errorMessage = "Login failed. Check your password.";
                //    }
                //}
                //else
                //{
                //    errorMessage = "Your account is blocked";
                //}
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                int test = 1;
                throw;
            }
        }
    }
}