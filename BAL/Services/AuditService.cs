using BAL.Services.Contracts;
using BOL.AUDITTRAIL;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        public AuditService(IAuditRepository auditRepository)
        {
            this._auditRepository = auditRepository;
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
    }
}
