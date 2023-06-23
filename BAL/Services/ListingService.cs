using BAL.Services.Contracts;
using BOL.BANNERADS;
using DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<int> CountRatingAsync(int ListingID, int rating)
        {
            return await _listingRepository.CountRatingAsync(ListingID, rating);
        }

        public async Task<IEnumerable<BOL.LISTING.Rating>> GetRatingAsync(int ListingID)
        {
            return await _listingRepository.GetRatingAsync(ListingID);
        }

        public async Task<IEnumerable<ListingBanner>> GetSecCatListingByListingId(int listingId)
        {
            var listingCat = await _listingRepository.GetCategoryByListingId(listingId);

            return await _listingRepository.GetListingBannerBySecondCategoryId(listingCat.SecondCategoryID);
        }
    }
}
