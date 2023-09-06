using BAL.Services.Contracts;
using BOL.LISTING;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ManageListing
{
    public partial class Enquiry
    {
        [Inject] public IUserService userService { get; set; }
        [Inject] public IListingService listingService { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }

        IList<ListingEnquiry> listingEnquiries;

        public string CurrentUserGuid { get; set; }
        public bool isVendor { get; set; } = false;

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
                    listingEnquiries = await listingService.GetEnquiryByOwnerIdAsync(CurrentUserGuid);
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }
    }
}
