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

        public async Task<object> AddAsync(object data)
        {
            await _auditDbContext.AddAsync(data);
            await _auditDbContext.SaveChangesAsync();
            return data;
        }

        public async Task UpdateAsync(object data)
        {
            _auditDbContext.Update(data);
            await _auditDbContext.SaveChangesAsync();
        }

        public async Task<Subscribes> GetSubscribeByListingAndUserId(int listingId, string userGuid)
        {
            return await _auditDbContext.Subscribes
                .FirstOrDefaultAsync(i => i.ListingID == listingId && i.UserGuid == userGuid);
        }

        public async Task<Bookmarks> GetBookmarkByListingAndUserId(int listingId, string userGuid)
        {
            return await _auditDbContext.Bookmarks
                .FirstOrDefaultAsync(i => i.ListingID == listingId && i.UserGuid == userGuid);
        }

        public async Task<ListingLikeDislike> GetLikeByListingAndUserId(int listingId, string userGuid)
        {
            return await _auditDbContext.ListingLikeDislike
                .FirstOrDefaultAsync(i => i.ListingID == listingId && i.UserGuid == userGuid);
        }


        public async Task<IEnumerable<Subscribes>> GetSubscriberByListingId(int listingId)
        {
            return await _auditDbContext.Subscribes
                .Where(i => i.ListingID == listingId && i.Subscribe)
                .OrderByDescending(i => i.VisitDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Bookmarks>> GetBookmarksByListingId(int listingId)
        {
            return await _auditDbContext.Bookmarks
                .Where(i => i.ListingID == listingId && i.Bookmark)
                .OrderByDescending(i => i.VisitDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ListingLikeDislike>> GetLikesByListingId(int listingId)
        {
            return await _auditDbContext.ListingLikeDislike
                .Where(i => i.ListingID == listingId && i.Like)
                .OrderByDescending(i => i.VisitDate)
                .ToListAsync();
        }
    }
}
