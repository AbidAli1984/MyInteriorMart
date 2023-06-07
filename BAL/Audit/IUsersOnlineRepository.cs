using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Audit
{
    public interface IUsersOnlineRepository
    {
        public Task AddUserAsync(string UserName, string Name, string ConnectionID);
        public Task RemoveUserAsync(string ConnectionID);
        public Task<bool> CheckIfUserOnlineByConnectionID(string ConnectionID);
        public Task<bool> CheckIfUserOnlineByUserID(string UserID);
        public Task<UsersOnline> FindUserByUserIDAsync(string UserID);
        public Task<UsersOnline> FindUserByConnectionIDAsync(string ConnectionID);
        public Task<IList<string>> GetConnectionIdListBelongToUserAsync(string UserID);
        // Shafi: Create repository for NotificationEntityViewModel
        // new NotificationEntitiesTypes can be added here in future if required
        public IList<NotificationEntityTypeViewModel> GetNotificationEntityTypeList();
        // End:
        // Shafi: Get notification entity type name
        public string GetNotificationEntityTypeName(string entityTypeName);
        // End:
        public Task CreateListingNotificationAsync(string actorGUID, string notifierGUID, string entityType, int entityID, string action, string description);
        public Task<IEnumerable<ListingNotification>> GetLatest10UnreadNotificationsOfNotifierAsync(string notifierUserName);
        public Task<int> CountAllUnreadNotificationsOfNotifierAsync(string notifierUserName);
        public Task<IEnumerable<ListingNotification>> GetAllUnreadNotificationsOfNotifierAsync(string notifierUserName);
        public Task<IEnumerable<ListingNotification>> GetAllReadNotificationsOfNotifierAsync(string notifierUserName);
        public Task<IEnumerable<ListingNotification>> GetAllNotificationsOfNotifierAsync(string notifierUserName);
        public Task NotificationMarkAsReadToggleAsyn(int notificationID, string entityType);
        public Task NotificationMarkAllAsReadAsync(List<int> listNotificationID, List<string> listEntityType);
        public Task NotificationMarkSingleAsReadAsync(int notification);
    }
}
