using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using BAL.Messaging.Notify;
using Microsoft.AspNetCore.Hosting;

namespace IDENTITY.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly INotification notification;
        private readonly IWebHostEnvironment webHost;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender, INotification notification, IWebHostEnvironment webHost)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            this.notification = notification;
            this.webHost = webHost;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                // Shafi: Get newsletter template
                var webRoot = webHost.WebRootPath;
                var notificationTemplate = System.IO.Path.Combine(webRoot, "Email-Templates", "Forgot-Password.html");
                string emailMsg = System.IO.File.ReadAllText(notificationTemplate, Encoding.UTF8).Replace("{ForgotPassword}", HtmlEncoder.Default.Encode(callbackUrl)).Replace("{email}", Input.Email).Replace("{callbackUrl}", callbackUrl);
                // End:

                // Shafi: Send password reset link to email
                notification.SendEmail(Input.Email, "Reset Password", emailMsg);
                // End:

                // Shafi: Send SMS
                if (user.PhoneNumber != null)
                {
                    notification.SendSMS(user.PhoneNumber, $"Dear user, password reset link of www.myinteriormart.com sent to your email {Input.Email}, Please check your email.");
                }

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
