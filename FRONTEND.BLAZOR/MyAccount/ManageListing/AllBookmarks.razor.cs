using BAL.Services.Contracts;
using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ManageListing
{
    public partial class AllBookmarks
    {
        [Inject] public IUserService userService { get; set; }
        [Inject] public IAuditService auditService { get; set; }

        public bool isVendor { get; set; } = false;

        public IList<BookmarkListingViewModel> BookmarkListingVMs = new List<BookmarkListingViewModel>();

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
                    isVendor = applicationUser.IsVendor;
                    BookmarkListingVMs = await auditService.GetBookmarksByOwnerIdAsync(applicationUser.Id);
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }
    }
}