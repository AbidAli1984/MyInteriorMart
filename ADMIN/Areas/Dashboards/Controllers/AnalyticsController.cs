using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADMIN.Areas.Dashboards.Controllers
{
    public class AnalyticsController : Controller
    {
        [Area("Dashboards")]
        [Authorize(Policy = "Admin-Dashboard-Analytics-View")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
