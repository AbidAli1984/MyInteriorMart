using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COM.Components.Theme
{
    public class FilterSideBar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
