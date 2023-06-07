using System;
using System.Linq;
using IDENTITY.Data;
using Microsoft.AspNetCore.Mvc;
using IDENTITY.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.SHARED;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IDENTITY.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class UserProfileController : Controller
    {
        private readonly ApplicationDbContext applicationContext;
        private readonly SharedDbContext sharedContext;
        private readonly UserManager<IdentityUser> userManager;
        public UserProfileController(ApplicationDbContext applicationContext, SharedDbContext sharedContext, UserManager<IdentityUser> userManager)
        {
            this.applicationContext = applicationContext;
            this.sharedContext = sharedContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Shafi: Get UserGuid & IP Address
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
            string ownerGuid = user.Id;
            // End:

            var profile = await applicationContext.UserProfile.Where(p => p.OwnerGuid == ownerGuid).FirstOrDefaultAsync();

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
            // Shafi: Get UserGuid & IP Address
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
            string ownerGuid = user.Id;
            // End:

            // Shafi: Check if user profile already exists
            var profileExists = await applicationContext.UserProfile.Where(p => p.OwnerGuid.Contains(ownerGuid)).FirstOrDefaultAsync();
            // End:

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
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
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
            userProfile.CreatedTime = timeZoneDate;
            // End:

            // Shafi: Check if user profile already exists
            var profileExists = await applicationContext.UserProfile.Where(p => p.OwnerGuid.Contains(ownerGuid)).FirstOrDefaultAsync();
            // End:

            if (profileExists == null)
            {
                if (ModelState.IsValid)
                {
                    applicationContext.UserProfile.Add(userProfile);
                    await applicationContext.SaveChangesAsync();
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
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
            string ownerGuid = user.Id;
            // End:

            var profile = await applicationContext.UserProfile.Where(p => p.OwnerGuid == ownerGuid).FirstOrDefaultAsync();

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
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
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
            userProfile.CreatedTime = timeZoneDate;
            // End:

            // Shafi: Check if user profile already exists
            var profileExists = await applicationContext.UserProfile.Where(p => p.OwnerGuid.Contains(ownerGuid)).FirstOrDefaultAsync();
            // End:

            if (userProfile != null)
            {
                if (ModelState.IsValid)
                {
                    applicationContext.Update(userProfile);
                    await applicationContext.SaveChangesAsync();
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
