using BAL.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ManageListing
{
    public partial class AllListing
    {
        // Begin: Check if record exisit with listingId
        public string currentPage = "nav-address";
        public bool buttonBusy { get; set; }
        public bool disable { get; set; }


        [Inject]
        private IListingService listingService { get; set; }

        [Inject]
        private IUserService userService { get; set; }

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }
        public string OwnerGuid { get; set; }
        public string IpAddressUser { get; set; }

        public IEnumerable<BOL.LISTING.Listing> userListings { get; set; }

        public async Task GetUsersListingAsync()
        {
            userListings = await listingService.GetUsersListingAsync(CurrentUserGuid);
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

                    IdentityUser iUser = await userService.GetUserByUserNameOrEmail(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;

                    await GetUsersListingAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}
