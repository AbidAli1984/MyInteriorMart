using System;
using System.Collections.Generic;
using System.Linq;
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

namespace FRONTEND.Areas.Analytics.Controllers
{
    [Area("Analytics")]
    [Authorize]
    public class LikesController : Controller
    {
        private readonly AuditDbContext auditContext;
        private readonly ListingDbContext listingContext;
        private readonly SharedDbContext sharedManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IHistoryAudit audit;
        private readonly IListingManager listingManager;

        public LikesController(AuditDbContext auditContext, ListingDbContext listingContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SharedDbContext sharedManager, IHistoryAudit audit, IListingManager listingManager)
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
            var likeList = await auditContext.ListingLikeDislike.Where(b => b.UserGuid == UserGuid && b.Like == true).ToListAsync();
            var likeListingIDs = likeList.Select(b => b.ListingID).ToList();
            var listingList = await listingContext.Listing.Where(l => likeListingIDs.Contains(l.ListingID)).ToListAsync();
            // End:

            // Shafi: Join Bookarks and Listing
            var likeModal = (from like in likeList
                                 join listing in listingList
                         on like.ListingID equals listing.ListingID
                                 select new LikeListingViewModel
                                 {
                                     LikeDislikeID = like.LikeDislikeID,
                                     ListingID = listing.ListingID,
                                     CompanyName = listing.CompanyName,
                                     VisitDate = like.VisitDate.ToString(),
                                     VisitTime = like.VisitTime.ToString(),
                                 }).ToList();
            // End:

            return View(likeModal);
        }
    }
}
