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
using BOL.LISTING.UploadImage;

namespace DAL.Repositories
{
    public class ListingRepository : IListingRepository
    {
        ListingDbContext _listingDbContext;
        public ListingRepository(ListingDbContext listingDbContext)
        {
            this._listingDbContext = listingDbContext;
        }

        #region Listings
        public async Task<IEnumerable<Categories>> GetCategoriesByFirstCategoryId(int firstCategoryID)
        {
            return await _listingDbContext.Categories
                    .Where(i => i.FirstCategoryID == firstCategoryID)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Categories>> GetCategoriesBySecondCategoryId(int secondCategoryID)
        {
            return await _listingDbContext.Categories
                    .Where(i => i.SecondCategoryID == secondCategoryID)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Categories>> GetCategoriesByThirdCategoryId(int thirdCategoryID)
        {
            var listCat = await _listingDbContext.Categories
                    .Where(i => !string.IsNullOrWhiteSpace(i.ThirdCategories))
                    .ToListAsync();

            if (listCat.Any())
                return listCat
                    .Where(x => x.ThirdCategories.Split(',').Any(x => x == thirdCategoryID.ToString()))
                    .ToList();

            return null;
        }

        public async Task<IEnumerable<Categories>> GetCategoriesByFourthCategoryId(int fourthCategoryID)
        {
            var listCat = await _listingDbContext.Categories
                    .Where(i => !string.IsNullOrWhiteSpace(i.FourthCategories))
                    .ToListAsync();

            if (listCat.Any())
                return listCat
                .Where(x => x.FourthCategories.Split(',').Any(x => x == fourthCategoryID.ToString()))
                .ToList();

            return null;
        }

        public async Task<IEnumerable<Categories>> GetCategoriesByFifthCategoryId(int fifthCategoryID)
        {
            var listCat = await _listingDbContext.Categories
                    .Where(i => !string.IsNullOrWhiteSpace(i.FifthCategories))
                    .ToListAsync();

            if (listCat.Any())
                return listCat
                    .Where(x => x.FifthCategories.Split(',').Any(x => x == fifthCategoryID.ToString()))
                    .ToList();

            return null;
        }

        public async Task<IEnumerable<Categories>> GetCategoriesBySixthCategoryId(int sixthCategoryID)
        {
            var listCat = await _listingDbContext.Categories
                    .Where(i => !string.IsNullOrWhiteSpace(i.SixthCategories))
                    .ToListAsync();

            if (listCat.Any())
                return listCat
                    .Where(x => x.SixthCategories.Split(',').Any(x => x == sixthCategoryID.ToString()))
                    .ToList();

            return null;
        }

        public async Task<IEnumerable<Listing>> GetApprovedListingsByListingIds(int[] listingIds)
        {
            return await _listingDbContext.Listing
                .Where(x => x.Approved && listingIds.Contains(x.ListingID))
                .OrderByDescending(i => i.ListingID)
                .ToListAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesByListingIds(int[] listingIds)
        {
            return await _listingDbContext.Address
                .Where(x => listingIds.Contains(x.ListingID))
                .OrderByDescending(i => i.ListingID)
                .ToListAsync();
        }

        public async Task<IEnumerable<Communication>> GetCommunicationsByListingIds(int[] listingIds)
        {
            return await _listingDbContext.Communication
                .Where(x => listingIds.Contains(x.ListingID))
                .OrderByDescending(i => i.ListingID)
                .ToListAsync();
        }
        #endregion

        public async Task<Categories> GetCategoryByListingId(int listingId)
        {
            return await _listingDbContext.Categories
                .Where(i => i.ListingID == listingId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ListingBanner>> GetListingBannersBySecondCategoryId(int secondCategoryId)
        {
            return await _listingDbContext.ListingBanner
                .Where(i => i.SecondCategoryID == secondCategoryId)
                .OrderBy(i => i.Priority)
                .ToListAsync();
        }

        public async Task<int> CountRatingAsync(int ListingID, int rating)
        {
            var count = await _listingDbContext.Rating.Where(r => r.ListingID == ListingID && r.Ratings == rating).CountAsync();
            return count;
        }

        public async Task<IEnumerable<Listing>> GetApprovedListings()
        {
            return await _listingDbContext.Listing.Where(x => x.Approved)
                .OrderByDescending(i => i.ListingID)
                .ToListAsync();
        }

        public async Task<Listing> GetListingByOwnerId(string ownerId)
        {
            return await _listingDbContext.Listing
                .Where(i => i.OwnerGuid == ownerId)
                .FirstOrDefaultAsync();
        }

        public async Task<Listing> GetApprovedListingByListingId(int listingId)
        {
            return await _listingDbContext.Listing
                .Where(l => l.ListingID == listingId && l.Approved)
                .FirstOrDefaultAsync();
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

        public async Task<Rating> GetRatingByListingIdAndOwnerId(int listingId, string ownerId)
        {
            return await _listingDbContext.Rating.Where(l => l.ListingID == listingId && l.OwnerGuid == ownerId).FirstOrDefaultAsync();
        }

        public async Task<object> AddAsync(object data)
        {
            await _listingDbContext.AddAsync(data);
            await _listingDbContext.SaveChangesAsync();
            return data;
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

        public async Task<IList<CategoryBanner>> GetCategoryBannersByThirtCategoryId(int thirdCategoryId)
        {
            return await _listingDbContext.CategoryBanner
                .Where(i => i.ThirdCategoryID == thirdCategoryId)
                .OrderBy(i => i.Priority)
                .ToListAsync();
        }
        #endregion

        #region Upload Images
        public async Task<LogoImage> GetLogoImageByListingId(int listingId)
        {
            return await _listingDbContext.LogoImages
                .FirstOrDefaultAsync(l => l.ListingID == listingId);
        }
        public async Task<IList<OwnerImage>> GetOwnerImagesByListingId(int listingId)
        {
            return await _listingDbContext.OwnerImages
                .Where(x => x.ListingID == listingId)
                .ToListAsync();
        }
        public async Task DeleteOwnerImage(int id)
        {
            var ownerImage = await _listingDbContext.OwnerImages.FindAsync(id);
            _listingDbContext.OwnerImages.Remove(ownerImage);
            await _listingDbContext.SaveChangesAsync();
        }
        public async Task<IList<GalleryImage>> GetGalleryImagesByListingId(int listingId)
        {
            return await _listingDbContext.GalleryImages
                .Where(x => x.ListingID == listingId)
                .ToListAsync();
        }
        public async Task DeleteGalleryImage(int id)
        {
            var galleryImage = await _listingDbContext.GalleryImages.FindAsync(id);
            _listingDbContext.GalleryImages.Remove(galleryImage);
            await _listingDbContext.SaveChangesAsync();
        }
        #endregion
    }
}
