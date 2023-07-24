using BOL.LISTING;
using DAL.LISTING;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BOL.BANNERADS;

namespace DAL.Repositories
{
    public class ListingRepository : IListingRepository
    {
        ListingDbContext _listingDbContext;
        public ListingRepository(ListingDbContext listingDbContext)
        {
            this._listingDbContext = listingDbContext;
        }
        public async Task<Categories> GetCategoryByListingId(int listingId)
        {
            return await _listingDbContext.Categories
                .Where(i => i.ListingID == listingId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ListingBanner>> GetListingBannerBySecondCategoryId(int secondCategoryId)
        {
            return await _listingDbContext.ListingBanner
                .Where(i => i.SecondCategoryID == secondCategoryId)
                .OrderBy(i => i.Priority)
                .ToListAsync(); ;
        }

        public async Task<int> CountRatingAsync(int ListingID, int rating)
        {
            var count = await _listingDbContext.Rating.Where(r => r.ListingID == ListingID && r.Ratings == rating).CountAsync();
            return count;
        }

        public async Task<int> CountViewsAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await _listingDbContext.ListingViews.Where(x => x.Date.Day == date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }

        public async Task<int> CountReviewAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await _listingDbContext.Rating.Where(x => x.Date.Day == date.Day && x.Date.Month == x.Date.Month && x.Date.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }

        public async Task<IEnumerable<Listing>> GetListings()
        {
            return await _listingDbContext.Listing.OrderByDescending(i => i.ListingID).ToListAsync();
        }

        public async Task<IEnumerable<Listing>> GetListingsByOwnerId(string ownerId)
        {
            return await _listingDbContext.Listing.Where(i => i.OwnerGuid == ownerId).OrderByDescending(i => i.ListingID).ToListAsync();
        }

        public async Task<Listing> GetListingByListingId(int listingId)
        {
            return await _listingDbContext.Listing.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
        }

        public async Task<Communication> GetCommunicationByListingId(int listingId)
        {
            return await _listingDbContext.Communication.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
        }

        public async Task<PaymentMode> GetPaymentModeByListingId(int listingId)
        {
            return await _listingDbContext.PaymentMode.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
        }

        public async Task<WorkingHours> GetWorkingHoursByListingId(int listingId)
        {
            return await _listingDbContext.WorkingHours.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
        }

        public async Task<Address> GetAddressByListingId(int listingId)
        {
            return await _listingDbContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
        }

        public async Task<Specialisation> GetSpecialisationByListingId(int listingId)
        {
            return await _listingDbContext.Specialisation.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Rating>> GetRatingsByListingId(int listingId)
        {
            return await _listingDbContext.Rating.Where(l => l.ListingID == listingId).ToListAsync();
        }

        public async Task<Rating> GetRatingsByListingIdAndOwnerId(int listingId, string ownerId)
        {
            return await _listingDbContext.Rating.Where(l => l.ListingID == listingId && l.OwnerGuid == ownerId).FirstOrDefaultAsync();
        }

        public async Task AddAsync(object data)
        {
            await _listingDbContext.AddAsync(data);
            await _listingDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(object data)
        {
            _listingDbContext.Update(data);
            await _listingDbContext.SaveChangesAsync();
        }

        #region Banner
        public async Task<IList<HomeBanner>> GetHomeBannerList()
        {
            return await _listingDbContext.HomeBanner
                .OrderBy(i => i.Priority)
                .ToListAsync();
        }
        #endregion
    }
}
