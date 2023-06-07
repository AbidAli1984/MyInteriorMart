using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADMIN.Areas.Dashboards.Controllers
{
    public class UserHistoryController : Controller
    {
        [Area("Dashboards")]
        [Authorize(Policy = "Admin-Dashboard-Users-View")]
        public IActionResult Index(int subtractDays)
        {
            ViewBag.SubtractDays = subtractDays;
            return View();
        }
    }
}
