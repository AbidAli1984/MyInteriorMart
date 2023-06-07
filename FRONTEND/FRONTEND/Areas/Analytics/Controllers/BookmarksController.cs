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
    public class BookmarksController : Controller
    {
        private readonly AuditDbContext auditContext;
        private readonly ListingDbContext listingContext;
        private readonly SharedDbContext sharedManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IHistoryAudit audit;
        private readonly IListingManager listingManager;

        public BookmarksController(AuditDbContext auditContext, ListingDbContext listingContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SharedDbContext sharedManager, IHistoryAudit audit, IListingManager listingManager)
        {
            this.auditContext = auditContext;
            this.listingContext = listingContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.sharedManager = sharedManager;
            this.audit = audit;
            this.listingManager = listingManager;
        }

        // GET: Analytics/Bookmarks
        public async Task<IActionResult> Index()
        {
            // Shafi: Get user guid
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            string UserGuid = user.Id;
            // End:

            // Shafi: Begin partial view model BookmarkListingViewModels
            var bookmarkList = await auditContext.Bookmarks.Where(b => b.UserGuid == UserGuid).ToListAsync();
            var bookmarkListingIDs = bookmarkList.Select(b => b.ListingID).ToList();
            var listingList = await listingContext.Listing.Where(l => bookmarkListingIDs.Contains(l.ListingID)).ToListAsync();
            // End:

            // Shafi: Join Bookarks and Listing
            var bookmarkModal = (from bookmark in bookmarkList
                                 join listing in listingList
                         on bookmark.ListingID equals listing.ListingID
                         select new BookmarkListingViewModel
                         {
                             BookmarkID = bookmark.BookmarksID,
                             ListingID = listing.ListingID,
                             CompanyName = listing.CompanyName,
                             VisitDate = bookmark.VisitDate.ToString(),
                             VisitTime = bookmark.VisitTime.ToString(),
                         }).ToList();
            // End:

            return View(bookmarkModal);
        }
    }
}
