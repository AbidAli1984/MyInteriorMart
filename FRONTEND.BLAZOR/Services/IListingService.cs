using BOL.LISTING;
using BOL.PLAN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BOL.AUDITTRAIL;
using BOL.VIEWMODELS.Dashboards;
using BOL.VIEWMODELS;
using Microsoft.EntityFrameworkCore.Storage;

namespace FRONTEND.BLAZOR.Services
{
    public interface IListingService_bak
    {
        // Shafi: Get Plans
        Task<IEnumerable<Plan>> GetPlansAsync();
        // End:
        Task<bool> CheckIfUserLikedListing(int listingId, string userGuid);

        Task<int> CountListingBookmarkAsync(int ListingID);

        // End:


        // Shafi: Used to create line charts in Client Listing Dashboard
        public Task<DashboardListingLast30DaysViews> GetLast30DaysListingViewsAsync(int ListingID);
        public Task<DashboardListingLast30DaysLikes> GetLast30DaysListingLikeAsync(int ListingID);
        public Task<DashboardListingLast30DaysSubscribes> GetLast30DaysListingSubscribesAsync(int ListingID);
        public Task<DashboardListingLast30DaysBookmarks> GetLast30DaysListingBookmarksAsync(int ListingID);
        public Task<DashboardListingLast30DaysReviews> GetLast30DaysListingReviewsAsync(int ListingID);
        // End:
    }
}
