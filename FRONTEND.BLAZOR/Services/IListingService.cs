using BOL.LISTING;
using BOL.PLAN;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BOL.AUDITTRAIL;
using BOL.VIEWMODELS.Dashboards;
using BOL.VIEWMODELS;
using Microsoft.EntityFrameworkCore.Storage;

namespace FRONTEND.BLAZOR.Services
{
    public interface IListingService
    {// Shafi: Check if listing fullfill free listing criteria
        // To fullfill [Free Listing Criteria] a user's listing must have fill all 7 free listing forms
        // This service is mainly used in search result of portal
        public bool CheckIfListingFullfillFreeListingCrieteria(int id);
        // End:

        // Shafi: Check if record exists
        bool CompanyExists(int id);
        bool CommunicationExists(int id);
        bool AddressExists(int id);
        bool CategoriesExists(int id);
        bool SocialExists(int id);
        bool CertificationsExists(int id);
        bool ProfileExists(int id);
        bool PaymentExists(int id);
        bool WorkingExists(int id);
        bool SpecialisationExists(int id);
        bool Kilometer10Exists(int id);
        bool BranchesExists(int id);
        bool LogoExists(int id);
        bool ThumbnailExists(int id);
        bool OwnerPhotoExists(int id);
        // End:

        // Shafi: Verify Record Ownership
        Task<bool> CompanyOwnerAsync(int ListingID, string UserGuid);
        Task<bool> CommunicationOwnerAsync(int CommunicationID, int ListingID, string UserGuid);
        Task<bool> AddressOwnerAsync(int AddressID, int ListingID, string UserGuid);
        Task<bool> CategoryOwnerAsync(int CategoryID, int ListingID, string UserGuid);
        Task<bool> SocialOwnerAsync(int SocialNetworkID, int ListingID, string UserGuid);
        Task<bool> CertificationOwnerAsync(int CertificationID, int ListingID, string UserGuid);
        Task<bool> ProfileOwnerAsync(int ProfileID, int ListingID, string UserGuid);
        Task<bool> PaymentOwnerAsync(int PaymentID, int ListingID, string UserGuid);
        Task<bool> WorkingOwnerAsync(int WorkingHoursID, int ListingID, string UserGuid);
        Task<bool> SpecialisationOwnerAsync(int SpecialisationID, int ListingID, string UserGuid);
        Task<bool> BranchesOwnerAsync(int BranchID, int ListingID, string UserGuid);
        // End:

        // Shafi: Get Plans
        Task<IEnumerable<Plan>> GetPlansAsync();
        // End:

        // Shafi: Get Plan Name
        string GetPlanName(string PlanType, int? PlanID);
        // End:

        // Shafi: Rating
        Task CreateRatingAsync(int ListingID, string OwnerGuid, string IPAddress, DateTime Date, DateTime Time, int Ratings, string Comment, string UserEmail);
        Task<bool> ReviewExistsAsync(int ListingID, string OwnerGuid);

        Task<IEnumerable<Rating>> GetRatingAsync(int ListingID);

        Task<int> CountRatingAsync(int ListingID, int rating);

        public Task<Rating> RatingDetailsAsync(int ListingID);

        Task<string> RatingAverageAsync(int ListingID);

        Task<int> CountListingReviewsAsync(int ListingID);

        Task<int> CountListingLikesAsync(int ListingID);

        Task<int> CountListingSubscribeAsync(int ListingID);

        Task<bool> CheckIfUserSubscribedToListing(int listingId, string userGuid);
        Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid);
        Task<bool> CheckIfUserLikedListing(int listingId, string userGuid);

        Task<int> CountListingBookmarkAsync(int ListingID);

        Task<int> CountListingViewsAsync(int ListingID);

        // End:

        // Shafi: Get all listing details
        public Task<Listing> GetListingDetailsAsync(int ListingID);
        public Task<Communication> CommunicationDetailsAsync(int ListingID);
        // End:

        // Shafi: Begin analytics
        public Task IncrementViewCountByOneAsync(int? ListingID);
        public Task RecordListingViewAsync(int? ListingID, string UserType, string OwnerGuid, string IPAddress, DateTime Date, string Country, string City, string Pincode, string State, string IPV4, string Latitude, string Longitude);

        public Task<IEnumerable<ListingViews>> GetListingViewsAsync(int ListingID);
        // End:

        // Shafi: Check if business is open or close
        public Task<string> BusinessOpenOrCloseAsync(int ListingID);
        // End:

        // Shafi: Used to create line charts in Client Listing Dashboard
        public Task<DashboardListingLast30DaysViews> GetLast30DaysListingViewsAsync(int ListingID);
        public Task<DashboardListingLast30DaysLikes> GetLast30DaysListingLikeAsync(int ListingID);
        public Task<DashboardListingLast30DaysSubscribes> GetLast30DaysListingSubscribesAsync(int ListingID);
        public Task<DashboardListingLast30DaysBookmarks> GetLast30DaysListingBookmarksAsync(int ListingID);
        public Task<DashboardListingLast30DaysReviews> GetLast30DaysListingReviewsAsync(int ListingID);
        // End:
        public Task<IEnumerable<DashboardListingViewCountByCountryViewModel>> GetListingViewsCountByCountry(int ListingID);

        public Task<DashboardListingViewByMonth> CountListingViewsByMonth(int subtractMonth, int ListingID);

        public Task<bool> CheckIfUserHas5Listings(string ownerGuid);

        // Shafi: Others
        public string ListingCompanyName(int listingId);
    }
}
