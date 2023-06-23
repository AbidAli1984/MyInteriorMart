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

        public async Task<IEnumerable<Rating>> GetRatingAsync(int ListingID)
        {
            var ratings = await _listingDbContext.Rating.Where(r => r.ListingID == ListingID).ToListAsync();
            return ratings;
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
    }
}
