using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace COM.Components.Dashboard
{
    public class EditListingSideMenu : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
