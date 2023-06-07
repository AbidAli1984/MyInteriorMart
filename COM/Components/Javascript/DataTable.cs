using Microsoft.AspNetCore.Mvc;

namespace COM.Components.Javascript
{
    public class DataTable : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}