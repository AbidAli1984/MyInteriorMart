using Microsoft.AspNetCore.Mvc;

namespace COM.Components.Listings
{
    public class Rating : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}