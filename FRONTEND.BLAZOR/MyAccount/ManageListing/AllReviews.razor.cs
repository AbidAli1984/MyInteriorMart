using BAL.Services.Contracts;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ManageListing
{
    public partial class AllReviews
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
                    ReviewListingVMs = await listingService.GetReviewsByOwnerIdAsync(CurrentUserGuid);
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }
    }
}
