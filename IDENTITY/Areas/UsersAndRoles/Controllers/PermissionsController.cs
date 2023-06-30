using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BAL.Claims;
using Microsoft.AspNetCore.Authorization;
using IDENTITY.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BAL.Identity;
using Microsoft.AspNetCore.Authentication;
using Hangfire;
using System.ComponentModel;

namespace IDENTITY.Areas.UsersAndRoles.Controllers
{
    [Area("UsersAndRoles")]
    [Authorize]
    public class PermissionsController : Controller
    {
        private readonly IClaimsAdmin claimsAdmin;

        public PermissionsController(IClaimsAdmin claimsAdmin)
        {
            this.claimsAdmin = claimsAdmin;
        }

        // Shafi: List all claims
        //[Authorize(Policy = "Admin-Roles-ShowRolePermissions")]
        [HttpPost]
        public IActionResult Show(string roleId)
        {
            // Shafi: Get role ID
            ViewBag.RoleID = roleId;
            // End:

            ViewBag.AdminDashboard = claimsAdmin.Dashboard();
            ViewBag.AdminUser = claimsAdmin.UserManager();
            ViewBag.AdminListing = claimsAdmin.Listing();
            ViewBag.AdminLocality = claimsAdmin.Locality();
            ViewBag.AdminCategory = claimsAdmin.Category();
            ViewBag.AdminKeyword = claimsAdmin.Keywords();
            ViewBag.AdminMiscellaneous = claimsAdmin.Miscellaneous();
            ViewBag.AdminPage = claimsAdmin.Pages();
            ViewBag.AdminNotification = claimsAdmin.Notifications();
            ViewBag.AdminSlideshow = claimsAdmin.Slideshow();
            ViewBag.AdminHistoryAndCache = claimsAdmin.HistoryAndCache();
            return View();
        }
        // End:

        // Shafi: Assign Permissions to Role
        //[Authorize(Policy = "Admin-Roles-AssignClaimToRole")]
        [HttpPost]
        public IActionResult AssignClaimsToRole(string roleId, string claimType, string claimList)
        {
            // Shafi: Assign claims to role
            var jobId = BackgroundJob.Schedule(() => claimsAdmin.AssignClaimsToRole(roleId, claimType, claimList), TimeSpan.FromSeconds(1));
            // End:

            // Shafi: Create TempData object
            TempData["RoleId"] = roleId;
            // End:

            // Shafi: Redirect to
            return Redirect("/UsersAndRoles/RoleCategoryAndRoles");
            // End:
        }
        // End:

        [HttpPost]
        public IActionResult Dashboard(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Dashboard Claim List ViewBag
            ViewBag.AdminDashboard = claimsAdmin.Dashboard();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult Users(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Dashboard Claim List ViewBag
            ViewBag.AdminUser = claimsAdmin.UserManager();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult Listings(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Listings Claim List ViewBag
            ViewBag.AdminListing = claimsAdmin.Listing();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult Localities(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Localities Claim List ViewBag
            ViewBag.AdminLocality = claimsAdmin.Locality();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult Categories(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Localities Claim List ViewBag
            ViewBag.AdminCategory = claimsAdmin.Category();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult CategoryKeywords(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Localities Claim List ViewBag
            ViewBag.AdminKeyword = claimsAdmin.Keywords();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult Miscellaneous(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Localities Claim List ViewBag
            ViewBag.AdminMiscellaneous = claimsAdmin.Miscellaneous();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult Pages(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Localities Claim List ViewBag
            ViewBag.AdminPage = claimsAdmin.Pages();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult Notifications(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Notifications Claim List ViewBag
            ViewBag.AdminNotification = claimsAdmin.Notifications();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult Slideshow(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Slideshow Claim List ViewBag
            ViewBag.AdminSlideshow = claimsAdmin.Slideshow();
            // End:

            return View();
        }

        [HttpPost]
        public IActionResult HistoryCache(string roleId)
        {
            // Shafi: Show roleId in Dashboard View
            ViewBag.RoleID = roleId;
            // End:

            // Shafi: Slideshow Claim List ViewBag
            ViewBag.AdminHistoryAndCache = claimsAdmin.HistoryAndCache();
            // End:

            return View();
        }
    }
}
