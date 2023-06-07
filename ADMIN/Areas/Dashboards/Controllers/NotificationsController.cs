using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ADMIN.Areas.Dashboards.Controllers
{
    [Area("Dashboards")]
    [Authorize(Policy = "Admin-Dashboard-Notifications-View")]
    public class NotificationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
