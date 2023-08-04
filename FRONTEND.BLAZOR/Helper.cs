using AntDesign;
using BAL;
using BAL.FileManager;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.ComponentModels.MyAccount.Profile;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR
{
    public class Helper
    {
        public static string dateFormat = "dd/MM/yyyy";
        public static string dbDateFormat = "yyyy-MM-dd";
        public static string tempImagePath = @"\FileManager\tempImages\";
        public static string profileImagesPath = @"\FileManager\ProfileImages\";
        public static string ListingImagesPath = @"\FileManager\ListingImages";

        private ISharedService _sharedService;

        public Helper(ISharedService sharedService)
        {
            _sharedService = sharedService;
        }

        public void ShowNotification(NotificationService _notice, NotificationType type, NotificationPlacement placement, string message, string description)
        {
            _notice.Open(new NotificationConfig()
            {
                Message = message,
                Description = description,
                NotificationType = type,
                Placement = placement
            });
        }

        public DateTime GetCurrentDateTime(string TimeZoneOfCountry = "India Standard Time")
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneOfCountry));
        }
        
        #region Upload or Move Images
        public async Task<string> UploadProfileImage(Stream file, string fileName)
        {
            string filePath = tempImagePath + fileName + ".jpg";
            return await FileManagerService.UploadFile(file, filePath, true);
        }

        public async Task<string> MoveProfileImage(UserProfileVM userProfileVM, string fileName)
        {
            string sourceFile = userProfileVM.ImgUrl.Split("?")[0];
            string destFile = profileImagesPath + fileName + ".jpg";
            return await FileManagerService.MoveFile(sourceFile, destFile);
        }

        public async Task UploadLogoImage(UploadImagesVM UploadImagesVM)
        {
            string filePath = $"{ListingImagesPath}\\{UploadImagesVM.OwnerId}\\LOGO.jpg";
            UploadImagesVM.LogoImageUrl = await FileManagerService.UploadFile(UploadImagesVM.LogoImage, filePath, true);
            UploadImagesVM.LogoImage = null;
        }

        public async Task<string> UploadOwnerOrImage(Stream file, string ownerId, string fileName)
        {
            string filePath = $"{ListingImagesPath}\\{ownerId}\\Owners\\{fileName}.jpg";
            return await FileManagerService.UploadFile(file, filePath, true);
        }

        public async Task<string> UploadGalleryImage(Stream file, string ownerId, string fileName)
        {
            string filePath = $"{ListingImagesPath}\\{ownerId}\\Gallery\\{fileName}.jpg";
            return await FileManagerService.UploadFile(file, filePath, true);
        }
        #endregion  

        public void NavigateToPageByStep(int steps, NavigationManager navManager)
        {
            if (steps < Constants.CompanyComplete)
                navManager.NavigateTo("/MyAccount/Listing/Company");
            else if (steps < Constants.CommunicationComplete)
                navManager.NavigateTo("/MyAccount/Listing/Communication");
            else if (steps < Constants.AddressComplete)
                navManager.NavigateTo("/MyAccount/Listing/Address");
            else if (steps < Constants.CompanyComplete)
                navManager.NavigateTo("/MyAccount/Listing/Communication");
            else if (steps < Constants.CompanyComplete)
                navManager.NavigateTo("/MyAccount/Listing/Communication");
            else if (steps < Constants.CompanyComplete)
                navManager.NavigateTo("/MyAccount/Listing/Communication");
            else if (steps < Constants.CompanyComplete)
                navManager.NavigateTo("/MyAccount/Listing/Communication");
        }

        #region Address Information
        public async Task GetStateByCountryId(AddressVM addressVM, ChangeEventArgs events = null)
        {
            if (events != null)
            {
                addressVM.CountryId = Convert.ToInt32(events.Value.ToString());
                addressVM.StateId = 0;
                addressVM.CityId = 0;
                addressVM.StationId = 0;
                addressVM.PincodeId = 0;
                addressVM.LocalityId = 0;
            }
            addressVM.States.Clear();
            addressVM.Cities.Clear();
            addressVM.Areas.Clear();
            addressVM.Pincodes.Clear();
            addressVM.Localities.Clear();

            if (addressVM.CountryId > 0)
                addressVM.States = await _sharedService.GetStatesByCountryId(addressVM.CountryId);
        }

        public async Task GetCityByStateId(AddressVM addressVM, ChangeEventArgs events = null)
        {
            if (events != null)
            {
                addressVM.StateId = Convert.ToInt32(events.Value.ToString());
                addressVM.CityId = 0;
                addressVM.StationId = 0;
                addressVM.PincodeId = 0;
                addressVM.LocalityId = 0;
            }
            addressVM.Cities.Clear();
            addressVM.Areas.Clear();
            addressVM.Pincodes.Clear();
            addressVM.Localities.Clear();

            if (addressVM.StateId > 0)
                addressVM.Cities = await _sharedService.GetCitiesByStateId(addressVM.StateId);
        }

        public async Task GetAreaByCityId(AddressVM addressVM, ChangeEventArgs events = null)
        {
            if (events != null)
            {
                addressVM.CityId = Convert.ToInt32(events.Value.ToString());
                addressVM.StationId = 0;
                addressVM.PincodeId = 0;
                addressVM.LocalityId = 0;
            }
            addressVM.Areas.Clear();
            addressVM.Pincodes.Clear();
            addressVM.Localities.Clear();

            if (addressVM.CityId > 0)
                addressVM.Areas = await _sharedService.GetAreasByCityId(addressVM.CityId);
        }

        public async Task GetPincodesByAreaId(AddressVM addressVM, ChangeEventArgs events = null)
        {
            if (events != null)
            {
                addressVM.StationId = Convert.ToInt32(events.Value.ToString());
                addressVM.PincodeId = 0;
                addressVM.LocalityId = 0;
            }
            addressVM.Pincodes.Clear();
            addressVM.Localities.Clear();

            if (addressVM.StationId > 0)
                addressVM.Pincodes = await _sharedService.GetPincodesByAreaId(addressVM.StationId);
        }

        public async Task GetLocalitiesByPincodeId(AddressVM addressVM, ChangeEventArgs events = null)
        {
            if (events != null)
            {
                addressVM.PincodeId = Convert.ToInt32(events.Value.ToString());
                addressVM.LocalityId = 0;
            }
            addressVM.Localities.Clear();

            if (addressVM.PincodeId > 0)
                addressVM.Localities = await _sharedService.GetLocalitiesByPincode(addressVM.PincodeId);
        }
        #endregion
    }
}
