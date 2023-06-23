using BOL.BANNERADS;
using BOL.LISTING;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IListingService
    {
        Task<IEnumerable<ListingBanner>> GetSecCatListingByListingId(int listingId);

        Task<IEnumerable<Rating>> GetRatingAsync(int ListingID);

        Task<int> CountRatingAsync(int ListingID, int rating);

        public Task<IEnumerable<Listing>> GetUsersListingAsync(string currentUserGuid);
    }
}
