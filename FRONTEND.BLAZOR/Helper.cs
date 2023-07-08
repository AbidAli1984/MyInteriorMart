using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR
{
    public class Helper
    {
        public static string dateFormat = "dd/MM/yyyy";
        public static string dbDateFormat = "yyyy-MM-dd";

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
    }
}
