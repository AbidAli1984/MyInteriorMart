using BAL.Services.Contracts;
using BOL.BANNERADS;
using BOL.LABOURNAKA;
using BOL.LABOURNAKA.ViewModal;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.StaffPanel.Banners
{
    public partial class Home
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IWebHostEnvironment webEnv { get; set; }
        [Inject]
        public IUserService userService { get; set; }


        public string CurrentUserGuid { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public ApplicationUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OwnerGuid { get; set; }
        public string IpAddressUser { get; set; }
        public bool ShowAddBanner { get; set; }
        public IBrowserFile FileBanner { get; set; }

        // Begin: Home Banner Properties
        public string Placement { get; set; }
        public string Name { get; set; }
        public int? Priority { get; set; }
        public string LinkUrl { get; set; }
        public string TargetWindow { get; set; }
        public bool Disable { get; set; }

        public async Task NullAllPropertiesAsync()
        {
            Placement = null;
            Name = null;
            Priority = null;
            LinkUrl = null;
            TargetWindow = null;
            FileBanner = null;
            await Task.Delay(1);
        }

        // End: Home Banner Properties

        // Begin: Point File to 
        public async Task OnChangeImageAsync(InputFileChangeEventArgs e)
        {
            FileBanner = null;
            FileBanner = e.File;
            await Task.Delay(1);
        }

        public void ToggleAddBanner()
        {
            ShowAddBanner = !ShowAddBanner;
        }

        // Begin: Get All Home Banners
        public IEnumerable<HomeBanner> ListHomeBanners { get; set; }
        public async Task GetAllHomeBannersAsync()
        {
            ListHomeBanners = await listingContext.HomeBanner
                .OrderBy(i => i.Placement)
                .ThenBy(i => i.Priority)
                .ToListAsync();
        }
        // End: Get All Home Banners

        public async Task CreateAndUploadBannerAsync()
        {
            try
            {
                // Null ErrorMessage Value
                ErrorMessage = null;

                // Null SuccessMessage Value
                SuccessMessage = null;

                if (Placement != null && Name != null && Priority != null && LinkUrl != null && TargetWindow != null && FileBanner != null)
                {
                    // Get File Extension
                    string contentType = FileBanner.ContentType;
                    string fileExt = contentType.Substring(contentType.IndexOf('/') + 1);

                    if(fileExt != "jpeg")
                    {
                        ErrorMessage = "Please upload only jpeg files.";
                    }
                    else
                    {
                        // Add Home Banner Record in Database
                        HomeBanner hm = new HomeBanner
                        {
                            Placement = Placement,
                            Name = Name,
                            Priority = Priority.Value,
                            LinkUrl = LinkUrl,
                            TargetWindow = TargetWindow
                        };

                        await listingContext.AddAsync(hm);
                        await listingContext.SaveChangesAsync();

                        // Get Server's Custom Public Directory
                        var cloudBoxPath = Path.Combine(webEnv.WebRootPath, "CloudBox");

                        // Check if CloudBox Directory Exist
                        if (!Directory.Exists(cloudBoxPath))
                        {
                            Directory.CreateDirectory(cloudBoxPath);
                        }

                        // Create Home Directory in Cloud Box If Does Not Exists
                        var homeDirPath = cloudBoxPath + "\\home";
                        if (!Directory.Exists(homeDirPath))
                        {
                            Directory.CreateDirectory(homeDirPath);
                        }

                        // Get Home Banner ID
                        var homeBannerId = hm.BannerId;

                        // Rename Home Banner File
                        var newFileName = homeBannerId + "." + fileExt;

                        // Open A Stream and Set Its Max Size to 1000000 i.e. 1 MB
                        Stream stream = FileBanner.OpenReadStream(1000000);

                        // Path of a File To Be Uploaded
                        var fileUploadPath = cloudBoxPath + "\\home\\" + newFileName; 

                        // Create a File Stream
                        FileStream fileStream = File.Create(fileUploadPath);

                        // Upload File
                        await stream.CopyToAsync(fileStream);

                        // Close Stream
                        stream.Close();

                        // Close File Stream
                        fileStream.Close();

                        // Null All Properties Value
                        await NullAllPropertiesAsync();

                        // Get All Home Banners
                        await GetAllHomeBannersAsync();

                        // Show Success Message
                        SuccessMessage = $"Banner {hm.Name} uploaded successfully.";
                    }
                }
                else
                {
                    ErrorMessage = "All Fields Required.";
                }

                await Task.Delay(1);
            }
            catch(Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }

        // Begin: Delete Banner
        public async Task DeleteBannerAsync(int bannerId)
        {
            try
            {
                var banner = await listingContext.HomeBanner
                .FindAsync(bannerId);

                if (banner != null)
                {
                    // Get Server's Custom Public Directory
                    var cloudBoxPath = Path.Combine(webEnv.WebRootPath, "CloudBox");

                    // Image To Delete Path
                    var deleteImageFilePath = cloudBoxPath + "\\home\\" + bannerId + ".jpeg";

                    // Check if File exists, if yes then delete it
                    FileInfo file = new FileInfo(deleteImageFilePath);
                    if(File.Exists(deleteImageFilePath))
                    {
                        file.Delete();
                    }

                    // Delete Banner Record from Database
                    listingContext.Remove(banner);
                    await listingContext.SaveChangesAsync();

                    // Get All Home Banners
                    await GetAllHomeBannersAsync();

                    // Show Success Message
                    SuccessMessage = $"File {banner.Name} deleted successfully.";
                }
            }
            catch(Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
        // End: Delete Banner
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
                    IpAddressUser = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    CreatedDate = timeZoneDate;
                    CreatedTime = timeZoneDate;
                    // End:

                    iUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;

                    await GetAllHomeBannersAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}
