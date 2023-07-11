using BAL.Services.Contracts;
using BOL.LABOURNAKA;
using BOL.LABOURNAKA.ViewModal;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.StaffPanel.LabourNaka
{
    public partial class CategoriesLabourNaka
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        IWebHostEnvironment webEnv { get; set; }
        [Inject]
        public IUserService userService { get; set; }

        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public ApplicationUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OwnerGuid { get; set; }
        public string IpAddressUser { get; set; }
        public bool enableParentCat { get; set; }

        public IList<LaborCategoryVM> ListLaborCategoryVM = new List<LaborCategoryVM>();
        public IEnumerable<LaborCategory> ListLaborCategory { get; set; }

        // Begin: Get All Labour Categories
        public async Task GetLaborCategoriesAsync()
        {
            ListLaborCategory = await listingContext
                .LabourCategory
                .Where(i => i.IsChild == false)
                .ToListAsync();
        }

        public async Task GetLaborCategoriesVMAsync()
        {
            // Empty existing list
            ListLaborCategoryVM.Clear();

            var laborCategories = await listingContext.LabourCategory.ToListAsync();

            foreach(var lc in laborCategories)
            {
                string parentCatName = await listingContext.LabourCategory
                    .Where(i => i.CategoryId == lc.ParentCategoryId)
                    .Select(i => i.Name)
                    .FirstOrDefaultAsync();

                LaborCategoryVM lcvm = new LaborCategoryVM
                {
                    LaborCategory = lc,
                    ParentCategoryName = parentCatName
                };

                ListLaborCategoryVM.Add(lcvm);
            }
        }
        // End: Get All Labour Categories
         
        // Begin: Create Labour Category
        public string CategoryName { get; set; }
        public string CategoryUrl { get; set; }
        public string IsChildStr { get; set; } = "false";
        public bool IsChild { get; set; }
        public int? ParentCategoryId { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public bool ShowCreateCategory { get; set; }

        public void ToggleCreateLabour()
        {
            ShowCreateCategory = !ShowCreateCategory;
        }
        public async Task CreateLabourCategoryAsync()
        {
            if(CategoryName != null && CategoryUrl != null && MetaTitle != null && MetaDescription != null)
            {
                if(IsChildStr == "true" && ParentCategoryId != null)
                {
                    LaborCategory lc = new LaborCategory
                    {
                        Name = CategoryName,
                        Url = CategoryUrl,
                        MetaTitle = MetaTitle,
                        MetaDescription = MetaDescription,
                        IsChild = true,
                        ParentCategoryId = ParentCategoryId.Value
                    };

                    await listingContext.AddAsync(lc);
                    await listingContext.SaveChangesAsync();

                    // Clear Properties
                    CategoryName = null;
                    CategoryUrl = null;
                    MetaTitle = null;
                    MetaDescription = null;

                    // Load Labour Categories
                    await GetLaborCategoriesAsync();

                    // Clear Error Message
                    ErrorMessage = null;

                    // Reload ListLabourCategoryVM
                    await GetLaborCategoriesVMAsync();
                }
                else if(IsChildStr == "false")
                {
                    LaborCategory lc = new LaborCategory
                    {
                        Name = CategoryName,
                        Url = CategoryUrl,
                        MetaTitle = MetaTitle,
                        MetaDescription = MetaDescription,
                        IsChild = false
                    };

                    await listingContext.AddAsync(lc);
                    await listingContext.SaveChangesAsync();

                    // Clear Properties
                    CategoryName = null;
                    CategoryUrl = null;
                    MetaTitle = null;
                    MetaDescription = null;

                    // Load Labour Categories
                    await GetLaborCategoriesAsync();

                    // Clear Error Message
                    ErrorMessage = null;

                    // Reload ListLabourCategoryVM
                    await GetLaborCategoriesVMAsync();
                }
                else
                {
                    ErrorMessage = "Please select Parent Category.";
                }
                
            }
            else
            {
                ErrorMessage = "All fields are compulsory.";
            }
        }
        // End: Create Labour Category

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

                    await GetLaborCategoriesAsync();
                    await GetLaborCategoriesVMAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}
