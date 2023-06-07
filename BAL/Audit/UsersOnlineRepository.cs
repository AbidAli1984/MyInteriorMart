using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BOL.AUDITTRAIL;
using DAL.AUDIT;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BOL.VIEWMODELS;

namespace BAL.Audit
{
    public class UsersOnlineRepository : IUsersOnlineRepository
    {
        private readonly AuditDbContext auditContext;
        private readonly UserManager<IdentityUser> userManager;
        public UsersOnlineRepository(AuditDbContext auditContext, UserManager<IdentityUser> userManager)
        {
            this.auditContext = auditContext;
            this.userManager = userManager;
        }

        public async Task AddUserAsync(string UserID, string Name, string ConnectionID)
        {
            UsersOnline user = new UsersOnline();
            user.UserID = UserID;
            user.Name = Name;
            user.ConnectionID = ConnectionID;

            auditContext.UsersOnline.Add(user);
            await auditContext.SaveChangesAsync();
        }

        public async Task RemoveUserAsync(string ConnectionID)
        {
            var user = await auditContext.UsersOnline.Where(i => i.ConnectionID == ConnectionID).FirstOrDefaultAsync();
            auditContext.UsersOnline.Remove(user);
            await auditContext.SaveChangesAsync();
        }

        public async Task<bool> CheckIfUserOnlineByConnectionID(string ConnectionID)
        {
            var userOnline = await auditContext.UsersOnline.Where(i => i.ConnectionID == ConnectionID).AnyAsync();
            return userOnline;
        }

        public async Task<bool> CheckIfUserOnlineByUserID(string UserID)
        {
            var userOnline = await auditContext.UsersOnline.Where(i => i.UserID == UserID).AnyAsync();
            return userOnline;
        }

        public async Task<UsersOnline> FindUserByConnectionIDAsync(string ConnectionID)
        {
            var user = await auditContext.UsersOnline.Where(i => i.ConnectionID == ConnectionID).FirstOrDefaultAsync();
            return user;
        }

        public async Task<UsersOnline> FindUserByUserIDAsync(string UserID)
        {
            var user = await auditContext.UsersOnline.Where(i => i.UserID == UserID).FirstOrDefaultAsync();
            return user;
        }

        public async Task<IList<string>> GetConnectionIdListBelongToUserAsync(string UserID)
        {
            IList<string> connectionIds = new List<string>();
            var connectionForUser = await auditContext.UsersOnline.Where(i => i.UserID == UserID).Select(i => i.ConnectionID).ToListAsync();
            foreach(var connection in connectionForUser)
            {
                connectionIds.Add(connection);
            }

            return connectionIds;
        }

        // Shafi: Create repository for NotificationEntityViewModel
        // new NotificationEntitiesTypes can be added here in future if required
        public IList<NotificationEntityTypeViewModel> GetNotificationEntityTypeList()
        {
            IList<NotificationEntityTypeViewModel> entityList = new List<NotificationEntityTypeViewModel>();
            NotificationEntityTypeViewModel listing = new NotificationEntityTypeViewModel() { NotificationEntityType = "LISTING" };
            NotificationEntityTypeViewModel enquiry = new NotificationEntityTypeViewModel() { NotificationEntityType = "ENQUIRY" };
            NotificationEntityTypeViewModel chat = new NotificationEntityTypeViewModel() { NotificationEntityType = "CHAT" };

            entityList.Add(listing);
            entityList.Add(enquiry);
            entityList.Add(chat);

            return entityList;
        }
        // End:

        // Shafi: Get notification entity type name
        public string GetNotificationEntityTypeName(string entityTypeName)
        {
            var entityList = GetNotificationEntityTypeList();
            var result = entityList.Where(i => i.NotificationEntityType == entityTypeName).FirstOrDefault();
            return result.NotificationEntityType;
        }
        // End:

        // Shafi: Create Listing Notification Record in Table
        public async Task CreateListingNotificationAsync(string actorGUID, string notifierGUID, string entityType, int entityID, string action, string description)
        {
            ListingNotification notification = new ListingNotification()
            {
                DateTime = DateTime.Now,
                ActorGUID = actorGUID,
                NotifierGUID = notifierGUID,
                EntityType = entityType,
                EntityID = entityID,
                Action = action,
                Description = description
            };

            auditContext.ListingNotification.Add(notification);
            await auditContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ListingNotification>> GetLatest10UnreadNotificationsOfNotifierAsync(string notifierUserName)
        {
            var user = await userManager.FindByNameAsync(notifierUserName);
            string notifierGUID = user.Id;

            var result = await auditContext.ListingNotification.Where(i => i.NotifierGUID == notifierGUID && i.MarkAsRead == false).OrderBy(i => i.ListingNotificationID).OrderByDescending(i => i.ListingNotificationID).Take(10).ToListAsync();

            return result;
        }

        public async Task<int> CountAllUnreadNotificationsOfNotifierAsync(string notifierUserName)
        {
            var user = await userManager.FindByNameAsync(notifierUserName);
            string notifierGUID = user.Id;

            var count = await auditContext.ListingNotification.Where(i => i.NotifierGUID == notifierGUID && i.MarkAsRead == false).CountAsync();
            return count;
        }

        public Task<IEnumerable<ListingNotification>> GetAllUnreadNotificationsOfNotifierAsync(string notifierUserName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ListingNotification>> GetAllReadNotificationsOfNotifierAsync(string notifierUserName)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ListingNotification>> GetAllNotificationsOfNotifierAsync(string notifierUserName)
        {
            throw new NotImplementedException();
        }

        public async Task NotificationMarkAsReadToggleAsyn(int notificationID, string entityType)
        {
            // Shafi: Find notification
            var notification = await auditContext.ListingNotification.Where(i => i.ListingNotificationID == notificationID && i.EntityType == entityType).FirstOrDefaultAsync();
            // End:

            // Shafi: Get Mark As Read Value
            var markAsRead = notification.MarkAsRead;
            // End:

            // Shafi: Toggle Mark As Read Value
            if(markAsRead == false)
            {
                notification.MarkAsRead = true;
            }
            else
            {
                notification.MarkAsRead = false;
            }
            // End:

            // Shafi: Add notification to auditContext and save changes
            auditContext.Update(notification);
            await auditContext.SaveChangesAsync();
            // End:
        }

        public Task NotificationMarkAllAsReadAsync(List<int> listNotificationID, List<string> listEntityType)
        {
            throw new NotImplementedException();
        }

        public async Task NotificationMarkSingleAsReadAsync(int notificationId)
        {
            var notification = await auditContext.ListingNotification.Where(i => i.ListingNotificationID == notificationId).FirstOrDefaultAsync();
            notification.MarkAsRead = true;
            auditContext.Update(notification);
            await auditContext.SaveChangesAsync();
        }
    }
}
