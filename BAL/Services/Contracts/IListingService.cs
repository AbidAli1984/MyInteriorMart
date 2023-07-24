using BOL.BANNERADS;
using BOL.ComponentModels.Pages;
using BOL.LISTING;
using BOL.VIEWMODELS;
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

        public Task<IEnumerable<Listing>> GetListingsByOwnerId(string currentUserGuid);

        Task<Categories> GetCategoryByListingId(int listingId);

        Task<Listing> GetListingByListingId(int listingId);

        Task<Communication> GetCommunicationByListingId(int listingId);

        Task<PaymentMode> GetPaymentModeByListingId(int listingId);

        Task<WorkingHours> GetWorkingHoursByListingId(int listingId);

        Task<Address> GetAddressByListingId(int listingId);

        Task<Specialisation> GetSpecialisationByListingId(int listingId);

        Task<IEnumerable<Rating>> GetRatingsByListingId(int listingId);

        Task<Rating> GetRatingsByListingIdAndOwnerId(int listingId, string ownerId);

        Task AddAsync(object data);

        Task UpdateAsync(object data);

        Task<IList<SearchResultViewModel>> GetSearchListings();

        #region Banner
        Task<IndexVM> GetHomeBannerList();
        #endregion
    }
}
