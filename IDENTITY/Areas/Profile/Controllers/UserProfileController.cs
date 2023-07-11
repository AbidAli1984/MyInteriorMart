using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.SHARED;
using System.Threading.Tasks;
using BAL.Services.Contracts;
using BOL.IDENTITY;

namespace IDENTITY.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class UserProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly SharedDbContext sharedContext;
        public UserProfileController(IUserProfileService userProfileService, SharedDbContext sharedContext,
            IUserService userService)
        {
            this._userService = userService;
            this._userProfileService = userProfileService;
            this.sharedContext = sharedContext;
        }

        public async Task<IActionResult> Index()
        {
            // Shafi: Get UserGuid & IP Address
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            string ownerGuid = user.Id;
            // End:

            var profile = await _userProfileService.GetProfileByOwnerGuid(ownerGuid);

            if (profile != null)
            {
                ViewBag.ProfileExists = true;
                return View(profile);
            }
            else
            {
                ViewBag.ProfileExists = false;
                return View();
            }
        }

        public async Task<IActionResult> Create()
        {
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            string ownerGuid = user.Id;

            var profileExists = await _userProfileService.GetProfileByOwnerGuid(ownerGuid);

            if (profileExists == null)
            {
                ViewData["Countries"] = new SelectList(sharedContext.Country, "CountryID", "Name");
                ViewData["TimeZone"] = new SelectList(TimeZoneInfo.GetSystemTimeZones().OrderByDescending(t => t.Id == "India Standard Time").ThenBy(t => t.DisplayName), "Id", "DisplayName");

                return View();
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfileID,OwnerGuid,IPAddress,CreatedDate,CreatedTime,Name,Gender,DateOfBirth,CountryID,StateID,CityID,AssemblyID,PincodeID,TimeZoneOfCountry")] UserProfile userProfile)
        {
            // Shafi: Get UserGuid & IP Address
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            string remoteIpAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
            string ownerGuid = user.Id;
            // End:

            // Shafi: Assign values in background
            userProfile.OwnerGuid = ownerGuid;
            userProfile.IPAddress = remoteIpAddress;
            // End:

            // Shafi: Assign Time Zone to Create dDate & Created Time
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            userProfile.CreatedDate = timeZoneDate;
            // End:

            var profileExists = await _userProfileService.GetProfileByOwnerGuid(ownerGuid);

            if (profileExists == null)
            {
                if (ModelState.IsValid)
                {
                    await _userProfileService.AddUserProfile(userProfile);
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

            ViewData["Countries"] = new SelectList(sharedContext.Country, "CountryID", "Name");
            ViewData["TimeZone"] = new SelectList(TimeZoneInfo.GetSystemTimeZones().OrderByDescending(t => t.Id == "India Standard Time").ThenBy(t => t.DisplayName), "Id", "DisplayName");
            return View(userProfile);
        }

        public async Task<IActionResult> Edit()
        {
            // Shafi: Get UserGuid & IP Address
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            string ownerGuid = user.Id;
            // End:

            var profile = await _userProfileService.GetProfileByOwnerGuid(ownerGuid);

            if (profile != null)
            {
                ViewData["Countries"] = new SelectList(sharedContext.Country, "CountryID", "Name");
                ViewData["States"] = new SelectList(sharedContext.State, "StateID", "Name");
                ViewData["Cities"] = new SelectList(sharedContext.City, "CityID", "Name");
                ViewData["Assemblies"] = new SelectList(sharedContext.Station, "StationID", "Name");
                ViewData["Pincodes"] = new SelectList(sharedContext.Pincode, "PincodeID", "PincodeNumber");
                ViewData["TimeZone"] = new SelectList(TimeZoneInfo.GetSystemTimeZones().OrderByDescending(t => t.Id == "India Standard Time").ThenBy(t => t.DisplayName), "Id", "DisplayName");

                ViewBag.ProfileExists = true;
                return View(profile);
            }
            else
            {
                ViewData["Countries"] = new SelectList(sharedContext.Country, "CountryID", "Name");
                ViewData["States"] = new SelectList(sharedContext.State, "StateID", "Name");
                ViewData["Cities"] = new SelectList(sharedContext.City, "CityID", "Name");
                ViewData["Assemblies"] = new SelectList(sharedContext.City, "AssemblyID", "Name");
                ViewData["Pincodes"] = new SelectList(sharedContext.City, "PincodeID", "Name");
                ViewData["TimeZone"] = new SelectList(TimeZoneInfo.GetSystemTimeZones().OrderByDescending(t => t.Id == "India Standard Time").ThenBy(t => t.DisplayName), "Id", "DisplayName");

                ViewBag.ProfileExists = false;
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("ProfileID,OwnerGuid,IPAddress,CreatedDate,CreatedTime,Name,Gender,DateOfBirth,CountryID,StateID,CityID,AssemblyID,PincodeID,TimeZoneOfCountry")] UserProfile userProfile)
        {
            // Shafi: Get UserGuid & IP Address
            var user = await _userService.GetUserByUserName(User.Identity.Name);
            string remoteIpAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
            string ownerGuid = user.Id;
            // End:

            // Shafi: Assign values in background
            userProfile.OwnerGuid = ownerGuid;
            userProfile.IPAddress = remoteIpAddress;
            // End:

            // Shafi: Assign Time Zone to Create dDate & Created Time
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            userProfile.CreatedDate = timeZoneDate;
            // End:

            // Shafi: Check if user profile already exists
            var profileExists = await _userProfileService.GetProfileByOwnerGuid(ownerGuid);
            // End:

            if (userProfile != null)
            {
                if (ModelState.IsValid)
                {
                    await _userProfileService.UpdateUserProfile(userProfile);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["Countries"] = new SelectList(sharedContext.Country, "CountryID", "Name");
                    ViewData["States"] = new SelectList(sharedContext.State, "StateID", "Name");
                    ViewData["Cities"] = new SelectList(sharedContext.City, "CityID", "Name");
                    ViewData["Assemblies"] = new SelectList(sharedContext.City, "AssemblyID", "Name");
                    ViewData["Pincodes"] = new SelectList(sharedContext.City, "PincodeID", "Name");
                    ViewData["TimeZone"] = new SelectList(TimeZoneInfo.GetSystemTimeZones().OrderByDescending(t => t.Id == "India Standard Time").ThenBy(t => t.DisplayName), "Id", "DisplayName");

                    return View(userProfile);
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
