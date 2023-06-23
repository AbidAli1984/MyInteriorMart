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
            var subscribed = await _auditDbContext.Subscribes.Where(i => i.ListingID == listingId && i.UserGuid == userGuid).AnyAsync();

            return subscribed;
        }

        public async Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid)
        {
            var bookmarked = await _auditDbContext.Bookmarks.Where(i => i.ListingID == listingId && i.UserGuid == userGuid).AnyAsync();

            return bookmarked;
        }

        public async Task<bool> CheckIfUserLikedListing(int listingId, string userGuid)
        {
            var liked = await _auditDbContext.ListingLikeDislike.Where(i => i.ListingID == listingId && i.UserGuid == userGuid).AnyAsync();

            return liked;
        }
    }
}
