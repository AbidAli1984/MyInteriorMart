using IDENTITY.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> UserManager;
        private readonly ApplicationDbContext ApplicationContext;
        private readonly SignInManager<IdentityUser> SignInManager;
        public LoginController(UserManager<IdentityUser> userManager, ApplicationDbContext applicationContext, SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            ApplicationContext = applicationContext;
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
            var userRecord = await ApplicationContext.Users.Where(i => i.PhoneNumber == mobile).FirstOrDefaultAsync();


            if(userRecord != null)
            {
                var user = await UserManager.FindByNameAsync(userRecord.UserName);
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
