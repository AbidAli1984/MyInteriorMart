using AntDesign;
using BAL.FileManager;
using BAL.Services.Contracts;
using BOL;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.ComponentModels.MyAccount.Profile;
using BOL.LISTING;
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


        public void NavigateToPageByStep(Listing listing, int previousStep, NavigationManager navManager)
        {
            if (listing == null)
                navManager.NavigateTo("/MyAccount/Listing/Company");
            else if (listing.Steps < previousStep)
            {
                if (listing.Steps < Constants.CompanyComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Company");
                else if (listing.Steps < Constants.CommunicationComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Communication");
                else if (listing.Steps < Constants.AddressComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Address");
                else if (listing.Steps < Constants.CompanyComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Communication");
                else if (listing.Steps < Constants.CompanyComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Communication");
                else if (listing.Steps < Constants.CompanyComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Communication");
                else if (listing.Steps < Constants.CompanyComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Communication");
            }
        }

    }
}
