using BAL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ManageListing
{
    public partial class AllListing
    {
        [Inject]
        private IListingService listingService { get; set; }
        [Inject]
        private IUserService userService { get; set; }

        public IEnumerable<BOL.LISTING.Listing> userListings { get; set; }

        public bool isVendor { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUser applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    isVendor = applicationUser.IsVendor;
                    userListings = await listingService.GetUsersListingAsync(applicationUser.Id);
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }
    }
}
