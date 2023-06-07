using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IDENTITY.Areas.Profile.Controllers
{
    [Area("Profile")]
    public class ProfileImageUploadController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IWebHostEnvironment hostingEnvironment;
        public ProfileImageUploadController(IWebHostEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;

        }

        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(int ProfileId, IFormFile file)
        {
            // Shafi Wrote: Get current user and guid
            IdentityUser currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            string userGuid = currentUser.Id;
            // End:

            ViewBag.AllDirectories = hostingEnvironment.WebRootPath + "\\FileManager\\ProfileImages\\";
            string directoryPath = hostingEnvironment.WebRootPath + "\\FileManager\\ProfileImages\\";

            var OriginalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            var renameFile = ProfileId + ".jpg";

            if (file.Length > 0)
                using (var fileStream = new FileStream(Path.Combine(directoryPath, renameFile), FileMode.Create))
                    await file.CopyToAsync(fileStream);

            return RedirectToAction("Index", "UserProfile", new { @area = "Profile" });
        }
    }
}
