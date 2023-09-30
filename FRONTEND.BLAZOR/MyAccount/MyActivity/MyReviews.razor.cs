using BAL.Services.Contracts;
using BOL.LISTING;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.MyActivity
{
    public partial class MyReviews
    {
        [Inject] private AuthenticationStateProvider authenticationState { get; set; }
        [Inject] private IUserService userService { get; set; }
        [Inject] private IListingService listingService { get; set; }

        public string CurrentUserGuid { get; set; }
        public bool isVendor { get; set; } = false;

        public IList<ReviewListingViewModel> ReviewListingVMs = new List<ReviewListingViewModel>();

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
                    ReviewListingVMs = await listingService.GetMyReviewsByUserIdAsync(CurrentUserGuid);
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task UpdateReview(ReviewListingViewModel review)
        {
            try
            {
                var rating = await listingService.GetRatingByRatingId(review.RatingId);
                if (rating != null)
                {
                    rating.Comment = review.Comment;
                    rating.Ratings = review.Ratings;
                    await listingService.UpdateAsync(rating);
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }
    }
}
