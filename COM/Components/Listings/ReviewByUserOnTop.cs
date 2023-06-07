using BAL.Listings;
using DAL.LISTING;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace COM.Components.Listings
{
    public class ReviewByUserOnTop : ViewComponent
    {
        private readonly IListingManager listingRepository;
        private readonly ListingDbContext listingContext;
        private readonly UserManager<IdentityUser> userManager;
        public ReviewByUserOnTop(IListingManager listingRepository, ListingDbContext listingContext, UserManager<IdentityUser> userManager)
        {
            this.listingRepository = listingRepository;
            this.listingContext = listingContext;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int ListingID)
        {
            // Shafi: Pass listing id to view component
            ViewBag.ListingID = ListingID;
            // End:

            if (User.Identity.IsAuthenticated == true)
            {
                // Shafi: Get current user guid
                var user = await userManager.FindByNameAsync(User.Identity.Name);
                var OwnerGuid = user.Id;
                ViewBag.OwnerGuid = OwnerGuid;
                // End:

                bool allRatings = listingContext.Rating.Any(r => r.OwnerGuid == OwnerGuid);
                ViewBag.AllRatings = allRatings;

                if (allRatings == true)
                {
                    // Shafi: Get rating posted by user for this listing
                    var review = await listingContext.Rating.Where(r => r.ListingID == ListingID && r.OwnerGuid == OwnerGuid).FirstOrDefaultAsync();
                    // End:

                    return View(review);
                }
                else
                {
                    // Shafi: If user is not loggedin and listing exisits take first or default
                    var review = await listingContext.Rating.Where(r => r.ListingID == ListingID).OrderByDescending(r => r.RatingID).FirstOrDefaultAsync();
                    // End:

                    return View(review);
                }
            }
            else
            {
                // Shafi: If user is not loggedin and listing exisits take first or default
                var review = await listingContext.Rating.Where(r => r.ListingID == ListingID).OrderByDescending(r => r.RatingID).FirstOrDefaultAsync();
                // End:

                return View(review);
            }
        }
    }
}