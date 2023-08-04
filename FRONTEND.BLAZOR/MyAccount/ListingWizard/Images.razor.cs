using AntDesign;
using BAL.FileManager;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
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

        #region Logo Image
        public void SetLogoImage(InputFileChangeEventArgs e)
        {
            UploadImagesVM.LogoImage = e.File.OpenReadStream();
        }

        public async Task UploadLogoImage()
        {
            if (!UploadImagesVM.isLogoValid())
            {
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Error", "Please select the image to upload!");
                return;
            }

            UploadImagesVM.LogoImageUrl = await helper.UploadLogoImage(UploadImagesVM.LogoImage, UploadImagesVM.OwnerId);
            UploadImagesVM.LogoImage = null;
            bool isUpdated = await listingService.AddOrUpdateLogoImage(UploadImagesVM);

            if (isUpdated)
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Logo Image uploaded successfully!");
            else
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Error", "Something went worng, please contact Administrator!");
        }
        #endregion

        #region Owner Image
        public void SetOwnerImage(InputFileChangeEventArgs e)
        {
            UploadImagesVM.OwnerImage = e.File.OpenReadStream();
        }

        public void SetOwnerDesignation(ChangeEventArgs events)
        {
            UploadImagesVM.OwnerImageDetail.Designation = events.Value.ToString();
        }

        public async Task UploadOwnerImage()
        {
            if (!UploadImagesVM.isOwnerValid())
            {
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Error", "All fields are compulsary!");
                return;
            }
            UploadImagesVM.OwnerImageDetail.ImageUrl = helper.GetOwnerImageFilePath(UploadImagesVM.OwnerId);
            bool isUpdated = await listingService.AddOwnerImage(UploadImagesVM);
            await helper.UploadOwnerImage(UploadImagesVM.OwnerImage, UploadImagesVM.OwnerImageDetail.ImageUrl);
            resetOwnerImageDetail();

            if (isUpdated)
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Owner Image uploaded successfully!");
            else
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Error", "Something went worng, please contact Administrator!");
        }

        public async Task DeleteOwnerImage(int id)
        {
            bool isDeleted = await listingService.DeleteOwnerImage(id);
            var imageDetail = UploadImagesVM.OwnerImages.Where(x => x.Id == id).FirstOrDefault();
            UploadImagesVM.OwnerImages.Remove(imageDetail);
            FileManagerService.DeletFile(imageDetail.ImageUrl);

            if (isDeleted)
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Owner Image deleted successfully!");
            else
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Error", "Something went worng, please contact Administrator!");
        }

        private void resetOwnerImageDetail()
        {
            string designation = UploadImagesVM.OwnerImageDetail.Designation;
            UploadImagesVM.OwnerImageDetail = new ImageDetails();
            UploadImagesVM.OwnerImageDetail.Designation = designation;
        }
        #endregion

        #region Gallery Image
        public void SetGalleryImage(InputFileChangeEventArgs e)
        {
            UploadImagesVM.GalleryImage = e.File.OpenReadStream();
        }

        public async Task UploadGalleryImage()
        {
            if (UploadImagesVM.isLogoValid())
            {
                //await helper.UploadLogoImage(UploadImagesVM);
                bool isUpdated = await listingService.AddOrUpdateLogoImage(UploadImagesVM);
                //StateHasChanged();

                if (isUpdated)
                    helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Logo Image uploaded successfully!");
                else
                    helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Error", "Something went worng, please contact Administrator!");
            }
        }
        #endregion
    }
}
