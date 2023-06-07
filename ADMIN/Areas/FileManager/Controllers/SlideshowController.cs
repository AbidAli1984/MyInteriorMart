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
    public class SlideshowController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        public SlideshowController(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpPost]
        public async Task<IActionResult> UploadSlide(string directory, string imageName, IFormFile file)
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
                    using (var fileStream = new FileStream(Path.Combine(directoryPath, imageName + ".jpg"), FileMode.Create))
                        await file.CopyToAsync(fileStream);

                return Redirect(Request.Headers["Referer"]);

            }
        }
    }
}
