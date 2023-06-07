using BAL.Listings;
using DAL.LISTING;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COM.Components.ListingDashboard
{
    public class ListingViewCountIncrement : ViewComponent
    {
        private readonly ListingDbContext listingContext;
        private readonly IListingManager listingRepo;
        public ListingViewCountIncrement(ListingDbContext listingContext, IListingManager listingRepo)
        {
            this.listingContext = listingContext;
            this.listingRepo = listingRepo;
        }

        public async Task<IViewComponentResult> InvokeAsync(int ListingID)
        {
            await listingRepo.IncrementViewCountByOneAsync(ListingID);
            return View();
        }
    }
}