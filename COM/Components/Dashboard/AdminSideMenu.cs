using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COM.Components.Dashboard
{
    public class AdminSideMenu : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminSideMenu(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            ViewBag.UserID = user.Id;
            return View();
        }
    }
}
