using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.LISTING;
using DAL.LISTING;
using Microsoft.AspNetCore.Authorization;
using DAL.SHARED;
using Microsoft.AspNetCore.Identity;
using BAL.Audit;
using BAL.Listings;
using BOL.AUDITTRAIL;
using DAL.AUDIT;
using BOL.VIEWMODELS;
using System.Linq;

namespace FRONTEND.Areas.Analytics.Controllers
{
    [Area("Analytics")]
    [Authorize]

    public class SubscribesController : Controller
    {
        private readonly AuditDbContext auditContext;
        private readonly ListingDbContext listingContext;
        private readonly SharedDbContext sharedManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IHistoryAudit audit;
        private readonly IListingManager listingManager;

        public SubscribesController(AuditDbContext auditContext, ListingDbContext listingContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SharedDbContext sharedManager, IHistoryAudit audit, IListingManager listingManager)
        {
            this.auditContext = auditContext;
            this.listingContext = listingContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.sharedManager = sharedManager;
            this.audit = audit;
            this.listingManager = listingManager;
        }

        // GET: Analytics/Likes
        public async Task<IActionResult> Index()
        {
            // Shafi: Get user guid
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            string UserGuid = user.Id;
            // End:

            // Shafi: Begin partial view model BookmarkListingViewModels
            var subscribeList = await auditContext.Subscribes.Where(b => b.UserGuid == UserGuid && b.Subscribe == true).ToListAsync();
            var subscribeListingIDs = subscribeList.Select(b => b.ListingID).ToList();
            var listingList = await listingContext.Listing.Where(l => subscribeListingIDs.Contains(l.ListingID)).ToListAsync();
            // End:

            // Shafi: Join Bookarks and Listing
            var likeModal = (from subscribe in subscribeList
                             join listing in listingList
                     on subscribe.ListingID equals listing.ListingID
                             select new SubscribeListingViewModel
                             {
                                 SubscribeID = subscribe.SubscribeID,
                                 ListingID = listing.ListingID,
                                 CompanyName = listing.CompanyName,
                                 VisitDate = subscribe.VisitDate.ToString(),
                                 VisitTime = subscribe.VisitTime.ToString(),
                             }).ToList();
            // End:

            return View(likeModal);
        }
    }
}
