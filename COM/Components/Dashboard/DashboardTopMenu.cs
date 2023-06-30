using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COM.Components.Dashboard
{
    public class DashboardTopMenu : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        public DashboardTopMenu(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.OwnerGuid = user.Id;
            }

            return View();
        }
    }
}