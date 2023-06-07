using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BOL.VIEWMODELS.Dashboards;
using BOL.AUDITTRAIL;
using BOL.LISTING;

namespace BAL.Dashboard.Listing
{
    public interface IDashboardListing
    {
        Task<int> CountAllAsync();
        Task<int> CountAllAsync(int month, int year);
        Task<int> CountFreeAsync();
        Task<int> CountPaidAsync();
        Task<int> CountOrphanAsync();
        Task<int> CountOrphanAsync(int month, int year);
        Task<int> CountOwnedAsync();
        Task<int> CountOwnedAsync(int month, int year);
        Task<int> CountClaimedAsync();
        Task<int> CountVerifiedAsync();
        Task<int> CountUnverifiedAsync();
        Task<int> CountZeroReviewsAsync();
        Task<int> CountWithoutProfileImageAsync();
        Task<int> CountClaimListingRequestsAsync();
        Task<int> CountListingVerificationRequestAsync();
        Task<DashboardListingViewModel> GraphAsync();
        Task<IList<DashboardListingByFirstCatViewModel>> GetListingCountByFirstCatAsync();

        Task<IList<DahboardListingHighestCount>> GetHighestLikesAsync();
        Task<IList<DahboardListingHighestCount>> GetHighestBookmarksAsync();
        Task<IList<DahboardListingHighestCount>> GetHighestSubscribesAsync();
        Task<IList<DahboardListingHighestCount>> GetHighestRatingAsync();
        Task<IList<DahboardListingHighestCount>> GetHighestListingViewsAsync();

        Task<IList<BOL.LISTING.Listing>> GetRecentCretedListings(int GetRecords);
        Task<IList<DashboardListingsWithoutWebsite>> GetListingsWithoutWebsite(int GetRecords);
    }
}
