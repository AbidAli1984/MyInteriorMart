using DAL.LISTING;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace COM.Components.Listings
{
    public class RatingEdit : ViewComponent
    {
        private readonly ListingDbContext listingContext;

        public RatingEdit(ListingDbContext listingContext) 
        {
            this.listingContext = listingContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(int RatingID)
        {
            var rating = await listingContext.Rating.Where(r => r.RatingID == RatingID).FirstOrDefaultAsync(); 

            return View(rating);
        }
    }
}