using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FRONTEND.BLAZOR.Services;
using AntDesign;
using BOL.CATEGORIES;
using BOL.LISTING;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using BlazorInputFile;
using System.IO;
using DAL.Models;
using BAL.Services.Contracts;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Logo
    {
        [Inject]
        public IUserService userService { get; set; }

        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-logo";
        public bool buttonBusy { get; set; }

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public ApplicationUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }

        // Begin: Check if record exists
        public bool companyExist { get; set; }
        public bool communicationExist { get; set; }
        public bool addressExist { get; set; }
        public bool categoryExist { get; set; }
        public bool specialisationExist { get; set; }
        public bool workingHoursExist { get; set; }
        public bool paymentModeExist { get; set; }

        public async Task CompanyExistAsync()
        {
            if (listingId != null)
            {
                var company = await listingContext.Listing.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (company != null)
                {
                    companyExist = true;
                }
                else
                {
                    companyExist = false;
                }
            }
        }

        public async Task CommunicationExistAsync()
        {
            if (listingId != null)
            {
                var communication = await listingContext.Communication.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (communication != null)
                {
                    communicationExist = true;
                }
                else
                {
                    communicationExist = false;
                }
            }
        }

        public async Task AddressExistAsync()
        {
            if (listingId != null)
            {
                var address = await listingContext.Address.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (address != null)
                {
                    addressExist = true;
                }
                else
                {
                    addressExist = false;
                }
            }
        }

        public async Task CategoryExistAsync()
        {
            if (listingId != null)
            {
                var category = await listingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (category != null)
                {
                    categoryExist = true;
                }
                else
                {
                    categoryExist = false;
                }
            }
        }

        public async Task SpecialisationExistAsync()
        {
            if (listingId != null)
            {
                var specialisation = await listingContext.Specialisation.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (specialisation != null)
                {
                    specialisationExist = true;
                }
                else
                {
                    specialisationExist = false;
                }
            }
        }

        public async Task WorkingHoursExistAsync()
        {
            if (listingId != null)
            {
                var wh = await listingContext.WorkingHours.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (wh != null)
                {
                    workingHoursExist = true;
                }
                else
                {
                    workingHoursExist = false;
                }
            }
        }

        public async Task PaymentModeExistAsync()
        {
            if (listingId != null)
            {
                var pm = await listingContext.PaymentMode.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (pm != null)
                {
                    paymentModeExist = true;
                }
                else
                {
                    paymentModeExist = false;
                }
            }
        }
        // End: Check if record exists

        private string ImgUrl;
        public bool LogoExist { get; set; }

        [Inject]
        public IWebHostEnvironment hostEnv { get; set; }

        public async Task CheckIfLogoExist()
        {
            var file = hostEnv.WebRootPath + "\\FileManager\\ListingLogo\\" + listingId + ".jpg";
            if(File.Exists(file) == true)
            {
                LogoExist = true;
            }
            else
            {
                LogoExist = false;
            }
            await Task.Delay(10);
        }

        public async Task GetFileType()
        {
            var file = hostEnv.WebRootPath + "\\FileManager\\ListingLogo\\" + listingId + ".jpg";

            if (File.Exists(file) == true)
            {
                ImgUrl = "/FileManager/ListingLogo/" + listingId + ".jpg";
            }
            else
            {
                ImgUrl = null;
            }
            
            await Task.Delay(1);
        }

        // Begin: Logo Upload
        public async Task UploadLogoAsync(InputFileChangeEventArgs e)
        {
            await GetFileType();

            try
            {
                var dir = hostEnv.WebRootPath + "\\FileManager\\ListingLogo\\";

                // If directory does not exists then create a new directory
                if(!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // get final path with file name
                var fileName = listingId + ".jpg";
                var path = dir + fileName;

                // Delete exisiting file if any
                if(File.Exists(path) == true)
                {
                    File.Delete(path);
                    await GetFileType();
                }

                var fileType = e.File.ContentType;
                if (fileType != "image/jpeg")
                {
                    ErrorMessage = $"Only *.jpeg or *.jpg file formats are supported. You are trying to upload a file with {fileType} format which is not supported.";
                }
                else
                {
                    try
                    {
                        await using FileStream fs = new(path, FileMode.Create);
                        await e.File.OpenReadStream().CopyToAsync(fs);
                        await GetFileType();
                        ErrorMessage = null;
                    }
                    catch (Exception exc)
                    {
                        ErrorMessage = exc.Message;
                    }
                }
            }
            catch(Exception exc)
            {
                ErrorMessage = exc.Message;
            }

            await CheckIfLogoExist();
            await Task.Delay(10);
        }
        // End: Logo Upload

        // Begin: Delete Logo
        public async Task DeleteLogoAsync(string imgName)
        {
            var dir = hostEnv.WebRootPath + "\\FileManager\\ListingLogo\\" + listingId + "\\";
            var image = dir + imgName;

            File.Delete(image);
            await GetFileType();
            await CheckIfLogoExist();
            ErrorMessage = null;
        }
        // End: Delete Logo

        // Begin: Get images in gallery
        public IList<string> GalleryListUrl = new List<string>();

        public async Task GetImagesInGalleryAsync()
        {
            GalleryListUrl.Clear();
            var dir = hostEnv.WebRootPath + "\\FileManager\\ListingGallery\\" + listingId;

            if(Directory.Exists(dir) == true)
            {
                var imgList = Directory.GetFiles(dir);
                foreach (string file in imgList)
                {
                    string fn = Path.GetFileName(file);
                    GalleryListUrl.Add(fn);
                }
            }

            await Task.Delay(5);
        }
        // Begin: Get images in gallery

        // Begin: Upload Gallery
        public async Task UploadGalleryAsync(InputFileChangeEventArgs e)
        {
            await GetFileType();

            try
            {
                var listingGallery = hostEnv.WebRootPath + "\\FileManager\\ListingGallery\\";
                var dir = hostEnv.WebRootPath + "\\FileManager\\ListingGallery\\" + listingId;

                // If directory does not exists then create a new directory
                if (!Directory.Exists(listingGallery))
                {
                    Directory.CreateDirectory(listingGallery);
                }

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                var imgFiles = e.GetMultipleFiles(20);

                foreach (var i in imgFiles)
                {
                    // get final path with file name
                    var path = dir + "\\" + i.Name;

                    // Delete exisiting file if any
                    if (File.Exists(path) == true)
                    {
                        File.Delete(path);
                    }

                    try
                    {
                        await using FileStream fs = new(path, FileMode.Create);
                        await i.OpenReadStream(1000000).CopyToAsync(fs);
                        await GetImagesInGalleryAsync();
                        ErrorMessage = null;
                    }
                    catch (Exception exc)
                    {
                        ErrorMessage = exc.Message;
                    }
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }

            await Task.Delay(10);
        }
        // End: Upload Gallery

        // Begin: Delete Image
        public async Task DeleteImageAsync(string imgName)
        {
            var dir = hostEnv.WebRootPath + "\\FileManager\\ListingGallery\\" + listingId + "\\";
            var image = dir + imgName;

            File.Delete(image);
            await GetImagesInGalleryAsync();
            ErrorMessage = null;
        }
        // End: Delete Image

        // Begin: Antdesign Blazor Notification
        private async Task NoticeWithIcon(NotificationType type, NotificationPlacement placement, string message, string description)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = message,
                Description = description,
                NotificationType = type,
                Placement = placement
            });
        }
        // End: Antdesign Blazor Notification

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    // Shafi: Assign Time Zone to CreatedDate & Created Time
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    IpAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    CreatedDate = timeZoneDate;
                    CreatedTime = timeZoneDate;
                    // End:

                    iUser = await userService.GetUserByUserNameOrEmail(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;

                    // Begin: Check if record exists
                    await CompanyExistAsync();
                    await CommunicationExistAsync();
                    await AddressExistAsync();
                    await CategoryExistAsync();
                    await SpecialisationExistAsync();
                    await WorkingHoursExistAsync();
                    await PaymentModeExistAsync();
                    // End: Check if record exists

                    await GetFileType();
                    await GetImagesInGalleryAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}
