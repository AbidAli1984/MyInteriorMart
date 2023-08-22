using BAL.FileManager;
using BAL.Services.Contracts;
using BOL.ComponentModels.Listings;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.ComponentModels.MyAccount.Profile;
using BOL.ComponentModels.Shared;
using DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class HelperFunctions
    {
        private readonly IListingRepository _listingRepository;
        private ISharedService _sharedService;

        public HelperFunctions(IListingRepository listingRepository, ISharedService sharedService)
        {
            _listingRepository = listingRepository;
            _sharedService = sharedService;
        }


        #region Upload or Move Images
        public async Task<string> UploadProfileImage(Stream file, string fileName)
        {
            string filePath = Constants.tempImagePath + fileName + ".jpg";
            return await FileManagerService.UploadFile(file, filePath, true);
        }

        public async Task<string> MoveProfileImage(UserProfileVM userProfileVM, string fileName)
        {
            string sourceFile = userProfileVM.ImgUrl.Split("?")[0];
            string destFile = Constants.profileImagesPath + fileName + ".jpg";
            return await FileManagerService.MoveFile(sourceFile, destFile);
        }

        public async Task<string> UploadLogoImage(Stream file, string ownerId)
        {
            string filePath = $"{Constants.ListingImagesPath}\\{ownerId}\\LOGO.jpg";
            return await FileManagerService.UploadFile(file, filePath, true);
        }

        public string GetOwnerImageFilePath(string ownerId)
        {
            return $"{Constants.ListingImagesPath.Replace("\\", "/")}/{ownerId}/Owners/";
        }

        public string GetGalleryImageFilePath(string ownerId)
        {
            return $"{Constants.ListingImagesPath.Replace("\\", "/")}/{ownerId}/Gallery/";
        }

        public string GetBannerImageFilePath(string ownerId)
        {
            return $"{Constants.ListingImagesPath.Replace("\\", "/")}/{ownerId}/Banner.jpg";
        }

        public async Task UploadImage(Stream file, string filePath)
        {
            filePath = filePath.Replace("/", "\\");
            await FileManagerService.UploadFile(file, filePath, true);
        }
        #endregion  

        #region Address Information
        public async Task GetStatesByCountryId(LWAddressVM lwAddressVM)
        {
            if (lwAddressVM.IsCountryChange)
            {
                lwAddressVM.StateId = 0;
                lwAddressVM.CityId = 0;
                lwAddressVM.LocalityId = 0;
                lwAddressVM.PincodeId = 0;
                lwAddressVM.AreaId = 0;
                lwAddressVM.IsCountryChange = false;
            }
            lwAddressVM.States.Clear();
            lwAddressVM.Cities.Clear();
            lwAddressVM.Localities.Clear();
            lwAddressVM.Pincodes.Clear();
            lwAddressVM.Areas.Clear();

            if (lwAddressVM.CountryId > 0)
                lwAddressVM.States = await _sharedService.GetStatesByCountryId(lwAddressVM.CountryId);
        }

        public async Task GetCitiesByStateId(LWAddressVM lwAddressVM)
        {
            if (lwAddressVM.IsStateChange)
            {
                lwAddressVM.CityId = 0;
                lwAddressVM.LocalityId = 0;
                lwAddressVM.PincodeId = 0;
                lwAddressVM.AreaId = 0;
                lwAddressVM.IsStateChange = false;
            }
            lwAddressVM.Cities.Clear();
            lwAddressVM.Localities.Clear();
            lwAddressVM.Pincodes.Clear();
            lwAddressVM.Areas.Clear();

            if (lwAddressVM.StateId > 0)
                lwAddressVM.Cities = await _sharedService.GetCitiesByStateId(lwAddressVM.StateId);
        }

        public async Task GetLocalitiesByCityId(LWAddressVM lwAddressVM)
        {
            if (lwAddressVM.IsCityChange)
            {
                lwAddressVM.LocalityId = 0;
                lwAddressVM.PincodeId = 0;
                lwAddressVM.AreaId = 0;
                lwAddressVM.IsCityChange = false;
            }
            lwAddressVM.Localities.Clear();
            lwAddressVM.Pincodes.Clear();
            lwAddressVM.Areas.Clear();

            if (lwAddressVM.CityId > 0)
                lwAddressVM.Localities = await _sharedService.GetLocalitiesByCityId(lwAddressVM.CityId);
        }

        public async Task GetPincodesByLocalityId(LWAddressVM lwAddressVM)
        {
            if (lwAddressVM.IsLocalityChange)
            {
                lwAddressVM.PincodeId = 0;
                lwAddressVM.AreaId = 0;
                lwAddressVM.IsLocalityChange = false;
            }
            lwAddressVM.Pincodes.Clear();
            lwAddressVM.Areas.Clear();

            if (lwAddressVM.LocalityId > 0)
                lwAddressVM.Pincodes = await _sharedService.GetPincodesByLocalityId(lwAddressVM.LocalityId);
        }

        public async Task GetAreasByPincodeId(LWAddressVM lwAddressVM)
        {
            if (lwAddressVM.IsPincodeChange)
            {
                lwAddressVM.AreaId = 0;
                lwAddressVM.IsPincodeChange = false;
            }
            lwAddressVM.Areas.Clear();

            if (lwAddressVM.PincodeId > 0)
                lwAddressVM.Areas = await _sharedService.GetAreasByPincodeId(lwAddressVM.PincodeId);
            lwAddressVM.IsFirstLoad = false;
        }
        #endregion

        public async Task GetCastesByReligionId(ReligionsDropdownVM religionsDropdownVM)
        {
            religionsDropdownVM.Castes.Clear();
            if (religionsDropdownVM.SelectedReligionId > 0)
                religionsDropdownVM.Castes = await _sharedService.GetCastesByReligionId(religionsDropdownVM.SelectedReligionId);
        }

        public async Task<BusinessWorkingHour> IsBusinessOpen(int ListingID)
        {
            var workingTime = await _listingRepository.GetWorkingHoursByListingId(ListingID);
            BusinessWorkingHour businessWorking = new BusinessWorkingHour();

            if (workingTime == null)
            {
                businessWorking.IsBusinessOpen = true;
                return businessWorking;
            }

            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");

            if ((day == Constants.Saturday && workingTime.SaturdayHoliday))
            {
                businessWorking.OpenDay = workingTime.SundayHoliday ? Constants.Monday : Constants.Sunday;
                businessWorking.OpenTime = (workingTime.SundayHoliday ? workingTime.MondayFrom : workingTime.SundayFrom).ToString("hh:mm tt");
                return businessWorking;
            }
            else if ((day == Constants.Sunday && workingTime.SundayHoliday))
            {
                businessWorking.OpenDay = Constants.Monday;
                businessWorking.OpenTime = workingTime.MondayFrom.ToString("hh:mm tt");
                return businessWorking;
            }

            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            DateTime OpenTime;
            DateTime CloseTime;

            if (day == Constants.Monday)
            {
                OpenTime = workingTime.MondayFrom;
                CloseTime = workingTime.MondayTo;
                businessWorking.OpenDay = Constants.Tuesday;
            }
            else if (day == Constants.Tuesday)
            {
                OpenTime = workingTime.TuesdayFrom;
                CloseTime = workingTime.TuesdayTo;
                businessWorking.OpenDay = Constants.Wednesday;
            }
            else if (day == Constants.Wednesday)
            {
                OpenTime = workingTime.WednesdayFrom;
                CloseTime = workingTime.WednesdayTo;
                businessWorking.OpenDay = Constants.Thursday;
            }
            else if (day == Constants.Thursday)
            {
                OpenTime = workingTime.ThursdayFrom;
                CloseTime = workingTime.ThursdayTo;
                businessWorking.OpenDay = Constants.Friday;
            }
            else if (day == Constants.Friday)
            {
                OpenTime = workingTime.FridayFrom;
                CloseTime = workingTime.FridayTo;
                if (workingTime.SaturdayHoliday)
                    businessWorking.OpenDay = workingTime.SundayHoliday ? Constants.Monday : Constants.Sunday;
                else
                    businessWorking.OpenDay = Constants.Saturday;
            }
            else if (day == Constants.Saturday)
            {
                OpenTime = workingTime.SaturdayFrom;
                CloseTime = workingTime.SaturdayTo;
                businessWorking.OpenDay = workingTime.SundayHoliday ? Constants.Monday : Constants.Sunday;
            }
            else
            {
                OpenTime = workingTime.SundayFrom;
                CloseTime = workingTime.SundayTo;
                businessWorking.OpenDay = Constants.Monday;
            }

            businessWorking.OpenTime = OpenTime.ToString("hh:mm tt");
            businessWorking.CloseTime = CloseTime.ToString("hh:mm tt");

            DateTime openTime = DateTime.Parse(businessWorking.OpenTime, System.Globalization.CultureInfo.CurrentCulture);
            DateTime closeTime = DateTime.Parse(businessWorking.CloseTime, System.Globalization.CultureInfo.CurrentCulture);
            businessWorking.IsBusinessOpen = currentTime > openTime && currentTime < closeTime;
            return businessWorking;
        }
    }
}
