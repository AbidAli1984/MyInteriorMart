using BOL.AUDITTRAIL;
using DAL.AUDIT;
using DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AuditDbContext _auditDbContext;
        public AuditRepository(AuditDbContext auditDbContext)
        {
            this._auditDbContext = auditDbContext;
        }

        public async Task<bool> CheckIfUserSubscribedToListing(int listingId, string userGuid)
        {
            return await _auditDbContext.Subscribes
                .AnyAsync(i => i.ListingID == listingId && i.UserGuid == userGuid && i.Subscribe);
        }

        public async Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid)
        {
            return await _auditDbContext.Bookmarks
                .AnyAsync(i => i.ListingID == listingId && i.UserGuid == userGuid && i.Bookmark);
        }

        public async Task<bool> CheckIfUserLikedListing(int listingId, string userGuid)
        {
            return await _auditDbContext.ListingLikeDislike
                .AnyAsync(i => i.ListingID == listingId && i.UserGuid == userGuid && i.Like);
        }

        public async Task<IEnumerable<Subscribes>> GetSubscriberByListingId(int listingId)
        {
            return await _auditDbContext.Subscribes
                .Where(i => i.ListingID == listingId && i.Subscribe)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bookmarks>> GetBookmarksByListingId(int listingId)
        {
            return await _auditDbContext.Bookmarks
                .Where(i => i.ListingID == listingId && i.Bookmark)
                .ToListAsync();
        }

        public async Task<IEnumerable<ListingLikeDislike>> GetLikesByListingId(int listingId)
        {
            return await _auditDbContext.ListingLikeDislike
                .Where(i => i.ListingID == listingId && i.Like)
                .ToListAsync();
        }
    }
}
