using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COM.Components.Dashboard
{
    public class DashboardSideMenu : ViewComponent
    {
        private readonly UserManager<IdentityUser> userManager;
        public DashboardSideMenu(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if(User.Identity.IsAuthenticated == true)
            {
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.OwnerGuid = user.Id;
            }

            return View();
        }
    }
}
