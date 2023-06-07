using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ADMIN.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ADMIN.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly ILogger<AdministratorController> _logger;

        public AdministratorController(ILogger<AdministratorController> logger)
        {
            _logger = logger;
        }

        [Authorize(Policy = "Admin-Dashboard-Realtime-View")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [Route("/MIM/Staff/Index")]
        public IActionResult Staff()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ViewBag.Exception = context.Error.InnerException;

            // Shafi: Remove TempData["Deleted"] & TempData["SuccessMessage"]
            // Add this becuase when tried to delete cascade record it shows error
            // but both of these temp data get generated during the delete record process
            // when user navigate to any Index view then a popup appear which says
            // successfully deleted or created records message
            // so, to distroy this popup, I removed temp data inside the Error() action result.
            TempData.Remove("Deleted");
            TempData.Remove("SuccessMessage");
            // End:

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
