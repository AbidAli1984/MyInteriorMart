using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Images
    {
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] public IUserService userService { get; set; }
        [Inject] IListingService listingService { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] NavigationManager navManager { get; set; }
        [Inject] NotificationService _notice { get; set; }

        UploadImagesVM UploadImagesVM { get; set; } = new UploadImagesVM();

        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    UploadImagesVM.OwnerId = applicationUser.Id;
                    var listing = await listingService.GetListingByOwnerId(UploadImagesVM.OwnerId);
                    if (listing == null)
                        navManager.NavigateTo("/MyAccount/Listing/Company");

                    UploadImagesVM.ListingId = listing.ListingID;
                }
            }
            catch (Exception exc)
            {

            }
        }

        public void SetLogoImage(InputFileChangeEventArgs e)
        {
            UploadImagesVM.LogoImage = e.File.OpenReadStream();
        }

        public async Task UploadLogoImage()
        {
            if (UploadImagesVM.isLogoValid())
            {
                await helper.UploadLogoImage(UploadImagesVM);
                bool isUpdated = await listingService.AddOrUpdateLogoImage(UploadImagesVM);
                //StateHasChanged();

                if (isUpdated)
                    helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success","Logo Image uploaded successfully!");
                else
                    helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Error", "Something went worng, please contact Administrator!");
            }
        }
    }
}
