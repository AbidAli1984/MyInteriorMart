using BAL.Services.Contracts;
using BOL.BANNERADS;
using BOL.ComponentModels.Listings;
using BOL.LISTING;
using BOL.VIEWMODELS;
using FRONTEND.BLAZOR.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.Listings
{
    public partial class ListingResults
    {

        [Inject]
        private IListingService listingService { get; set; }

        [Parameter]
        public string url { get; set; }

        [Parameter]
        public string level { get; set; }

        public IList<ListingResultVM> listLrvm = new List<ListingResultVM>();

        // Begin: Get All Category Banner
        public int Banner1Count { get; set; }
        public int Banner2Count { get; set; }
        public int Banner3Count { get; set; }


        protected async override Task OnInitializedAsync()
        {
            await PopulateListFLVM();
            await GetCategoryBannerListAsync();
        }

        protected override async Task OnAfterRenderAsync(bool render)
        {
            if (render)
            {
                await jsRuntime.InvokeVoidAsync("InitializeCarousel");
            }
        }



        public async Task PopulateListFLVM()
        {
            listLrvm = await listingService.GetListings(url, level);
            await Task.Delay(50);
        }




        public IEnumerable<CategoryBanner> CategoryBannerList { get; set; }
        public async Task GetCategoryBannerListAsync()
        {
            var thirdCat = await categoriesContext.ThirdCategory
                .Where(i => i.URL == url)
                .FirstOrDefaultAsync();

            CategoryBannerList = await listingContext.CategoryBanner
                .Where(i => i.ThirdCategoryID == thirdCat.ThirdCategoryID)
                .OrderBy(i => i.Priority)
                .ToListAsync();

            Banner1Count = CategoryBannerList.Where(i => i.Placement == "banner-1").Count();

            Banner2Count = CategoryBannerList.Where(i => i.Placement == "banner-2").Count();

            Banner3Count = CategoryBannerList.Where(i => i.Placement == "banner-3").Count();
        }
        // End: Get All Category Banner

        // Begin: Method for Rating
        public async Task<bool> ReviewExistsAsync(int ListingID, string OwnerGuid)
        {
            var result = await listingContext.Rating.Where(r => r.ListingID == ListingID && r.OwnerGuid == OwnerGuid).FirstOrDefaultAsync();

            if (result != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<Rating>> GetRatingAsync(int ListingID)
        {
            var ratings = await listingContext.Rating.Where(r => r.ListingID == ListingID).ToListAsync();
            return ratings;
        }

        public async Task<Rating> RatingDetailsAsync(int ListingID)
        {
            var rating = await listingContext.Rating.Where(r => r.ListingID == ListingID).FirstOrDefaultAsync();
            return rating;
        }

        public async Task<string> RatingAverageAsync(int ListingID)
        {
            // Shafi: Get rating average
            var allRating = await listingContext.Rating.Where(r => r.ListingID == ListingID).ToListAsync();
            var ratingCount = allRating.Count();

            if (ratingCount > 0)
            {
                var R1 = await CountRatingAsync(ListingID, 1);
                var R2 = await CountRatingAsync(ListingID, 2);
                var R3 = await CountRatingAsync(ListingID, 3);
                var R4 = await CountRatingAsync(ListingID, 4);
                var R5 = await CountRatingAsync(ListingID, 5);

                decimal averageCount = 5 * R5 + 4 * R4 + 3 * R3 + 2 * R2 + 1 * R1;
                decimal weightedCount = R5 + R4 + R3 + R2 + R1;
                decimal ratingAverage = averageCount / weightedCount;

                return ratingAverage.ToString("0.0");
            }
            else
            {
                return "0";
            }
            // End:
        }

        public async Task<int> CountRatingAsync(int ListingID, int rating)
        {
            var count = await listingContext.Rating.Where(r => r.ListingID == ListingID && r.Ratings == rating).CountAsync();
            return count;
        }

        public async Task<int> CountListingReviewsAsync(int ListingID)
        {
            var count = await listingContext.Rating.Where(r => r.ListingID == ListingID).CountAsync();
            return count;
        }
        // End: Method for Rating
      
        public string GetDayName(int days)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            return date.ToString("d ddd");
        }
        // End: Business Open Now & Close
    }
}
