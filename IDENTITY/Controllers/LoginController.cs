using BAL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IDENTITY.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUser> SignInManager;
        public LoginController(IUserService userService, SignInManager<ApplicationUser> signInManager)
        {
            this._userService = userService;
            SignInManager = signInManager;
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Email()
        {
            return View();
        }

        public ActionResult Link()
        {
            return View();
        }

        public ActionResult Mobile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ByMobileOtp(string mobile)
        {
            var userRecord = await _userService.GetUserByMobileNoOrEmail(mobile);

            if(userRecord != null)
            {
                var user = await _userService.GetUserByUserName(userRecord.UserName);
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, true);
                    return Redirect("/");
                }
                else
                {
                    return Redirect("www.google.com");
                }
            }
            else
            {
                return Redirect("www.google.com");
            }
            
        }
    }
}
