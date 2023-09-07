using BAL.Services.Contracts;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class Notification
    {
        [Inject] public IUserService userService { get; set; }
        [Inject] public IAuditService auditService { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }

        IList<ListingActivityVM> listingActivityVMs { get; set; } = new List<ListingActivityVM>();

        public bool isVendor { get; set; } = false;


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
                    listingActivityVMs = await auditService.GetNotificationByOwnerIdAsync(applicationUser.Id);
                }
            }
            catch (Exception exc)
            {

            }
        }
    }
}
