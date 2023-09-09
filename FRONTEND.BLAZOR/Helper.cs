using AntDesign;
using BAL.FileManager;
using BAL.Services.Contracts;
using BOL;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.ComponentModels.MyAccount.Profile;
using BOL.LISTING;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

        public void ShowNotification(NotificationService _notice, string message, NotificationType type = NotificationType.Success, 
            NotificationPlacement placement = NotificationPlacement.BottomRight)
        {
            _notice.Open(new NotificationConfig()
            {
                Message = type.ToString(),
                Description = message,
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
                else if (listing.Steps < Constants.CategoryComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Category");
                else if (listing.Steps < Constants.SpecialisationComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Specialisation");
                else if (listing.Steps < Constants.WorkingHourComplete)
                    navManager.NavigateTo("/MyAccount/Listing/WorkingHours");
                else if (listing.Steps < Constants.PaymentModeComplete)
                    navManager.NavigateTo("/MyAccount/Listing/PaymentMode");
                else if (listing.Steps < Constants.UploadImageComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Images");
                else if (listing.Steps < Constants.SocialLinkComplete)
                    navManager.NavigateTo("/MyAccount/Listing/SocialLinks");
                else if (listing.Steps < Constants.SEOKeywordComplete)
                    navManager.NavigateTo("/MyAccount/Listing/Keywords");
            }
        }

        public static string GetIpAddress(IHttpContextAccessor httpConAccess)
        {
            return httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
        }

    }
}
