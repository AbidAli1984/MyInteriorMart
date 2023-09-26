using BAL.Services.Contracts;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class MyActivity
    {
        [Inject] public IUserService userService { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] public IAuditService auditService { get; set; }

        public bool isVendor { get; set; } = false;
        public ListingActivityCount listingActivityCount { get; set; } = new ListingActivityCount();

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
                    listingActivityCount = await auditService.GetListingActivityCountsByUserId(applicationUser.Id);
                }
            }
            catch (Exception exc)
            {

            }
        }
    }
}
