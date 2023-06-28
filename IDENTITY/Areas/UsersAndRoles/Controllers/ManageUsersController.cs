using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BAL.Services.Contracts;
using IDENTITY.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IDENTITY.Areas.UsersAndRoles.Controllers
{
    [Area("UsersAndRoles")]
    [Authorize]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ISuspendedUserService _suspendedUserService;

        public ManageUsersController(UserManager<IdentityUser> _userManager, ISuspendedUserService suspendedUserService)
        {
            userManager = _userManager;
            this._suspendedUserService = suspendedUserService;
        }

        [Authorize(Policy = "Admin-Dashboard-Users-View")]
        [Authorize(Policy = "Admin-Users-ViewAll")]
        public async Task<IActionResult> Index()
        {
            ViewBag.AllUsers = userManager.Users.Count();
            ViewBag.VerifiedUsers = userManager.Users.Where(u => u.EmailConfirmed == true).Count();
            ViewBag.NotVerifiedUsers = userManager.Users.Where(u => u.EmailConfirmed == false).Count();

            return View(await userManager.Users.OrderByDescending(u => u.Id).ToListAsync());
        }

        //[HttpGet]
        //public async Task<IActionResult> UsersByRole(string roleHashCode)
        //{
        //    ViewBag.AllUsers = userManager.Users.Count();
        //    ViewBag.VerifiedUsers = userManager.Users.Where(u => u.EmailConfirmed == true).Count();
        //    ViewBag.NotVerifiedUsers = userManager.Users.Where(u => u.EmailConfirmed == false).Count();

        //    // Shafi Wrote: Get list of users in role
        //    var results = userManager.
        //    // End:

        //    return View(userManager.Users.ToList());
        //}

        [Authorize(Policy = "Admin-Users-ViewProfile")]
        [Route("~/SuperAdministrator/ManageUsers/UserProfile/{userHashCode?}")]
        [HttpGet]
        public async Task<IActionResult> UserProfile(string userHashCode)
        {
            TempData["userHasCode"] = userHashCode;
            var userDetails = await userManager.Users.Where(u => u.Id == userHashCode).FirstOrDefaultAsync();

            return View(userDetails);
        }

        
        [Authorize(Policy = "Admin-Users-BlockUser")]
        [Route("~/SuperAdministrator/ManageUsers/SuspendUser")]
        [HttpPost]
        public async Task<IActionResult> SuspendUser(string SuspendedTo, string SuspendedBy)
        {
            // Shafi: Get current user guid
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            // End:

            // Shafi: Return user profile as model in view
            var detailsOfUserToSuspend = await userManager.Users.Where(u => u.Id == SuspendedTo).FirstOrDefaultAsync();
            // End:

            ViewBag.SuspendedTo = SuspendedTo;
            ViewBag.SuspendedBy = SuspendedBy;
            return View(detailsOfUserToSuspend);
            // End:
        }

        [Authorize(Policy = "Admin-Users-BlockUser")]
        [Route("~/SuperAdministrator/ManageUsers/SuspendUserConfirmation")]
        [HttpPost]
        public async Task<IActionResult> SuspendUserConfirmation(string SuspendedTo, string SuspendedBy, string Reason, string Comment)
        {
            // Shafi: Get current user guid
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            // End:

            if (SuspendedTo != null && SuspendedBy != null && Reason != null)
            {
                // Shafi: Create SuspendedDate time using India Standard Time
                DateTime SuspendedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                // End:

                // Shafi: Reason for suspending
                string ReasonForSuspending = Reason + " : " + Comment;
                // End:

                var suspendedUser = await userManager.FindByIdAsync(SuspendedTo);

                await _suspendedUserService.SuspendUser(SuspendedTo, SuspendedBy, SuspendedDate, ReasonForSuspending);
                return Redirect("/UsersAndRoles/RoleCategoryAndRoles");
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            // End:
        }

        [Authorize(Policy = "Admin-Users-UnblockUser")]
        [Route("~/SuperAdministrator/ManageUsers/UnsuspendUser")]
        [HttpPost]
        public async Task<IActionResult> UnsuspendUser(string UnsuspendedTo)
        {
            // Shafi: Get current user guid
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            // End:

            // Shafi: Return user profile as model in view
            var detailsOfUserToUnsuspend = await userManager.Users.Where(u => u.Id == UnsuspendedTo).FirstOrDefaultAsync();
            // End:

            ViewBag.UnsuspendedTo = UnsuspendedTo;
            return View(detailsOfUserToUnsuspend);
            // End:
        }

        [Authorize(Policy = "Admin-Users-UnblockUser")]
        [Route("~/SuperAdministrator/ManageUsers/UnsuspendUserConfirmation")]
        [HttpPost]
        public async Task<IActionResult> UnsuspendUserConfirmation(string UnsuspendedTo, string Reason, string Comment)
        {
            // Shafi: Get current user guid
            var userName = User.Identity.Name;
            var user = await userManager.FindByNameAsync(userName);
            string UnsuspendedBy = user.Id;
            // End:

            if (UnsuspendedTo != null && UnsuspendedBy != null && Reason != null)
            {
                // Shafi: Create SuspendedDate time using India Standard Time
                DateTime UnsuspendedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                // End:

                // Shafi: Reason for Unsuspending
                string ReasonForUnsuspending = Reason + " : " + Comment;
                // End:

                var unsuspendedUser = await userManager.FindByIdAsync(UnsuspendedTo);

                await _suspendedUserService.UnSuspendUser(UnsuspendedTo, UnsuspendedBy, UnsuspendedDate, ReasonForUnsuspending);
                return Redirect("/Home/AccountUnsuspended");
            }
            else
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            // End:
        }

        [Authorize(Policy = "Admin-Users-ViewAllBlockedUser")]
        [Route("~/SuperAdministrator/ManageUsers/ListBlockedUsers")]
        public IActionResult ListBlockedUsers()
        {
            return View();
        }

        [Authorize(Policy = "Admin-Users-ViewAllUnblockedUser")]
        [Route("~/SuperAdministrator/ManageUsers/ListUnblockedUser")]
        public IActionResult ListUnblockedUser()
        {
            return View();
        }
    }
}