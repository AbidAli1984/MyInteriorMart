using AntDesign;
using BAL.FileManager;
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

        public async Task<string> UploadProfileImage(Stream file, string fileName)
        {
            string filePath = Helper.tempImagePath + fileName + ".jpg";
            string imgUrl = await FileManagerService.UploadFile(file, filePath, true);
            return imgUrl + "?DummyId=" + DateTime.Now.Ticks;
        }
    }
}
