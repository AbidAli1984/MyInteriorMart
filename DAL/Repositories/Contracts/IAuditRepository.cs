using BOL.AUDITTRAIL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IAuditRepository
    {
        Task<object> AddAsync(object data);
        Task UpdateAsync(object data);

        Task<Subscribes> GetSubscribeByListingAndUserId(int listingId, string userGuid);
        Task<Bookmarks> GetBookmarkByListingAndUserId(int listingId, string userGuid);
        Task<ListingLikeDislike> GetLikeByListingAndUserId(int listingId, string userGuid);

        Task<IEnumerable<Subscribes>> GetSubscriberByListingId(int listingId);
        Task<IEnumerable<Bookmarks>> GetBookmarksByListingId(int listingId);
        Task<IEnumerable<ListingLikeDislike>> GetLikesByListingId(int listingId);

        Task<IEnumerable<ListingLikeDislike>> GetLikesByUserId(string userId);
        Task<IEnumerable<Bookmarks>> GetBookmarksByUserId(string userId);
        Task<IEnumerable<Subscribes>> GetSubscriberByUserId(string userId);
    }
}
