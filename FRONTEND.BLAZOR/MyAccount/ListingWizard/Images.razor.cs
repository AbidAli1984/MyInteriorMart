using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Images
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        private IUserProfileService userProfileService { get; set; }
        [Inject]
        AuthenticationStateProvider authenticationState { get; set; }
        [Inject]
        Helper helper { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }

        UploadImages UploadImages { get; set; } = new UploadImages();

        public string CurrentUserGuid { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                }
            }
            catch (Exception exc)
            {

            }
        }

        public void SetLogoImage(InputFileChangeEventArgs e)
        {
            UploadImages.LogoImage = e.File.OpenReadStream();
        }

        public async Task UploadLogoImage()
        {
            if (UploadImages.LogoImage != null)
            {
                UploadImages.LogoImageUrl = await helper.UploadLogoImage(UploadImages.LogoImage, CurrentUserGuid);
                UploadImages.LogoImage = null;
            }
        }
    }
}
