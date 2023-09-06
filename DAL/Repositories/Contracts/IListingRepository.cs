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
        Task<IEnumerable<Listing>> GetApprovedListingsByListingIds(int[] listingIds);
        Task<IEnumerable<Address>> GetAddressesByListingIds(int[] listingIds);
        Task<IEnumerable<Communication>> GetCommunicationsByListingIds(int[] listingIds);
        #endregion

        Task<Categories> GetCategoryByListingId(int listingId);

        Task<int> CountRatingAsync(int ListingID, int rating);

        Task<IEnumerable<Listing>> GetApprovedListings();

        Task<Listing> GetListingByOwnerId(string ownerId);

        Task<Listing> GetApprovedListingByListingId(int listingId);

        Task<Communication> GetCommunicationByListingId(int listingId);

        Task<PaymentMode> GetPaymentModeByListingId(int listingId);

        Task<WorkingHours> GetWorkingHoursByListingId(int listingId);

        Task<Address> GetAddressByListingId(int listingId);

        Task<Specialisation> GetSpecialisationByListingId(int listingId);

        Task<IEnumerable<Rating>> GetRatingsByListingId(int listingId);

        Task<RatingReply> GetRatingReplyById(int id);

        Task<IList<ListingEnquiry>> GetEnquiryByListingId(int listingId);

        Task<Rating> GetRatingByListingIdAndOwnerId(int listingId, string ownerId);

        Task<object> AddAsync(object data);

        Task UpdateAsync(object data);

        #region Social Network
        Task<SocialNetwork> GetSocialNetworkByListingId(int listingId);
        #endregion

        #region Keyword
        Task<IList<Keyword>> GetKeywords(string searchText = null);
        Task<List<Keyword>> GetKeywordsByListingId(int listingId);
        Task<IList<Keyword>> AddKeywordsAsync(IList<Keyword> keywords);
        Task DeleteKeywordsByListingId(IList<Keyword> keywords);
        #endregion

        #region Banner
        Task<IList<HomeBanner>> GetHomeBannerList();
        Task<IList<CategoryBanner>> GetCategoryBannersByThirtCategoryId(int thirdCategoryId);
        #endregion

        #region Upload Images
        Task<LogoImage> GetLogoImageByListingId(int listingId);

        Task<IList<OwnerImage>> GetOwnerImagesByListingId(int listingId);
        Task DeleteOwnerImage(int id);

        Task<IList<GalleryImage>> GetGalleryImagesByListingId(int listingId);
        Task DeleteGalleryImage(int id);

        Task<BannerDetail> GetBannerDetailByListingId(int listingId);

        Task<IList<CertificationDetail>> GetCertificationDetailsByListingId(int listingId);
        Task DeleteCertificationDetail(int id);

        Task<IList<ClientDetail>> GetClientDetailsByListingId(int listingId);
        Task DeleteClientDetail(int id);
        #endregion
    }
}
