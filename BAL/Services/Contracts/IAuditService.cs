using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IAuditService
    {
        Task AddAsync(object data);
        Task UpdateAsync(object data);

        Task<Bookmarks> GetBookmarkByListingAndUserId(int listingId, string userGuid);
        Task<ListingLikeDislike> GetLikeByListingAndUserId(int listingId, string userGuid);
        Task<Subscribes> GetSubscribeByListingAndUserId(int listingId, string userGuid);

        Task<bool> CheckIfUserSubscribedToListing(int listingId, string userGuid);
        Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid);
        Task<bool> CheckIfUserLikedListing(int listingId, string userGuid);

        Task<IList<ListingActivityVM>> GetLikesByOwnerIdAsync(string ownerId);

        Task<IList<ListingActivityVM>> GetBookmarksByOwnerIdAsync(string ownerId);

        Task<IList<ListingActivityVM>> GetSubscribesByOwnerIdAsync(string ownerId);

        Task<IList<ListingActivityVM>> GetNotificationByOwnerIdAsync(string ownerId);

        Task<IList<ListingActivityVM>> GetListingLikesByUserIdAsync(string userId);
        Task<IList<ListingActivityVM>> GetListingBookmarksByUserIdAsync(string userId);
        Task<IList<ListingActivityVM>> GetListingSubscribesByUserIdAsync(string userId);

        Task<ListingActivityCount> GetListingActivityCountsByOwnerId(string ownerId);
        Task<ListingActivityCount> GetListingActivityCountsByUserId(string userId);
    }
}
