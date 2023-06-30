using BAL.Listings;
using DAL.LISTING;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace COM.Components.Listings
{
    public class RatingBars : ViewComponent
    {
        private readonly IListingManager listingRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ListingDbContext listingContext;
        public RatingBars(IListingManager listingRepository, UserManager<ApplicationUser> userManager, ListingDbContext listingContext)
        {
            this.listingRepository = listingRepository;
            this.userManager = userManager;
            this.listingContext = listingContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int ListingID)
        {
            ViewBag.ListingID = ListingID;

            if (User.Identity.IsAuthenticated == true)
            {
                // Shafi: Get current user guid
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var OwnerGuid = user.Id;
                ViewBag.OwnerGuid = OwnerGuid;
                // End:
            }

            // Shafi: Get rating average
            var allRating = await listingContext.Rating.Where(r => r.ListingID == ListingID).ToListAsync();
            var ratingCount = allRating.Count();
            ViewBag.RatingCount = ratingCount;

            if (ratingCount > 0)
            {
                var R1 = await listingRepository.CountRatingAsync(ListingID, 1);
                var R2 = await listingRepository.CountRatingAsync(ListingID, 2);
                var R3 = await listingRepository.CountRatingAsync(ListingID, 3);
                var R4 = await listingRepository.CountRatingAsync(ListingID, 4);
                var R5 = await listingRepository.CountRatingAsync(ListingID, 5);

                decimal averageCount = 5 * R5 + 4 * R4 + 3 * R3 + 2 * R2 + 1 * R1;
                decimal weightedCount = R5 + R4 + R3 + R2 + R1;
                decimal ratingAverage = averageCount / weightedCount;
                ViewBag.RatingAverage = ratingAverage.ToString("0.0");

                decimal R1Total = R1 * 15;
                decimal R2Total = R2 * 15;
                decimal R3Total = R3 * 15;
                decimal R4Total = R4 * 15;
                decimal R5Total = R5 * 15;

                decimal SubTotalOfAllR = R1Total + R2Total + R3Total + R4Total + R5Total;

                decimal R1Percent = (R1Total * 100) / SubTotalOfAllR;
                decimal R2Percent = (R2Total * 100) / SubTotalOfAllR;
                decimal R3Percent = (R3Total * 100) / SubTotalOfAllR;
                decimal R4Percent = (R4Total * 100) / SubTotalOfAllR;
                decimal R5Percent = (R5Total * 100) / SubTotalOfAllR;

                ViewBag.R1 = R1Percent;
                ViewBag.R2 = R2Percent;
                ViewBag.R3 = R3Percent;
                ViewBag.R4 = R4Percent;
                ViewBag.R5 = R5Percent;
            }
            else
            {
                ViewBag.RatingAverage = 0;
            }
            // End:

            return View();
        }
    }
}