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
using BOL.ComponentModels.Listings;

namespace BAL.Services
{
    public class ListingService : IListingService
    {
        private readonly IListingRepository _listingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISharedRepository _sharedRepository;
        private readonly HelperFunctions _helperFunctions;
        public ListingService(IListingRepository listingRepository, ICategoryRepository categoryRepository,
            ISharedRepository sharedRepository, HelperFunctions helperFunctions)
        {
            _listingRepository = listingRepository;
            _categoryRepository = categoryRepository;
            _sharedRepository = sharedRepository;
            _helperFunctions = helperFunctions;
        }

        public async Task<IList<ListingResultVM>> GetListings(string url, string level)
        {
            IEnumerable<Categories> listCat = null;
            IList<ListingResultVM> listLrvm = new List<ListingResultVM>();

            if (level == Constants.LevelFirstCategory)
            {
                var id = await _categoryRepository.GetFirstCategoryByURL(url);
                listCat = await _listingRepository.GetCategoriesByFirstCategoryId(id.FirstCategoryID);
            }
            else if (level == Constants.LevelSecondCategory)
            {
                var id = await _categoryRepository.GetSecondCategoryByURL(url); ;
                listCat = await _listingRepository.GetCategoriesBySecondCategoryId(id.SecondCategoryID);
            }
            else if (level == Constants.LevelThirdCategory)
            {
                var id = await _categoryRepository.GetThirdCategoryByURL(url); ;
                listCat = await _listingRepository.GetCategoriesByThirdCategoryId(id.ThirdCategoryID);
            }
            else if (level == Constants.LevelFourthCategory)
            {
                var id = await _categoryRepository.GetFourthCategoryByURL(url); ;
                listCat = await _listingRepository.GetCategoriesByFourthCategoryId(id.FourthCategoryID);
            }
            else if (level == Constants.LevelFifthCategory)
            {
                var id = await _categoryRepository.GetFifthCategoryByURL(url);
                listCat = await _listingRepository.GetCategoriesByFifthCategoryId(id.FifthCategoryID);
            }
            else if (level == Constants.LevelSixthCategory)
            {
                var id = await _categoryRepository.GetSixthCategoryByURL(url); ;
                listCat = await _listingRepository.GetCategoriesBySixthCategoryId(id.SixthCategoryID);
            }

            if (listCat.Count() > 0)
            {
                int[] listingIds = listCat.Select(x => x.ListingID).ToArray();
                var approvedlistings = await _listingRepository.GetListingsByListingIds(listingIds);
                int[] approvedListingIds = approvedlistings.Select(x => x.ListingID).ToArray();
                var addresses = await _listingRepository.GetAddressesByListingIds(approvedListingIds);
                var communications = await _listingRepository.GetCommunicationsByListingIds(approvedListingIds);

                // Begin: Add result to listLrvm
                foreach (var item in approvedlistings)
                {
                    int listingId = item.ListingID;

                    var address = addresses.Where(x => x.ListingID == listingId).FirstOrDefault();
                    var assembly = await _sharedRepository.GetAreaByAreaId(address.AssemblyID);
                    var area = await _sharedRepository.GetLocalityByLocalityId(address.LocalityID);

                    var communication = communications.Where(x => x.ListingID == listingId).FirstOrDefault();

                    var secondCatId = listCat.First().SecondCategoryID;
                    var secondCat = await _categoryRepository.GetSecondCategoryById(secondCatId);

                    var businessWorking = await _helperFunctions.IsBusinessOpen(listingId);

                    var rating = await GetRatingAsync(listingId);
                    var ratingCount = rating.Count();
                    var ratingAverage = await RatingAverageAsync(listingId);

                    ListingResultVM lrvm = new ListingResultVM
                    {
                        ListingId = listingId,
                        CompanyName = item.CompanyName,
                        Url = item.ListingURL,
                        //SubCategoryId = item.Categories.SecondCategoryID,
                        SubCategory = secondCat != null ? secondCat.Name : string.Empty,
                        Assembly = assembly != null ? assembly.Name : string.Empty,
                        Area = area != null ? area.LocalityName : string.Empty,
                        Mobile = communication.Mobile,
                        Email = communication.Email,
                        BusinessYear = DateTime.Now.Year - item.YearOfEstablishment.Year,
                        BusinessWorking = businessWorking,
                        RatingAverage = ratingAverage,
                        RatingCount = ratingCount
                    };

                    listLrvm.Add(lrvm);
                    // End: Add result to listLrvm
                }
            }
            return listLrvm;
        }

        public async Task<decimal> RatingAverageAsync(int ListingID)
        {
            // Shafi: Get rating average
            var ratings = await _listingRepository.GetRatingsByListingId(ListingID);

            if (ratings.Count() > 0)
            {
                var R1 = await CountRatingAsync(ListingID, 1);
                var R2 = await CountRatingAsync(ListingID, 2);
                var R3 = await CountRatingAsync(ListingID, 3);
                var R4 = await CountRatingAsync(ListingID, 4);
                var R5 = await CountRatingAsync(ListingID, 5);

                decimal averageCount = 5 * R5 + 4 * R4 + 3 * R3 + 2 * R2 + 1 * R1;
                decimal weightedCount = R5 + R4 + R3 + R2 + R1;
                decimal ratingAverage = averageCount / weightedCount;

                return ratingAverage;
            }
            else
            {
                return 0;
            }
            // End:
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
