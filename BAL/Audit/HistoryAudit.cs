using BOL.AUDITTRAIL;
using BOL.LISTING;
using DAL.AUDIT;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Audit
{
    public class HistoryAudit : IHistoryAudit
    {
        private readonly AuditDbContext auditContext;
        public HistoryAudit(AuditDbContext auditContext)
        {
            this.auditContext = auditContext;
        }

        public async Task CreateUserHistoryAsync(string userGuid, string email, string mobile, string ipAddress, string userRole, string visitDate, string visitTime, string userAgent, string referrerURL, string visitedURL, string activity)
        {
            // Shafi: Get user history details
            UserHistory userHistory = new UserHistory();
            userHistory.UserGuid = userGuid;
            userHistory.Email = email;
            userHistory.Mobile = mobile;
            userHistory.IPAddress = ipAddress;
            userHistory.UserRole = userRole;
            userHistory.VisitDate = Convert.ToDateTime(visitDate);
            userHistory.VisitTime = Convert.ToDateTime(visitTime);
            userHistory.UserAgent = userAgent;
            userHistory.ReferrerURL = referrerURL;
            userHistory.VisitedURL = visitedURL;
            userHistory.Activity = activity;

            // Shafi: Save user history
            auditContext.Add(userHistory);
            await auditContext.SaveChangesAsync();
            // End:
        }

        public async Task CreateListingLastUpdatedAsync(int listingID, string updatedByGuid, string email, string mobile, string ipAddress, string userRole, string sectionUpdated, string updatedDate, string updatedTime, string updatedUrl, string userAgent, string activity)
        {
            // Shafi: Get listing last updated details
            ListingLastUpdated update = new ListingLastUpdated();
            update.ListingID = listingID;
            update.UpdatedByGuid = updatedByGuid;
            update.Email = email;
            update.Mobile = mobile;
            update.IPAddress = ipAddress;
            update.UserRole = userRole;
            update.SectionUpdated = sectionUpdated;
            update.UpdatedDate = Convert.ToDateTime(updatedDate);
            update.UpdatedTime = Convert.ToDateTime(updatedTime);
            update.UpdatedURL = updatedUrl;
            update.UserAgent = userAgent;
            update.Activity = activity;
            // End:

            // Shafi: Save listing last updated record
            auditContext.Add(update);
            await auditContext.SaveChangesAsync();
            // End:
        }

        public async Task CreateListingLikeDislikeAsync(int ListingID, string UserGuid, string Email, string Mobile, string IPAddress, string UserRole, string VisitDate, string VisitTime, string UserAgent, bool Like)
        {
            var likeDislike = new ListingLikeDislike
            {
                ListingID = ListingID,
                UserGuid = UserGuid,
                Email = Email,
                Mobile = Mobile,
                IPAddress = IPAddress,
                UserRole = UserRole,
                VisitDate = Convert.ToDateTime(VisitDate),
                VisitTime = Convert.ToDateTime(VisitTime),
                UserAgent = UserAgent,
                Like = Like
            };
            await auditContext.AddAsync(likeDislike);
            await auditContext.SaveChangesAsync();
        }

        public async Task EditListingLikeDislikeAsync(int ListingID, string UserGuid, string VisitDate, string VisitTime, bool Like)
        {
            var likeDislike = await auditContext.ListingLikeDislike.Where(l => l.ListingID == ListingID && l.UserGuid == UserGuid).FirstOrDefaultAsync();

            // Shafi: Set value
            likeDislike.Like = Like;
            likeDislike.VisitDate = Convert.ToDateTime(VisitDate);
            likeDislike.VisitTime = Convert.ToDateTime(VisitTime);
            likeDislike.Like = Like;
            // End:

            // Shafi: Update Like
            auditContext.Update(likeDislike);
            await auditContext.SaveChangesAsync();
            // End:
        }

        public async Task<bool> ListingLikeByUser(int ListingID, string UserGuid)
        {
            var result = await auditContext.ListingLikeDislike.Where(l => l.UserGuid == UserGuid && l.ListingID == ListingID).FirstOrDefaultAsync();

            if (result != null && result.Like == true)
            {
                return true;
            }
            else if(result != null && result.Like == false)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        public async Task CreateBookmarkAsync(int ListingID, string UserGuid, string Email, string Mobile, string IPAddress, string UserRole, string VisitDate, string VisitTime, string UserAgent, bool Bookmark)
        {
            var bookmark = new Bookmarks
            {
                ListingID = ListingID,
                UserGuid = UserGuid,
                Email = Email,
                Mobile = Mobile,
                IPAddress = IPAddress,
                UserRole = UserRole,
                VisitDate = Convert.ToDateTime(VisitDate),
                VisitTime = Convert.ToDateTime(VisitTime),
                UserAgent = UserAgent,
                Bookmark = Bookmark
            };
            await auditContext.AddAsync(bookmark);
            await auditContext.SaveChangesAsync();
        }

        public async Task EditBookmarkAsync(int ListingID, string UserGuid, string VisitDate, string VisitTime, bool Bookmark)
        {
            var bookmark = await auditContext.Bookmarks.Where(l => l.ListingID == ListingID && l.UserGuid == UserGuid).FirstOrDefaultAsync();

            // Shafi: Set value
            bookmark.VisitDate = Convert.ToDateTime(VisitDate);
            bookmark.VisitTime = Convert.ToDateTime(VisitTime);
            bookmark.Bookmark = Bookmark;
            // End:

            // Shafi: Update Like
            auditContext.Update(bookmark);
            await auditContext.SaveChangesAsync();
            // End:
        }

        public async Task<bool> ListingBookmarkByUser(int ListingID, string UserGuid)
        {
            var result = await auditContext.Bookmarks.Where(l => l.UserGuid == UserGuid && l.ListingID == ListingID).FirstOrDefaultAsync();

            if (result != null && result.Bookmark == true)
            {
                return true;
            }
            else if (result != null && result.Bookmark== false)
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        public async Task CreateSubscribeAsync(int ListingID, string UserGuid, string Email, string Mobile, string IPAddress, string UserRole, string VisitDate, string VisitTime, string UserAgent, bool Subscribe)
        {
            var subscribe = new Subscribes
            {
                ListingID = ListingID,
                UserGuid = UserGuid,
                Email = Email,
                Mobile = Mobile,
                IPAddress = IPAddress,
                UserRole = UserRole,
                VisitDate = Convert.ToDateTime(VisitDate),
                VisitTime = Convert.ToDateTime(VisitTime),
                UserAgent = UserAgent,
                Subscribe = Subscribe
            };
            await auditContext.AddAsync(subscribe);
            await auditContext.SaveChangesAsync();
        }

        public async Task EditSubscribeAsync(int ListingID, string UserGuid, string VisitDate, string VisitTime, bool Subscribe)
        {
            var subscribe = await auditContext.Subscribes.Where(l => l.ListingID == ListingID && l.UserGuid == UserGuid).FirstOrDefaultAsync();

            // Shafi: Set value
            subscribe.VisitDate = Convert.ToDateTime(VisitDate);
            subscribe.VisitTime = Convert.ToDateTime(VisitTime);
            subscribe.Subscribe = Subscribe;
            // End:

            // Shafi: Update Like
            auditContext.Update(subscribe);
            await auditContext.SaveChangesAsync();
            // End:
        }

        public async Task<bool> ListingSubscribeByUser(int ListingID, string UserGuid)
        {
            var result = await auditContext.Subscribes.Where(l => l.UserGuid == UserGuid && l.ListingID == ListingID).FirstOrDefaultAsync();

            if (result != null && result.Subscribe == true)
            {
                return true;
            }
            else if (result != null && result.Subscribe == false)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
