using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using BAL.Services.Contracts;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;

namespace FRONTEND.BLAZOR.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }

        [Inject]
        IUserService userService { get; set; }

        [Inject]
        SignInManager<DAL.Models.ApplicationUser> SignInManager { get; set; }

        [Inject]
        NavigationManager navManager { get; set; }

        public string CurrentUserGuid { get; set; }
        public bool isVendor { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public bool UserStaffPanelAuthenticated { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    ApplicationUser appUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = appUser.Id;
                    isVendor = appUser.IsVendor;

                    if (user.IsInRole("Staffs") == true)
                    {
                        UserStaffPanelAuthenticated = true;
                    }

                    userAuthenticated = true;
                }
            }
            catch (Exception exc)
            {
                
            }
        }
    }
}
