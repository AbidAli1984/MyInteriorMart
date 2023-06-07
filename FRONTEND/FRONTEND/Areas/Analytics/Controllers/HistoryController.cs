﻿using System;
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

namespace FRONTEND.Areas.Analytics.Controllers
{
    [Area("Analytics")]
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly AuditDbContext auditContext;
        private readonly ListingDbContext listingContext;
        private readonly SharedDbContext sharedManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IHistoryAudit audit;
        private readonly IListingManager listingManager;

        public HistoryController(AuditDbContext auditContext, ListingDbContext listingContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SharedDbContext sharedManager, IHistoryAudit audit, IListingManager listingManager)
        {
            this.auditContext = auditContext;
            this.listingContext = listingContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.sharedManager = sharedManager;
            this.audit = audit;
            this.listingManager = listingManager;
        }

        // GET: Analytics/History
        public async Task<IActionResult> Index()
        {
            // Shafi: Get user guid
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            string UserGuid = user.Id;
            // End:

            return View(await auditContext.UserHistory.Where(h => h.UserGuid == UserGuid).OrderByDescending(h => h.HistoryID).ToListAsync());
        }
    }
}
