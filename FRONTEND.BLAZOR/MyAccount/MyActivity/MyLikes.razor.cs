using BAL.Services.Contracts;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.MyActivity
{
    public partial class MyLikes
    {
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] public IUserService userService { get; set; }
        [Inject] public IAuditService auditService { get; set; }

        public IList<ListingActivityVM> likeListingVMs = new List<ListingActivityVM>();

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
                    likeListingVMs = await auditService.GetListingLikesByUserIdAsync(applicationUser.Id);
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }
    }
}
