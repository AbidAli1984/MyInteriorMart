using AntDesign;
using BAL.Services.Contracts;
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

namespace FRONTEND.BLAZOR.MyAccount.ManageListing
{
    public partial class AllReviews
    {
        [Inject]
        public IUserService userService { get; set; }

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
        public ApplicationUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OwnerGuid { get; set; }
        public string IpAddressUser { get; set; }

        public IEnumerable<BOL.LISTING.Rating> userRatings { get; set; }
        public IList<ReviewListingViewModel> listRLVM = new List<ReviewListingViewModel>();

        public async Task GetUsersReviewsAsync()
        {
            userRatings = await listingContext.Rating.Where(i => i.OwnerGuid == CurrentUserGuid).OrderByDescending(i => i.ListingID).ToListAsync();

            foreach(var i in userRatings)
            {
                var listing = await listingContext.Listing.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var category = await listingContext.Categories.Where(x => x.ListingID == i.ListingID).FirstOrDefaultAsync();
                var firstCategory = await categoriesContext.FirstCategory.Where(x => x.FirstCategoryID == category.FirstCategoryID).FirstOrDefaultAsync();
                var secondCategory = await categoriesContext.SecondCategory.Where(x => x.SecondCategoryID == category.SecondCategoryID).FirstOrDefaultAsync();

                if (listing != null)
                {
                    ReviewListingViewModel rlvm = new ReviewListingViewModel
                    {
                        ReviewID = i.RatingID,
                        ListingId = i.ListingID,
                        OwnerGuid = i.OwnerGuid,
                        Date = i.Date,
                        Name = listing.CompanyName,
                        NameFirstLetter = listing.CompanyName[0].ToString(),
                        ListingUrl = listing.ListingURL,
                        FirstCat = firstCategory.Name,
                        SecondCat = secondCategory.Name,
                        Ratings = i.Ratings,
                        Comment = i.Comment
                    };

                    listRLVM.Add(rlvm);
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

                    iUser = await userService.GetUserByUserNameOrEmail(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;

                    await GetUsersReviewsAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}
