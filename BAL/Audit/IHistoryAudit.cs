using BOL.AUDITTRAIL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Audit
{
    public interface IHistoryAudit
    {
        Task CreateUserHistoryAsync(string userGuid, string email, string mobile, string ipAddress, string userRole, string visitDate, string visitTime, string userAgent, string referrerURL, string visitedURL, string activity);

        Task CreateListingLastUpdatedAsync(int listingID, string updatedByGuid, string email, string mobile, string ipAddress, string userRole, string sectionUpdated, string updatedDate, string updatedTime, string updatedUrl, string userAgent, string activity);


        // Shafi: Manage Like Dislike
        // Create
        Task CreateListingLikeDislikeAsync(int ListingID, string UserGuid, string Email, string Mobile, string IPAddress, string UserRole, string VisitDate, string VisitTime, string UserAgent, bool Like);

        // Edit
        Task EditListingLikeDislikeAsync(int ListingID, string UserGuid, string VisitDate, string VisitTime, bool Like);

        // Check if listing is liked or not by current user
        Task<bool> ListingLikeByUser(int ListingID, string UserGuid);
        // End:

        // Shafi: Manage Bookmarks
        // Create
        Task CreateBookmarkAsync(int ListingID, string UserGuid, string Email, string Mobile, string IPAddress, string UserRole, string VisitDate, string VisitTime, string UserAgent, bool Bookmark);

        // Edit
        Task EditBookmarkAsync(int ListingID, string UserGuid, string VisitDate, string VisitTime, bool Bookmark);

        // Check if listing is liked or not by current user
        Task<bool> ListingBookmarkByUser(int ListingID, string UserGuid);
        // End:

        // Shafi: Manage Subscribe
        // Create
        Task CreateSubscribeAsync(int ListingID, string UserGuid, string Email, string Mobile, string IPAddress, string UserRole, string VisitDate, string VisitTime, string UserAgent, bool Subscribe);

        // Edit
        Task EditSubscribeAsync(int ListingID, string UserGuid, string VisitDate, string VisitTime, bool Subscribe);

        // Check if listing is liked or not by current user
        Task<bool> ListingSubscribeByUser(int ListingID, string UserGuid);
        // End:
    }
}
