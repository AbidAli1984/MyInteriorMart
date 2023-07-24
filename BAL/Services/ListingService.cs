using BAL.Services.Contracts;
using BOL.BANNERADS;
using DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;
using BOL.LISTING;
using BOL.ComponentModels.Pages;
using System.Linq;
using BOL.VIEWMODELS;
using System;

namespace BAL.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        public ListingService(IListingRepository listingRepository)
        {
            _listingRepository = listingRepository;
        }

        public async Task<int> CountRatingAsync(int ListingID, int rating)
        {
            return await _listingRepository.CountRatingAsync(ListingID, rating);
        }

        public async Task<IEnumerable<Rating>> GetRatingAsync(int ListingID)
        {
            return await _listingRepository.GetRatingsByListingId(ListingID);
        }

        public async Task<IEnumerable<ListingBanner>> GetSecCatListingByListingId(int listingId)
        {
            var listingCat = await _listingRepository.GetCategoryByListingId(listingId);

            return await _listingRepository.GetListingBannerBySecondCategoryId(listingCat.SecondCategoryID);
        }

        public async Task<IEnumerable<BOL.LISTING.Listing>> GetListingsByOwnerId(string ownerId)
        {
            return await _listingRepository.GetListingsByOwnerId(ownerId);
        }

        public async Task<Listing> GetListingByListingId(int listingId)
        {
            return await _listingRepository.GetListingByListingId(listingId);
        }

        public async Task<Categories> GetCategoryByListingId(int listingId)
        {
            return await _listingRepository.GetCategoryByListingId(listingId);
        }


        public async Task<Communication> GetCommunicationByListingId(int listingId)
        {
            return await _listingRepository.GetCommunicationByListingId(listingId);
        }

        public async Task<PaymentMode> GetPaymentModeByListingId(int listingId)
        {
            return await _listingRepository.GetPaymentModeByListingId(listingId);
        }

        public async Task<WorkingHours> GetWorkingHoursByListingId(int listingId)
        {
            return await _listingRepository.GetWorkingHoursByListingId(listingId);
        }

        public async Task<Address> GetAddressByListingId(int listingId)
        {
            return await _listingRepository.GetAddressByListingId(listingId);
        }

        public async Task<Specialisation> GetSpecialisationByListingId(int listingId)
        {
            return await _listingRepository.GetSpecialisationByListingId(listingId);
        }

        public async Task<IEnumerable<Rating>> GetRatingsByListingId(int listingId)
        {
            return await _listingRepository.GetRatingsByListingId(listingId);
        }

        public async Task<Rating> GetRatingsByListingIdAndOwnerId(int listingId, string ownerId)
        {
            return await _listingRepository.GetRatingsByListingIdAndOwnerId(listingId, ownerId);
        }

        public async Task AddAsync(object data)
        {
            await _listingRepository.AddAsync(data);
        }

        public async Task UpdateAsync(object data)
        {
            await _listingRepository.UpdateAsync(data);
        }

        public async Task<IList<SearchResultViewModel>> GetSearchListings()
        {
            var listings = await _listingRepository.GetListings();
            return listings.Select((x) => new SearchResultViewModel
            {
                label = x.CompanyName,
                value = Convert.ToString(x.ListingID)
            }).ToList();
        }

        #region Banner
        public async Task<IndexVM> GetHomeBannerList()
        {
            var HomeBannerList = await _listingRepository.GetHomeBannerList();
            IndexVM indexVM = new IndexVM()
            {
                HomeBannerTop = HomeBannerList.Where(i => i.Placement == "HomeTop").ToList(),
                HomeBannerMiddle1 = HomeBannerList.Where(i => i.Placement == "HomeMiddle1").ToList(),
                HomeBannerMiddle2 = HomeBannerList.Where(i => i.Placement == "HomeMiddle2").ToList(),
                HomeBannerBottom = HomeBannerList.Where(i => i.Placement == "HomeBottom").ToList()
            };
            return indexVM;
        }
        #endregion
    }
}
