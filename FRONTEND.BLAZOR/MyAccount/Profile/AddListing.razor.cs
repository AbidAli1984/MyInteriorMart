using BAL.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class AddListing
    {
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        IListingService listingService { get; set; }
        [Inject]
        AuthenticationStateProvider authenticationState { get; set; }

        public bool isVendor { get; set; } = false;
        public int stepsCompleted { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    string CurrentUserGuid = applicationUser.Id;
                    isVendor = applicationUser.IsVendor;
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    if (listing != null)
                        stepsCompleted = listing.Steps;
                }
            }
            catch (Exception exc)
            {

            }
        }
    }
}
