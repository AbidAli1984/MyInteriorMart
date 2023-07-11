using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using BAL.Audit;
using DAL.AUDIT;
using DAL.LISTING;
using Humanizer;
using BAL.Services.Contracts;
using BOL.IDENTITY;
using DAL.Models;

namespace HUBS.Notifications
{
    [Authorize]
    public class NotificationUserDashboardHub : Hub
    {
        private readonly IUsersOnlineRepository usersOnlineRepo;
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly AuditDbContext auditContext;
        private readonly ListingDbContext listingContext;

        public NotificationUserDashboardHub(IUsersOnlineRepository usersOnlineRepo, IUserService userService, 
            IUserProfileService userProfileService, AuditDbContext auditContext, ListingDbContext listingContext)
        {
            this.usersOnlineRepo = usersOnlineRepo;
            this._userService = userService;
            this._userProfileService = userProfileService;
            this.auditContext = auditContext;
            this.listingContext = listingContext;
        }

        public async Task<ApplicationUser> GetUser()
        {
            var user = await _userService.GetUserByUserName(Context.User.Identity.Name);
            return user;
        }

        public async Task<UserProfile> GetUserProfile()
        {
            var user = GetUser();
            var profile = await _userProfileService.GetProfileByOwnerGuid(user.Id.ToString());
            return profile;
        }       

        public async Task LikeUnlikeAsync(string likeDislikeId, string action, string ListingID)
        {
            // Shafi: Convert string to int
            var ID = Int16.Parse(likeDislikeId);
            var ListingIDInt = Int16.Parse(ListingID);
            // End:

            // Shafi: Find owner of listing
            var listing = await listingContext.Listing.Where(i => i.ListingID == ListingIDInt).FirstOrDefaultAsync();
            string notifierGuid = listing.OwnerGuid;
            string listingCompanyName = listing.CompanyName;
            // End:

            // Shafi: Get Listing Owner (Notifier) Email
            var notifier = await _userService.GetUserById(notifierGuid);
            var notifierEmail = notifier.UserName;
            // End:

            // Shafi: Check if notifier is online currenly
            var notifierOnline = await usersOnlineRepo.CheckIfUserOnlineByUserID(notifierEmail);
            // End:

            // Shafi: If notifier is online then execute this
            var connectionIdList = await usersOnlineRepo.GetConnectionIdListBelongToUserAsync(notifierEmail);
            List<string> notifierConnectionIds = new List<string>();
            foreach(var connection in connectionIdList)
            {
                notifierConnectionIds.Add(connection);
            }
            // End

            // Shafi: Find ListingLikeDislike record by ID
            var userGuid = await auditContext.ListingLikeDislike.Where(i => i.LikeDislikeID == ID).Select(i => i.UserGuid).FirstOrDefaultAsync();
            // End:

            // Shafi: Get profile of user who liked listing
            var profile = await _userProfileService.GetProfileByOwnerGuid(userGuid);
            // End:

            // Shafi: Initialize a variable
            string imageId = "";
            string name = "";
            // End:

            // Shafi: If profile is exists executive this
            if (profile != null)
            {
                // Get profile id
                imageId = profile.ProfileID.ToString();
                // End:

                // Get name of person who liked listing
                name = profile.Name;
                // End:
            }
            else
            {
                // Get profile id
                imageId = "No Image";
                // End:

                // Get name of person who liked listing
                name = "Anonymous user.";
                // End:
            }
            // End:

            // Shafi: Notification message
            string message = $"{action} your <a href='#0' target='_blank'>listing</a>.";
            // End:

            // Shafi: Initialize icon value with empty string
            string icon = "";
            // End:

            // Shafi: Set icon image
            switch (action)
            {
                case "Liked":
                    icon = "/icons-notification-green/like.png";
                    break;
                case "Unliked":
                    icon = "/icons-notification-red/unlike.png";
                    break;
            }
            // End:

            // Shafi: Create Listing Notification Record...........

            // Get Notification Entity Type List
            var entityType = usersOnlineRepo.GetNotificationEntityTypeName("LISTING");
            // End:

            // Get actorGUID
            var actor = await _userService.GetUserByUserName(Context.User.Identity.Name);
            var actorGUID = actor.Id;
            // End:

            await usersOnlineRepo.CreateListingNotificationAsync(actorGUID, notifierGuid, entityType, Int16.Parse(ListingID), action, message);
            // End: ...........

            // Shafi: If notifier is online then send message to clients which belong to notifier
            if (notifierConnectionIds != null)
            {

                DateTime currentDate = DateTime.Now;
                TimeSpan timeSpan = currentDate.Subtract(DateTime.Now);
                var notificationTime = DateTime.UtcNow.AddMinutes(-timeSpan.TotalMinutes).Humanize();
                var unreadNotificationCount = await auditContext.ListingNotification.Where(i => i.NotifierGUID == notifier.Id && i.MarkAsRead == false).CountAsync();

                await Clients.Clients(notifierConnectionIds).SendAsync("NotifyLikeUnlike", likeDislikeId, message, icon, name, imageId, notificationTime, unreadNotificationCount);
            }
            // End:
        }

        public override async Task OnConnectedAsync()
        {
            string UserID = Context.User.Identity.Name;
            string ConnectionID = Context.ConnectionId;
            //var profile = await GetUserProfile();
            string Name = "Test Name";
            await usersOnlineRepo.AddUserAsync(UserID, Name, ConnectionID);
            await Clients.All.SendAsync("Notify", UserID, ConnectionID, Name);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string ConnectionID = Context.ConnectionId;
            await usersOnlineRepo.RemoveUserAsync(ConnectionID);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
