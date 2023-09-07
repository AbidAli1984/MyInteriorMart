using BAL.Services.Contracts;
using BOL.AUDITTRAIL;
using BOL.LISTING;
using BOL.VIEWMODELS;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IListingRepository _listingRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public AuditService(IAuditRepository auditRepository, IListingRepository listingRepository, IUserProfileRepository userProfileRepository)
        {
            _auditRepository = auditRepository;
            _listingRepository = listingRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task AddAsync(object data)
        {
            await _auditRepository.AddAsync(data);
        }

        public async Task UpdateAsync(object data)
        {
            await _auditRepository.UpdateAsync(data);
        }

        public async Task<Bookmarks> GetBookmarkByListingAndUserId(int listingId, string userGuid)
        {
            return await _auditRepository.GetBookmarkByListingAndUserId(listingId, userGuid);
        }

        public async Task<ListingLikeDislike> GetLikeByListingAndUserId(int listingId, string userGuid)
        {
            return await _auditRepository.GetLikeByListingAndUserId(listingId, userGuid);
        }

        public async Task<Subscribes> GetSubscribeByListingAndUserId(int listingId, string userGuid)
        {
            return await _auditRepository.GetSubscribeByListingAndUserId(listingId, userGuid);
        }

        public async Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid)
        {
            var bookmark = await GetBookmarkByListingAndUserId(listingId, userGuid);
            return bookmark != null && bookmark.Bookmark;
        }

        public async Task<bool> CheckIfUserLikedListing(int listingId, string userGuid)
        {
            var like = await GetLikeByListingAndUserId(listingId, userGuid);
            return like != null && like.Like;
        }

        public async Task<bool> CheckIfUserSubscribedToListing(int listingId, string userGuid)
        {
            var subscribe = await GetSubscribeByListingAndUserId(listingId, userGuid);
            return subscribe != null && subscribe.Subscribe;
        }

        public async Task<IList<ListingActivityVM>> GetLikesByOwnerIdAsync(string ownerId)
        {
            return await GetListingActivity(ownerId, Constants.Like);
        }

        public async Task<IList<ListingActivityVM>> GetBookmarksByOwnerIdAsync(string ownerId)
        {
            return await GetListingActivity(ownerId, Constants.Bookmark);
        }

        public async Task<IList<ListingActivityVM>> GetSubscribesByOwnerIdAsync(string ownerId)
        {
            return await GetListingActivity(ownerId, Constants.Subscribe);
        }

        private async Task<IList<ListingActivityVM>> GetListingActivity(string ownerId, int activityType, bool isNotification = false)
        {
            var listing = await _listingRepository.GetListingByOwnerId(ownerId);
            string activityText = string.Empty;
            IEnumerable<ListingActivity> ListingActivity = null;

            if (activityType == Constants.Like)
            {
                activityText = "Liked";
                ListingActivity = await _auditRepository.GetLikesByListingId(listing.ListingID);
            }
            else if (activityType == Constants.Bookmark)
            {
                activityText = "Bookmarked";
                ListingActivity = await _auditRepository.GetBookmarksByListingId(listing.ListingID);
            }
            else if (activityType == Constants.Subscribe)
            {
                activityText = "Subscribed";
                ListingActivity = await _auditRepository.GetSubscriberByListingId(listing.ListingID);
            }

            if (ListingActivity == null)
                return null;

            var listingActivityVMs = ListingActivity.Select(x => new ListingActivityVM
            {
                OwnerGuid = x.UserGuid,
                CompanyName = listing.CompanyName,
                VisitDate = x.VisitDate.ToString(Constants.dateFormat1),
                ActivityType = activityType,
                ActivityText = activityText,
                isNotification = isNotification
            }).ToList();

            foreach (var like in listingActivityVMs)
            {
                var profile = await _userProfileRepository.GetProfileByOwnerGuid(like.OwnerGuid);

                if (profile != null)
                {
                    like.UserName = profile.Name;
                    like.UserImage = string.IsNullOrWhiteSpace(profile.ImageUrl) ? "resources/img/icon/profile.svg" : profile.ImageUrl;
                }
            }
            return listingActivityVMs;
        }

        public async Task<IList<ListingActivityVM>> GetNotificationByOwnerIdAsync(string ownerId)
        {
            var activitySubscribe = await GetListingActivity(ownerId, Constants.Subscribe, true);
            var activityLikes = await GetListingActivity(ownerId, Constants.Like, true);
            var activityBookmarks = await GetListingActivity(ownerId, Constants.Bookmark, true);

            List<ListingActivityVM> listingActivityVMs = new List<ListingActivityVM>();

            if (activitySubscribe != null)
                listingActivityVMs.AddRange(activitySubscribe);

            if (activityLikes != null)
                listingActivityVMs.AddRange(activityLikes);

            if (activityBookmarks != null)
                listingActivityVMs.AddRange(activityBookmarks);

            return listingActivityVMs.OrderByDescending(x => x.VisitDate).ToList();
        }
    }
}
