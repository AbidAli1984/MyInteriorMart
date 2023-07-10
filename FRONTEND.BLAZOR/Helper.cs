using AntDesign;
using BAL.FileManager;
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

        public async Task ShowNotification(NotificationService _notice, NotificationType type, NotificationPlacement placement, string message, string description)
        {
            await _notice.Open(new NotificationConfig()
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
    }
}
