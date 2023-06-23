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

        Task<int> CountRatingAsync(int ListingID, int rating);

        Task<IEnumerable<Listing>> GetUsersListingAsync(string currentUserGuid);

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
