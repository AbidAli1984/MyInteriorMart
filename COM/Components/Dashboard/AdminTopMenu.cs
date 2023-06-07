using Microsoft.AspNetCore.Mvc;

namespace COM.Components.Dashboard
{
    public class AdminTopMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}