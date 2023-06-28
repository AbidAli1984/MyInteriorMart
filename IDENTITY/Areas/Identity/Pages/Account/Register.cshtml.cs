using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using BAL.Messaging;
using BAL.Messaging.Notify;
using Microsoft.AspNetCore.Hosting;
using IDENTITY.Services;
using BAL.Services.Contracts;
using DAL.Models;

namespace IDENTITY.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly INotification notification;
        private IWebHostEnvironment webHost;
        private readonly IUserService _userService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, INotification notification,
            IWebHostEnvironment webHost,
            IUserService usersService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.notification = notification;
            this.webHost = webHost;
            _userService = usersService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [Phone]
            [Display(Name = "Phone")]
            public string Phone { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email, PhoneNumber = Input.Phone };
                if (await _userService.IsMobileNoAlreadyRegistered(Input.Phone))
                {
                    TempData["ErrorMessage"] = $"This number already registered.";
                    ModelState.AddModelError(string.Empty, TempData["ErrorMessage"].ToString());
                }
                else
                {
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        if (await _userService.IsMobileNoAlreadyRegistered(Input.Phone) != false)
                        {
                            _logger.LogInformation("User created a new account with password.");

                            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            var callbackUrl = Url.Page(
                                "/Account/ConfirmEmail",
                                pageHandler: null,
                                values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                                protocol: Request.Scheme);

                            // Shafi: Get newsletter template
                            var webRoot = webHost.WebRootPath;
                            var notificationTemplate = System.IO.Path.Combine(webRoot, "Email-Templates", "Account-Confirmation.html");
                            string emailMsg = System.IO.File.ReadAllText(notificationTemplate, Encoding.UTF8).Replace("{callbackUrl}", HtmlEncoder.Default.Encode(callbackUrl)).Replace("{email}", Input.Email).Replace("{callbackUrl}", callbackUrl);
                            // End:

                            // Shafi: Send welcome email to user after registration
                            var messageDetails = await notification.MessagesDetailsAsync("User-Registration");
                            notification.SendEmail(user.UserName, messageDetails.EmailSubject, emailMsg);
                            // End:

                            // Shafi: Send email confirmation link to user
                            notification.SendEmail(Input.Email, "Email Confirmation Link", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here.<br/><br/>Visit www.myinteriormart.com</a>.");
                            // End:

                            if (_userManager.Options.SignIn.RequireConfirmedAccount)
                            {
                                return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                            }
                            else
                            {
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                return LocalRedirect("~/Profile/UserProfile/Create");
                            }
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
