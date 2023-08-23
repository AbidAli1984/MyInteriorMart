﻿using BOL.BANNERADS;
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

        Task<ListingDetailVM> GetListingDetailByListingId(int listingId, string currentUserId);

        Task<IList<ReviewListingViewModel>> GetReviewsAsync(int listingId);

        Task<IEnumerable<Rating>> GetRatingAsync(int ListingID);

        Task<int> CountRatingAsync(int ListingID, int rating);

        Task<Listing> GetListingByOwnerId(string ownerId);

        Task UpdateListingStepByOwnerId(string ownerId, int currentPageStep);

        Task<Categories> GetCategoryByListingId(int listingId);

        Task<Communication> GetCommunicationByListingId(int listingId);

        Task<PaymentMode> GetPaymentModeByListingId(int listingId);

        Task<WorkingHours> GetWorkingHoursByListingId(int listingId);

        Task<Address> GetAddressByListingId(int listingId);

        Task<Specialisation> GetSpecialisationByListingId(int listingId);

        Task<IEnumerable<Rating>> GetRatingsByListingId(int listingId);

        Task<Rating> GetRatingByListingIdAndOwnerId(int listingId, string ownerId);

        Task AddAsync(object data);

        Task UpdateAsync(object data);

        Task<IList<SearchResultViewModel>> GetSearchListings();

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
