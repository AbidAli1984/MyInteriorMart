using BOL.BANNERADS;
using BOL.LISTING;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IListingRepository
    {
        Task<Categories> GetCategoryByListingId(int listingId);

        Task<IEnumerable<ListingBanner>> GetListingBannerBySecondCategoryId(int secondCategoryId);

        Task<IEnumerable<Rating>> GetRatingAsync(int ListingID);

        Task<int> CountRatingAsync(int ListingID, int rating);

        public Task<IEnumerable<Listing>> GetUsersListingAsync(string currentUserGuid);
    }
}
