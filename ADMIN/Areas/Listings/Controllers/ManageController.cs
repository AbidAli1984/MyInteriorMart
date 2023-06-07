using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BOL.LISTING;
using BOL.AUDITTRAIL;
using BOL.BILLING;
using BOL.CATEGORIES;
using BOL.PLAN;
using BOL.SHARED;
using BOL.VIEWMODELS;
using DAL.LISTING;
using DAL.AUDIT;
using DAL.BILLING;
using DAL.CATEGORIES;
using DAL.SHARED;
using Microsoft.EntityFrameworkCore;
using IDENTITY.Data;
using BAL.Audit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BAL.Listings;
using Microsoft.AspNetCore.Hosting;
using BAL.Messaging.Notify;
using System.Text;

namespace ADMIN.Areas.Listings.Controllers
{
    [Area("Listings")]
    [Authorize]
    public class ManageController : Controller
    {
        private readonly ListingDbContext listingContext;
        private readonly AuditDbContext auditContext;
        private readonly BillingDbContext billingContext;
        private readonly CategoriesDbContext categoriesContext;
        private readonly SharedDbContext sharedContext;
        private readonly ApplicationDbContext applicationContext;
        private readonly IHistoryAudit historyAudit;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IListingManager listingManager;
        private readonly IWebHostEnvironment webHost;
        private readonly INotification notification;

        public ManageController(ListingDbContext listingContext, AuditDbContext auditContext, BillingDbContext billingContext, CategoriesDbContext categoriesContext, SharedDbContext sharedContext, ApplicationDbContext applicationContext, IHistoryAudit historyAudit, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IListingManager listingManager, IWebHostEnvironment webHost, INotification notification)
        {
            this.listingContext = listingContext;
            this.auditContext = auditContext;
            this.billingContext = billingContext;
            this.categoriesContext = categoriesContext;
            this.sharedContext = sharedContext;
            this.applicationContext = applicationContext;
            this.historyAudit = historyAudit;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.listingManager = listingManager;
            this.webHost = webHost;
            this.notification = notification;
        }

        [Authorize(Policy = "Admin-Listing-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var result = from list in listingContext.Listing
                         join comm in listingContext.Communication
                         on list.ListingID equals comm.ListingID

                         join add in listingContext.Address
                         on list.ListingID equals add.ListingID

                         join cat in listingContext.Categories
                               on list.ListingID equals cat.ListingID

                         join spec in listingContext.Specialisation
                               on list.ListingID equals spec.ListingID

                         join work in listingContext.WorkingHours
                               on list.ListingID equals work.ListingID

                         join pay in listingContext.PaymentMode
                               on list.ListingID equals pay.ListingID

                         select new FreeListingViewModel
                         {
                             Listing = list,
                             Communication = comm,
                             //Address = add,
                             //Categories = cat,
                             Specialisation = spec,
                             //WorkingHours = work,
                             PaymentMode = pay
                         };

            return View(await result.OrderByDescending(l => l.Listing.CreatedDate).ToListAsync());
        }

        [Authorize(Policy = "Admin-Review-ViewAll")]
        public async Task<IActionResult> AllReviews()
        {
            var reviews = await listingContext.Rating.OrderByDescending(r => r.RatingID).ToListAsync();
            return View(reviews);
        }

        [Authorize(Policy = "Admin-Like-ViewAll")]
        public async Task<IActionResult> AllLikes()
        {
            var likesDislike = await auditContext.ListingLikeDislike.OrderByDescending(r => r.LikeDislikeID).ToListAsync();
            return View(likesDislike);
        }

        [Authorize(Policy = "Admin-Subscribe-ViewAll")]
        public async Task<IActionResult> AllSubscribes()
        {
            var subscribes = await auditContext.Subscribes.OrderByDescending(r => r.SubscribeID).ToListAsync();
            return View(subscribes);
        }

        [Authorize(Policy = "Admin-Bookmark-ViewAll")]
        public async Task<IActionResult> AllBookmarks()
        {
            var bookmarks = await auditContext.Bookmarks.OrderByDescending(r => r.BookmarksID).ToListAsync();
            return View(bookmarks);
        }

        //[Authorize(Policy = "Admin-Listing-Pending-Approval")]
        public async Task<IActionResult> PendingApproval()
        {
            var result = from list in listingContext.Listing
                         join comm in listingContext.Communication
                         on list.ListingID equals comm.ListingID

                         join add in listingContext.Address
                         on list.ListingID equals add.ListingID

                         join cat in listingContext.Categories
                               on list.ListingID equals cat.ListingID

                         join spec in listingContext.Specialisation
                               on list.ListingID equals spec.ListingID

                         join work in listingContext.WorkingHours
                               on list.ListingID equals work.ListingID

                         join pay in listingContext.PaymentMode
                               on list.ListingID equals pay.ListingID

                         select new FreeListingViewModel
                         {
                             Listing = list,
                             Communication = comm,
                             //Address = add,
                             //Categories = cat,
                             Specialisation = spec,
                             //WorkingHours = work,
                             PaymentMode = pay
                         };

            return View(await result.OrderByDescending(l => l.Listing.CreatedDate).ToListAsync());
        }

        public async Task<JsonResult> ApproveListing(int? id)
        {
            if(id == null)
            {
                return Json("No Record Found");
            }
            else
            {
                var listing = await listingContext.Listing.Where(i => i.ListingID == id).FirstOrDefaultAsync();
                listing.Approved = true;
                listingContext.Update(listing);
                await listingContext.SaveChangesAsync();

                return Json("Success");
            }
        }

        [HttpPost]
        public async Task<JsonResult> DisapproveListing(int? id)
        {
            if (id == null)
            {
                return Json("No Record Found");
            }
            else
            {
                var listing = await listingContext.Listing.Where(i => i.ListingID == id).FirstOrDefaultAsync();
                listing.Approved = false;
                listingContext.Update(listing);
                await listingContext.SaveChangesAsync();
                return Json("Success");
            }
        }

        public async Task<JsonResult> RejectListing(int? id)
        {
            if (id == null)
            {
                return Json("No Record Found");
            }
            else
            {
                var listing = await listingContext.Listing.Where(i => i.ListingID == id).FirstOrDefaultAsync();
                listing.Rejected = true;
                listingContext.Update(listing);
                await listingContext.SaveChangesAsync();
                return Json("Success");
            }
        }

        public async Task<JsonResult> UnrejectListing(int? id)
        {
            if (id == null)
            {
                return Json("No Record Found");
            }
            else
            {
                var listing = await listingContext.Listing.Where(i => i.ListingID == id).FirstOrDefaultAsync();
                listing.Rejected = false;
                listingContext.Update(listing);
                await listingContext.SaveChangesAsync();
                return Json("Success");
            }
        }

    }
}
