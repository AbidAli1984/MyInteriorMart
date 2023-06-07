using AntDesign;
using BOL.AUDITTRAIL;
using BOL.SHARED;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ManageListing
{
    public partial class AllBookmarks
    {
        // Begin: Check if record exisit with listingId
        public string currentPage = "nav-address";
        public bool buttonBusy { get; set; }
        public bool disable { get; set; }

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public IdentityUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OwnerGuid { get; set; }
        public string IpAddressUser { get; set; }

        public IEnumerable<Bookmarks> userBookmarks { get; set; }

        public IList<BookmarkListingViewModel> listBLVM = new List<BookmarkListingViewModel>();

        public async Task GetUsersBookmarksAsync()
        {
            userBookmarks = await auditContext.Bookmarks.Where(i => i.UserGuid == CurrentUserGuid && i.Bookmark == true).OrderByDescending(i => i.BookmarksID).ToListAsync();

            foreach (var i in userBookmarks)
            {
                var listing = await listingContext.Listing.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var category = await listingContext.Categories.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var firstCategory = await categoriesContext.FirstCategory.Where(x => x.FirstCategoryID == category.FirstCategoryID).FirstOrDefaultAsync();
                var secondCategory = await categoriesContext.SecondCategory.Where(x => x.SecondCategoryID == category.SecondCategoryID).FirstOrDefaultAsync();

                if (listing != null)
                {
                    BookmarkListingViewModel rlvm = new BookmarkListingViewModel
                    {
                        BookmarkID = i.BookmarksID,
                        ListingID = i.ListingID,
                        UserGuid = i.UserGuid,
                        VisitDate = i.VisitDate.ToString("dd/MM/yyyy"),
                        CompanyName = listing.CompanyName,
                        NameFirstLetter = listing.CompanyName[0].ToString(),
                        ListingUrl = listing.ListingURL,
                        FirstCat = firstCategory.Name,
                        SecondCat = secondCategory.Name
                    };

                    listBLVM.Add(rlvm);
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

                    iUser = await applicationContext.Users.Where(i => i.UserName == user.Identity.Name).FirstOrDefaultAsync();
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;

                    await GetUsersBookmarksAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}