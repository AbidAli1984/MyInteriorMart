using AntDesign;
using BAL.Services.Contracts;
using BOL.AUDITTRAIL;
using BOL.SHARED;
using BOL.VIEWMODELS;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.StaffPanel
{
    public partial class SubscribesStaff
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IUserService userService { get; set; }

        // Begin: Check if record exisit with listingId
        public string currentPage = "nav-address";
        public bool buttonBusy { get; set; }
        public bool disable { get; set; }

        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public ApplicationUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OwnerGuid { get; set; }
        public string IpAddressUser { get; set; }

        public IEnumerable<Subscribes> userSubscribes { get; set; }

        public IList<SubscribeListingViewModel> listSLVM = new List<SubscribeListingViewModel>();

        public async Task GetUsersSubscribesAsync()
        {
            userSubscribes = await auditContext.Subscribes.OrderByDescending(i => i.SubscribeID).ToListAsync();

            foreach (var i in userSubscribes)
            {
                var listing = await listingContext.Listing.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var category = await listingContext.Categories.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var firstCategory = await categoriesContext.FirstCategory.Where(x => x.FirstCategoryID == category.FirstCategoryID).FirstOrDefaultAsync();
                var secondCategory = await categoriesContext.SecondCategory.Where(x => x.SecondCategoryID == category.SecondCategoryID).FirstOrDefaultAsync();

                if (listing != null)
                {
                    SubscribeListingViewModel slvm = new SubscribeListingViewModel
                    {
                        SubscribeID = i.SubscribeID,
                        ListingID = i.ListingID,
                        UserGuid = i.UserGuid,
                        VisitDate = i.VisitDate.ToString("dd/MM/yyyy"),
                        CompanyName = listing.CompanyName,
                        NameFirstLetter = listing.CompanyName[0].ToString(),
                        ListingUrl = listing.ListingURL,
                        FirstCat = firstCategory.Name,
                        SecondCat = secondCategory.Name
                    };

                    listSLVM.Add(slvm);
                }
            }
        }

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

                    await GetUsersSubscribesAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}