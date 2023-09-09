using AntDesign;
using BAL;
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
        [Inject] HelperFunctions helperFunction { get; set; }
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
                    helper.NavigateToPageByStep(listing, BOL.Constants.PaymentModeComplete, navManager);

                    UploadImagesVM.ListingId = listing.ListingID;
                    UploadImagesVM.step = listing.Steps;
                    var logoImage = await listingService.GetLogoImageByListingId(UploadImagesVM.ListingId);
                    if (logoImage != null)
                        UploadImagesVM.LogoImageUrl = logoImage.ImagePath;
                    UploadImagesVM.OwnerImages = await listingService.GetOwnerImagesByListingId(UploadImagesVM.ListingId);
                    UploadImagesVM.GalleryImages = await listingService.GetGalleryImagesByListingId(UploadImagesVM.ListingId);
                    UploadImagesVM.BannerImageDetail = await listingService.GetBannerDetailByListingId(UploadImagesVM.ListingId);
                    UploadImagesVM.CertificateImages = await listingService.GetCertificateDetailsByListingId(UploadImagesVM.ListingId);
                    UploadImagesVM.ClientImages = await listingService.GetClientDetailsByListingId(UploadImagesVM.ListingId);
                }
            }
            catch (Exception exc)
            {

            }
        }

        public void ExecuteStateHasChanged() { }

        #region Logo Image
        public void SetLogoImage(InputFileChangeEventArgs e)
        {
            UploadImagesVM.LogoImage = e.File.OpenReadStream();
        }

        public async Task UploadLogoImage()
        {
            if (!UploadImagesVM.isLogoValid())
            {
                helper.ShowNotification(_notice, "Please select the image to upload!", NotificationType.Info);
                return;
            }

            UploadImagesVM.LogoImageUrl = await helperFunction.UploadLogoImage(UploadImagesVM.LogoImage, UploadImagesVM.OwnerId);
            UploadImagesVM.LogoImage = null;
            bool isUpdated = await listingService.AddOrUpdateLogoImage(UploadImagesVM);

            if (isUpdated)
            {
                await listingService.UpdateListingStepByOwnerId(UploadImagesVM.OwnerId, BOL.Constants.UploadImageComplete, UploadImagesVM.step);
                helper.ShowNotification(_notice, "Logo Image uploaded successfully!");
            }
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
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
            if (!UploadImagesVM.isOwnerValid() || !UploadImagesVM.LWAddressVM.isValidCountryState() || !UploadImagesVM.ReligionsDropdownVM.isValid())
            {
                helper.ShowNotification(_notice, "All fields are compulsary!", NotificationType.Info);
                return;
            }
            UploadImagesVM.OwnerImageDetail.ImageUrl = helperFunction.GetOwnerImageFilePath(UploadImagesVM.OwnerId);
            bool isUpdated = await listingService.AddOwnerImage(UploadImagesVM);
            await helperFunction.UploadImage(UploadImagesVM.OwnerImage, UploadImagesVM.OwnerImageDetail.ImageUrl);
            resetOwnerImageDetail();

            if (isUpdated)
            {
                await listingService.UpdateListingStepByOwnerId(UploadImagesVM.OwnerId, BOL.Constants.UploadImageComplete, UploadImagesVM.step);
                helper.ShowNotification(_notice, "Owner Image uploaded successfully!");
            }
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
        }

        public async Task DeleteOwnerImage(int id)
        {
            bool isDeleted = await listingService.DeleteOwnerImage(id);
            var imageDetail = UploadImagesVM.OwnerImages.Where(x => x.Id == id).FirstOrDefault();
            if (imageDetail != null)
            {
                UploadImagesVM.OwnerImages.Remove(imageDetail);
                FileManagerService.DeletFile(imageDetail.ImageUrl);
            }

            if (isDeleted)
                helper.ShowNotification(_notice, "Owner Image deleted successfully!");
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
        }

        private void resetOwnerImageDetail()
        {
            UploadImagesVM.OwnerImage = null;
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
            if (!UploadImagesVM.isGalleryValid())
            {
                helper.ShowNotification(_notice, "All fields are compulsary!", NotificationType.Info);
                return;
            }
            UploadImagesVM.GalleryImageDetail.ImageUrl = helperFunction.GetGalleryImageFilePath(UploadImagesVM.OwnerId);
            bool isUpdated = await listingService.AddGalleryImage(UploadImagesVM);
            await helperFunction.UploadImage(UploadImagesVM.GalleryImage, UploadImagesVM.GalleryImageDetail.ImageUrl);
            UploadImagesVM.GalleryImageDetail = new ImageDetails();
            UploadImagesVM.GalleryImage = null;

            if (isUpdated)
            {
                await listingService.UpdateListingStepByOwnerId(UploadImagesVM.OwnerId, BOL.Constants.UploadImageComplete, UploadImagesVM.step);
                helper.ShowNotification(_notice, "Gallery Image uploaded successfully!");
            }
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
        }

        public async Task DeleteGalleryImage(int id)
        {
            bool isDeleted = await listingService.DeleteGalleryImage(id);
            var imageDetail = UploadImagesVM.GalleryImages.FirstOrDefault(x => x.Id == id);
            if (imageDetail != null)
            {
                UploadImagesVM.GalleryImages.Remove(imageDetail);
                FileManagerService.DeletFile(imageDetail.ImageUrl);
            }

            if (isDeleted)
                helper.ShowNotification(_notice, "Gallery Image deleted successfully!");
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
        }
        #endregion

        #region Banner Image
        public void SetBannerImage(InputFileChangeEventArgs e)
        {
            UploadImagesVM.BannerImage = e.File.OpenReadStream();
        }

        public async Task UploadBannerImage()
        {
            if (!UploadImagesVM.isBannerValid())
            {
                helper.ShowNotification(_notice, "Please select the image to upload!", NotificationType.Info);
                return;
            }

            UploadImagesVM.BannerImageDet.ImageUrl = helperFunction.GetBannerImageFilePath(UploadImagesVM.OwnerId);
            UploadImagesVM.BannerImageDetail = await listingService.AddOrUpdateBannerImage(UploadImagesVM);
            await helperFunction.UploadImage(UploadImagesVM.BannerImage, UploadImagesVM.BannerImageDet.ImageUrl);
            UploadImagesVM.BannerImageDet = new ImageDetails();
            UploadImagesVM.BannerImage = null;

            if (UploadImagesVM.BannerImageDetail != null)
            {
                await listingService.UpdateListingStepByOwnerId(UploadImagesVM.OwnerId, BOL.Constants.UploadImageComplete, UploadImagesVM.step);
                helper.ShowNotification(_notice, "Banner Image uploaded successfully!");
            }
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
        }
        #endregion

        #region Certificate Image
        public void SetCertificateImage(InputFileChangeEventArgs e)
        {
            UploadImagesVM.CertificateImage = e.File.OpenReadStream();
        }

        public async Task UploadCertificateImage()
        {
            if (!UploadImagesVM.isCertificateValid())
            {
                helper.ShowNotification(_notice, "All fields are compulsary!", NotificationType.Info);
                return;
            }
            UploadImagesVM.CertificateImageDetail.ImageUrl = helperFunction.GetCertificateImageFilePath(UploadImagesVM.OwnerId);
            bool isUpdated = await listingService.AddCertificateDetail(UploadImagesVM);
            await helperFunction.UploadImage(UploadImagesVM.CertificateImage, UploadImagesVM.CertificateImageDetail.ImageUrl);
            UploadImagesVM.CertificateImageDetail = new ImageDetails();
            UploadImagesVM.CertificateImage = null;

            if (isUpdated)
            {
                await listingService.UpdateListingStepByOwnerId(UploadImagesVM.OwnerId, BOL.Constants.UploadImageComplete, UploadImagesVM.step);
                helper.ShowNotification(_notice, "Certificate Image uploaded successfully!");
            }
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
        }

        public async Task DeleteCertificateImage(int id)
        {
            bool isDeleted = await listingService.DeleteCertificateDetail(id);
            var imageDetail = UploadImagesVM.CertificateImages.FirstOrDefault(x => x.Id == id);
            if (imageDetail != null)
            {
                UploadImagesVM.CertificateImages.Remove(imageDetail);
                FileManagerService.DeletFile(imageDetail.ImageUrl);
            }

            if (isDeleted)
                helper.ShowNotification(_notice, "Certificate Image deleted successfully!");
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
        }
        #endregion

        #region Client Image
        public void SetClientImage(InputFileChangeEventArgs e)
        {
            UploadImagesVM.ClientImage = e.File.OpenReadStream();
        }

        public async Task UploadClientImage()
        {
            if (!UploadImagesVM.isClientValid())
            {
                helper.ShowNotification(_notice, "All fields are compulsary!", NotificationType.Info);
                return;
            }
            UploadImagesVM.ClientImageDetail.ImageUrl = helperFunction.GetClientImageFilePath(UploadImagesVM.OwnerId);
            bool isUpdated = await listingService.AddClientDetail(UploadImagesVM);
            await helperFunction.UploadImage(UploadImagesVM.ClientImage, UploadImagesVM.ClientImageDetail.ImageUrl);
            UploadImagesVM.ClientImageDetail = new ImageDetails();
            UploadImagesVM.ClientImage = null;

            if (isUpdated)
            {
                await listingService.UpdateListingStepByOwnerId(UploadImagesVM.OwnerId, BOL.Constants.UploadImageComplete, UploadImagesVM.step);
                helper.ShowNotification(_notice, "Client Image uploaded successfully!");
            }
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
        }

        public async Task DeleteClientImage(int id)
        {
            bool isDeleted = await listingService.DeleteClientDetail(id);
            var imageDetail = UploadImagesVM.ClientImages.FirstOrDefault(x => x.Id == id);
            if (imageDetail != null)
            {
                UploadImagesVM.ClientImages.Remove(imageDetail);
                FileManagerService.DeletFile(imageDetail.ImageUrl);
            }

            if (isDeleted)
                helper.ShowNotification(_notice, "Client Image deleted successfully!");
            else
                helper.ShowNotification(_notice, "Something went worng, please contact Administrator!", NotificationType.Error);
        }
        #endregion
    }
}
