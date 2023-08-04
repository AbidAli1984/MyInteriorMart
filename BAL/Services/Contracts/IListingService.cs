using BOL.BANNERADS;
using BOL.ComponentModels.Listings;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL.ComponentModels.Pages;
using BOL.LISTING;
using BOL.LISTING.UploadImage;
using BOL.VIEWMODELS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IListingService
    {
        Task<IList<ListingResultVM>> GetListings(string url, string level);

        Task<ListingDetailVM> GetListingDetailByListingId(int listingId, string currentUserId);

        Task<IEnumerable<ListingBanner>> GetSecCatListingByListingId(int listingId);

        Task<IEnumerable<Rating>> GetRatingAsync(int ListingID);

        Task<int> CountRatingAsync(int ListingID, int rating);

        Task<Listing> GetListingByOwnerId(string ownerId);

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

        #region Upload Images
        Task<LogoImage> GetLogoImageByListingId(int listingId);
        Task<bool> AddOrUpdateLogoImage(UploadImagesVM uploadImagesVM);
        Task<IList<ImageDetails>> GetOwnerImagesByListingId(int listingId);
        Task<bool> AddOwnerImage(UploadImagesVM uploadImagesVM);
        Task<bool> DeleteOwnerImage(int id);
        Task<IList<ImageDetails>> GetGalleryImagesByListingId(int listingId);
        Task<bool> AddGalleryImage(UploadImagesVM uploadImagesVM);
        Task<bool> DeleteGalleryImage(int id);
        #endregion
    }
}
