using BAL.Dashboard.Listing;
using DAL.LISTING;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STAFF.Areas.ListingManagement.Controllers
{
    [Area("ListingManagement")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDashboardListing dashboardListing;
        private readonly ListingDbContext listingContext;
        private readonly UserManager<IdentityUser> userManager;
        public HomeController(IDashboardListing dashboardListing, ListingDbContext listingContext, UserManager<IdentityUser> userManager)
        {
            this.dashboardListing = dashboardListing;
            this.listingContext = listingContext;
            this.userManager = userManager;
        }

        [Route("/StaffPanel/ListingManagement/Home/Index")]
        public IActionResult Index()
        {


            return View();
        }

        // Begin: Listing Period
        public async Task<JsonResult> ListingByPeriod(int periodInDays)
        {
            DateTime period = DateTime.Now.AddDays(-periodInDays);

            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var data = await listingContext.Listing.Where(i => /*i.OwnerGuid == user.Id && */i.CreatedDate >= period).CountAsync();
            return Json(data);
        }
        // End:
    }
}
