using BAL.Services.Contracts;
using BOL.BANNERADS;
using BOL.CATEGORIES;
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
    public partial class Category
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
        public int? FirstCategoryID { get; set; }
        public int? SecondCategoryID { get; set; }
        public int? ThirdCategoryID { get; set; }

        // Begin: Get List First Categories
        public IEnumerable<FirstCategory> ListFirstCategory { get; set; }
        public async Task GetListFirstCategoryAsync()
        {
            ListFirstCategory = await categoriesContext.FirstCategory
                .ToListAsync();
        }
        // End: Get List First Categories

        // Begin: Get List Second Categories
        public IEnumerable<SecondCategory> ListSecondCategory { get; set; }
        public async Task GetSecondCategoryAsync(ChangeEventArgs e)
        {
            // Get First Category Id
            var firstCatId = Int32.Parse(e.Value.ToString());

            // Assign Value of First Category ID
            FirstCategoryID = firstCatId;

            ListSecondCategory = await categoriesContext.SecondCategory
                .Where(i => i.FirstCategoryID == firstCatId)
                .ToListAsync();
        }
        // End: Get List Second Categories

        // Begin: Get List Third Categories
        public IEnumerable<ThirdCategory> ListThirdCategory { get; set; }
        public async Task GetThirdCategoryAsync(ChangeEventArgs e)
        {
            // Get Second Category ID
            var secondCatId = Int32.Parse(e.Value.ToString());

            // Assign Value of Second Category
            SecondCategoryID = secondCatId;

            ListThirdCategory = await categoriesContext.ThirdCategory
                .Where(i => i.SecondCategoryID == secondCatId)
                .ToListAsync();
        }
        // End: Get List Third Categories

        public async Task NullAllPropertiesAsync()
        {
            Placement = null;
            Name = null;
            Priority = null;
            LinkUrl = null;
            TargetWindow = null;
            FileBanner = null;
            FirstCategoryID = null;
            SecondCategoryID = null;
            ThirdCategoryID = null;
            ListSecondCategory = null;
            ListThirdCategory = null;

            await GetListFirstCategoryAsync();
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

        // Begin: Get All Category Banners
        public IEnumerable<CategoryBanner> ListCategoryBanners { get; set; }
        public async Task GetAllCategoryBannersAsync()
        {
            ListCategoryBanners = await listingContext.CategoryBanner
                .OrderBy(i => i.Placement)
                .ThenBy(i => i.Priority)
                .ToListAsync();
        }
        // End: Get All Category Banners

        public async Task CreateAndUploadBannerAsync()
        {
            try
            {
                // Null ErrorMessage Value
                ErrorMessage = null;

                // Null SuccessMessage Value
                SuccessMessage = null;

                if (Placement != null && Name != null && Priority != null && LinkUrl != null && TargetWindow != null && FileBanner != null && FirstCategoryID != null && SecondCategoryID != null && ThirdCategoryID != null)
                {
                    // Get File Extension
                    string contentType = FileBanner.ContentType;
                    string fileExt = contentType.Substring(contentType.IndexOf('/') + 1);

                    if (fileExt != "jpeg")
                    {
                        ErrorMessage = "Please upload only jpeg files.";
                    }
                    else
                    {
                        // Add Category Banner Record in Database
                        CategoryBanner cm = new CategoryBanner
                        {
                            Placement = Placement,
                            Name = Name,
                            Priority = Priority.Value,
                            LinkUrl = LinkUrl,
                            TargetWindow = TargetWindow,
                            FirstCategoryID = FirstCategoryID.Value,
                            SecondCategoryID = SecondCategoryID.Value,
                            ThirdCategoryID = ThirdCategoryID.Value
                        };

                        await listingContext.AddAsync(cm);
                        await listingContext.SaveChangesAsync();

                        // Get Server's Custom Public Directory
                        var cloudBoxPath = Path.Combine(webEnv.WebRootPath, "CloudBox");

                        // Check if CloudBox Directory Exist
                        if (!Directory.Exists(cloudBoxPath))
                        {
                            Directory.CreateDirectory(cloudBoxPath);
                        }

                        // Create Category Directory in Cloud Box If Does Not Exists
                        var categoryDirPath = cloudBoxPath + "\\category";
                        if (!Directory.Exists(categoryDirPath))
                        {
                            Directory.CreateDirectory(categoryDirPath);
                        }

                        // Get Category Banner ID
                        var categoryBannerId = cm.BannerId;

                        // Rename Category Banner File
                        var newFileName = categoryBannerId + "." + fileExt;

                        // Open A Stream and Set Its Max Size to 1000000 i.e. 1 MB
                        Stream stream = FileBanner.OpenReadStream(1000000);

                        // Path of a File To Be Uploaded
                        var fileUploadPath = cloudBoxPath + "\\category\\" + newFileName;

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

                        // Get All Category Banners
                        await GetAllCategoryBannersAsync();

                        // Show Success Message
                        SuccessMessage = $"Banner {cm.Name} uploaded successfully.";
                    }
                }
                else
                {
                    ErrorMessage = "All Fields Required.";
                }

                await Task.Delay(1);
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }

        // Begin: Delete Banner
        public async Task DeleteBannerAsync(int bannerId)
        {
            try
            {
                var banner = await listingContext.CategoryBanner
                .FindAsync(bannerId);

                if (banner != null)
                {
                    // Get Server's Custom Public Directory
                    var cloudBoxPath = Path.Combine(webEnv.WebRootPath, "CloudBox");

                    // Image To Delete Path
                    var deleteImageFilePath = cloudBoxPath + "\\category\\" + bannerId + ".jpeg";

                    // Check if File exists, if yes then delete it
                    FileInfo file = new FileInfo(deleteImageFilePath);
                    if (File.Exists(deleteImageFilePath))
                    {
                        file.Delete();
                    }

                    // Delete Banner Record from Database
                    listingContext.Remove(banner);
                    await listingContext.SaveChangesAsync();

                    // Get All Category Banners
                    await GetAllCategoryBannersAsync();

                    // Show Success Message
                    SuccessMessage = $"File {banner.Name} deleted successfully.";
                }
            }
            catch (Exception exc)
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

                    await GetAllCategoryBannersAsync();
                    await GetListFirstCategoryAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}