using Microsoft.AspNetCore.Mvc;

namespace COM.Components.Identity
{
    public class IdentitySideMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}