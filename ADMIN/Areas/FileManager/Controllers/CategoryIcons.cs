using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADMIN.Areas.FileManager.Controllers
{
    [Area("FileManager")]
    public class CategoryIcons : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public CategoryIcons(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> UploadIcon(string directory, string imageName, IFormFile file)
        {
            string directoryPath = hostingEnvironment.WebRootPath + $"{directory}";

            if (file == null)
            {
                return Redirect(Request.Headers["Referer"]);
            }
            else
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                if (file.Length > 0)
                    using (var fileStream = new FileStream(Path.Combine(directoryPath, imageName + ".png"), FileMode.Create))
                        await file.CopyToAsync(fileStream);

                return Redirect(Request.Headers["Referer"]);

            }
        }

        [HttpPost]
        public IActionResult DeleteImage(string directory, string imageName)
        {
            // Shafi: Get directory path
            string fullPath = directory + imageName + ".png";

            string image = hostingEnvironment.WebRootPath + fullPath;

            if (directory != null && imageName != null)
            {
                if (System.IO.File.Exists(image))
                {
                    System.IO.File.Delete(image);
                    TempData["Success"] = $"Success! File {imageName} deleted.";
                    return Redirect(Request.Headers["Referer"]);
                }
                else
                {
                    TempData["Error"] = $"Error! Image {imageName} not found.";
                    return Redirect(Request.Headers["Referer"]);
                }
            }
            else
            {
                TempData["Error"] = "Error! All parameters are required.";
                return Redirect(Request.Headers["Referer"]);
            }
        }
    }
}
