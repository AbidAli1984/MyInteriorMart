using BOL.BANNERADS;
using BOL.ComponentModels.Listings;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.ComponentModels.Pages;
using BOL.LISTING;
using BOL.LISTING.UploadImage;
using BOL.SHARED;
using BOL.VIEWMODELS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IListingService
    {
        Task<IList<ListingResultVM>> GetListings(string url, string level, PageVM pageVM);

        Task<ListingDetailVM> GetListingDetailByListingId(string listingId, string currentUserId);

        Task<IList<ReviewListingViewModel>> GetReviewsAsync(int listingId);
        Task<IList<ReviewListingViewModel>> GetReviewsByOwnerIdAsync(string ownerId);

        Task<RatingReply> GetRatingReplyById(int id);

        Task<IList<ListingEnquiry>> GetEnquiryByOwnerIdAsync(string ownerId);

        Task<IEnumerable<Rating>> GetRatingAsync(int ListingID);

        Task<int> CountRatingAsync(int ListingID, int rating);

        Task<Listing> GetListingByOwnerId(string ownerId);

        Task UpdateListingStepByOwnerId(string ownerId, int currentPageStep, int currentDBStep);

        Task<Categories> GetCategoryByListingId(int listingId);

        Task<Communication> GetCommunicationByListingId(int listingId);

        Task<PaymentMode> GetPaymentModeByListingId(int listingId);

        Task<WorkingHours> GetWorkingHoursByListingId(int listingId);

        Task<Address> GetAddressByListingId(int listingId);

        Task<Specialisation> GetSpecialisationByListingId(int listingId);

        Task<Rating> GetRatingByListingIdAndOwnerId(int listingId, string ownerId);

        Task AddAsync(object data);

        Task UpdateAsync(object data);

        Task<IList<SearchResultViewModel>> GetSearchListings();



        #region Social Network
        Task<SocialNetwork> GetSocialNetworkByListingId(int listingId);
        #endregion

        #region Keyword
        Task<IList<SearchResultViewModel>> GetKeywords(string searchText = null);
        Task<List<BOL.LISTING.Keyword>> GetKeywordsByListingId(int listingId);
        Task<IList<BOL.LISTING.Keyword>> AddKeywordsAsync(IList<BOL.LISTING.Keyword> keywords);
        Task DeleteKeywordsByListingId(IList<BOL.LISTING.Keyword> keywords);
        #endregion

        #region Banner
        Task<IndexVM> GetHomeBannerList();
        Task<ListingResultBannerVM> GetListingResultBannersByUrl(string url);
        #endregion

        #region Upload Images
        Task<LogoImage> GetLogoImageByListingId(int listingId);
        Task<bool> AddOrUpdateLogoImage(UploadImagesVM uploadImagesVM);

        Task<IList<ImageDetails>> GetOwnerImagesByListingId(int listingId);
        Task<bool> AddOwnerImage(UploadImagesVM uploadImagesVM);
        Task<bool> DeleteOwnerImage(int id);

        Task<IList<ImageDetails>> GetGalleryImagesByListingId(int listingId);
        Task<bool> AddGalleryImage(UploadImagesVM uploadImagesVM);
        Task<bool> DeleteGalleryImage(int id);

        Task<BannerDetail> GetBannerDetailByListingId(int listingId);
        Task<BannerDetail> AddOrUpdateBannerImage(UploadImagesVM uploadImagesVM);

        Task<IList<ImageDetails>> GetCertificateDetailsByListingId(int listingId);
        Task<bool> AddCertificateDetail(UploadImagesVM uploadImagesVM);
        Task<bool> DeleteCertificateDetail(int id);

        Task<IList<ImageDetails>> GetClientDetailsByListingId(int listingId);
        Task<bool> AddClientDetail(UploadImagesVM uploadImagesVM);
        Task<bool> DeleteClientDetail(int id);
        #endregion
    }
}
