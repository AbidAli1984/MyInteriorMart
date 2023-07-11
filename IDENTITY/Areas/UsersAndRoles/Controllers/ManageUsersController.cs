using System;
using System.Linq;
using System.Threading.Tasks;
using BAL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IDENTITY.Areas.UsersAndRoles.Controllers
{
    [Area("UsersAndRoles")]
    [Authorize]
    public class ManageUsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISuspendedUserService _suspendedUserService;

        public ManageUsersController(IUserService userService, ISuspendedUserService suspendedUserService)
        {
            this._userService = userService;
            this._suspendedUserService = suspendedUserService;
        }

        [Authorize(Policy = "Admin-Dashboard-Users-View")]
        [Authorize(Policy = "Admin-Users-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsers();
            ViewBag.AllUsers = users.Count();
            ViewBag.VerifiedUsers = users.Where(u => u.EmailConfirmed == true).Count();
            ViewBag.NotVerifiedUsers = users.Where(u => u.EmailConfirmed == false).Count();

            return View(users.OrderByDescending(u => u.Id));
        }

        //[HttpGet]
        //public async Task<IActionResult> UsersByRole(string roleHashCode)
        //{
        //    var users = await _userService.GetUsers();
        //    ViewBag.AllUsers = users.Count();
        //    ViewBag.VerifiedUsers = users.Where(u => u.EmailConfirmed == true).Count();
        //    ViewBag.NotVerifiedUsers = users.Where(u => u.EmailConfirmed == false).Count();

        //    return View(users.OrderByDescending(u => u.Id).ToList());
        //}

        [Authorize(Policy = "Admin-Users-ViewProfile")]
        [Route("~/SuperAdministrator/ManageUsers/UserProfile/{userHashCode?}")]
        [HttpGet]
        public async Task<IActionResult> UserProfile(string userHashCode)
        {
            TempData["userHasCode"] = userHashCode;
            var userDetails = await _userService.GetUserById(userHashCode);

            return View(userDetails);
        }

        
        [Authorize(Policy = "Admin-Users-BlockUser")]
        [Route("~/SuperAdministrator/ManageUsers/SuspendUser")]
        [HttpPost]
        public async Task<IActionResult> SuspendUser(string SuspendedTo, string SuspendedBy)
        {
            // Shafi: Get current user guid
            var userName = User.Identity.Name;
            var user = await _userService.GetUserByUserName(userName);
            // End:

            // Shafi: Return user profile as model in view
            var detailsOfUserToSuspend = await _userService.GetUserById(SuspendedTo);
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
            var user = await _userService.GetUserByUserName(userName);
            // End:

            if (SuspendedTo != null && SuspendedBy != null && Reason != null)
            {
                // Shafi: Create SuspendedDate time using India Standard Time
                DateTime SuspendedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                // End:

                // Shafi: Reason for suspending
                string ReasonForSuspending = Reason + " : " + Comment;
                // End:

                var suspendedUser = await _userService.GetUserById(SuspendedTo);

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
            var user = await _userService.GetUserByUserName(userName);
            // End:

            // Shafi: Return user profile as model in view
            var detailsOfUserToUnsuspend = await _userService.GetUserById(UnsuspendedTo);
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
            var user = await _userService.GetUserByUserName(userName);
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

                var unsuspendedUser = await _userService.GetUserById(UnsuspendedTo);

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