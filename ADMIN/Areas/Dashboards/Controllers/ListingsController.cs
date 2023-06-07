using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BAL.Dashboard.Listing;

namespace ADMIN.Areas.Dashboards.Controllers
{
    [Area("Dashboards")]
    [Authorize(Policy = "Admin-Dashboard-Listings-View")]
    public class ListingsController : Controller
    {
        private readonly IDashboardListing dashboardListing;
        public ListingsController(IDashboardListing dashboardListing)
        {
            this.dashboardListing = dashboardListing;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Staff()
        {
            return View();
        }
    }
}
