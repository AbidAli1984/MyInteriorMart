using BAL.Services.Contracts;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
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

        public string CurrentUserGuid { get; set; }
        public bool isVendor { get; set; } = false;

        public IEnumerable<BOL.LISTING.Rating> userRatings { get; set; }
        public IList<ReviewListingViewModel> listRLVM = new List<ReviewListingViewModel>();

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    isVendor = applicationUser.IsVendor;
                    await GetUsersReviewsAsync();
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

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
    }
}
