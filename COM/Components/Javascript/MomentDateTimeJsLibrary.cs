using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COM.Components.Javascript
{
    public class MomentDateTimeJsLibrary : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
