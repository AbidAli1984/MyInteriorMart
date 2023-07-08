using AntDesign;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR
{
    public class Helper
    {
        public async Task NoticeWithIcon(NotificationService _notice, NotificationType type, NotificationPlacement placement, string message, string description)
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
