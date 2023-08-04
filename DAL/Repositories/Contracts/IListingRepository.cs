using BOL.BANNERADS;
using BOL.LISTING;
using BOL.LISTING.UploadImage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IListingRepository
    {
        #region Listings
        Task<IEnumerable<Categories>> GetCategoriesByFirstCategoryId(int firstCategoryID);
        Task<IEnumerable<Categories>> GetCategoriesBySecondCategoryId(int secondCategoryID);
        Task<IEnumerable<Categories>> GetCategoriesByThirdCategoryId(int thirdCategoryID);
        Task<IEnumerable<Categories>> GetCategoriesByFourthCategoryId(int fourthCategoryID);
        Task<IEnumerable<Categories>> GetCategoriesByFifthCategoryId(int fifthCategoryID);
        Task<IEnumerable<Categories>> GetCategoriesBySixthCategoryId(int sixthCategoryID);
        Task<IEnumerable<Listing>> GetListingsByListingIds(int[] listingIds);
        Task<IEnumerable<Address>> GetAddressesByListingIds(int[] listingIds);
        Task<IEnumerable<Communication>> GetCommunicationsByListingIds(int[] listingIds);
        #endregion

        Task<Categories> GetCategoryByListingId(int listingId);

        Task<IEnumerable<ListingBanner>> GetListingBannersBySecondCategoryId(int secondCategoryId);

        Task<int> CountRatingAsync(int ListingID, int rating);

        Task<IEnumerable<Listing>> GetListings();

        Task<Listing> GetListingByOwnerId(string ownerId);

        Task<Listing> GetListingByListingId(int listingId);

        Task<Communication> GetCommunicationByListingId(int listingId);

        Task<PaymentMode> GetPaymentModeByListingId(int listingId);

        Task<WorkingHours> GetWorkingHoursByListingId(int listingId);

        Task<Address> GetAddressByListingId(int listingId);

        Task<Specialisation> GetSpecialisationByListingId(int listingId);

        Task<IEnumerable<Rating>> GetRatingsByListingId(int listingId);
        Task<Rating> GetRatingsByListingIdAndOwnerId(int listingId, string ownerId);

        Task<object> AddAsync(object data);

        Task UpdateAsync(object data);

        #region Banner
        Task<IList<HomeBanner>> GetHomeBannerList();
        #endregion

        #region Upload Images
        Task<LogoImage> GetLogoImageByListingId(int listingId);
        #endregion
    }
}
