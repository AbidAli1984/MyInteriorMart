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
    }
}
