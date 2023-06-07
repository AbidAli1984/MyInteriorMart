using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COM.Components.Dashboard
{
    public class AdminSideMenu : ViewComponent
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminSideMenu(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
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
