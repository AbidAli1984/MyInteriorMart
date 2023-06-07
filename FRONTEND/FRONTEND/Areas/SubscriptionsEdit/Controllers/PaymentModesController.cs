﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.LISTING;
using DAL.LISTING;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Http;
using DAL.SHARED;
using BAL.Listings;
using Microsoft.AspNetCore.Identity.UI.Services;
using BAL.Audit;
using System.Data;
using System;
using System.Collections.Generic;

namespace FRONTEND.Areas.SubscriptionsEdit.Controllers
{
    [Area("SubscriptionsEdit")]
    public class PaymentModesController : Controller
    {
        private readonly ListingDbContext listingContext;
        private readonly SharedDbContext sharedContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IEmailSender emailSender;
        private readonly IHistoryAudit audit;
        private readonly IListingManager listingManager;

        public PaymentModesController(ListingDbContext listingContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SharedDbContext sharedContext, IEmailSender emailSender, IHistoryAudit audit, IListingManager listingManager)
        {
            this.listingContext = listingContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.sharedContext = sharedContext;
            this.emailSender = emailSender;
            this.audit = audit;
            this.listingManager = listingManager;
        }

        // GET: SubscriptionsEdit/PaymentModes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            // Shafi: Get UserGuid & IP Address
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
            string ownerGuid = user.Id;
            // End:

            if (id == null)
            {
                return NotFound();
            }

            var paymentMode = await listingContext.PaymentMode
                .FirstOrDefaultAsync(m => m.ListingID == id);
            if (paymentMode == null)
            {
                return NotFound();
            }

            // Shafi: Find listing
            var listing = await listingContext.Listing.Where(l => l.ListingID == paymentMode.ListingID).FirstOrDefaultAsync();
            // End: 

            // Shafi: Verify ownership record
            var owner = await listingManager.PaymentOwnerAsync(paymentMode.PaymentID, listing.ListingID, ownerGuid);

            if (owner == true)
            {
                return View(paymentMode);
            }
            else
            {
                return NotFound();
            }
            // End:
        }

        // GET: SubscriptionsEdit/PaymentModes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            // Shafi: Get UserGuid & IP Address
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
            string ownerGuid = user.Id;
            // End:s

            if (id == null)
            {
                return NotFound();
            }

            var paymentMode = await listingContext.PaymentMode.FindAsync(id);
            if (paymentMode == null)
            {
                return NotFound();
            }

            // Shafi: Find listing
            var listing = await listingContext.Listing.Where(l => l.ListingID == paymentMode.ListingID).FirstOrDefaultAsync();
            // End: 

            // Shafi: Verify ownership record
            var owner = await listingManager.PaymentOwnerAsync(paymentMode.PaymentID, listing.ListingID, ownerGuid);
            if (owner == true)
            {
                return View(paymentMode);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: SubscriptionsEdit/PaymentModes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentID,ListingID,OwnerGuid,IPAddress,Cash,NetBanking,Cheque,RtgsNeft,DebitCard,CreditCard,PayTM,PhonePay,Paypal")] PaymentMode paymentMode)
        {
            // Shafi: Get UserGuid & IP Address
            IdentityUser user = await userManager.FindByNameAsync(User.Identity.Name);
            string remoteIpAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
            string ownerGuid = user.Id;
            // End:

            if (id != paymentMode.PaymentID)
            {
                return NotFound();
            }

            // Shafi: Find listing
            var listing = await listingContext.Listing.Where(l => l.ListingID == paymentMode.ListingID).FirstOrDefaultAsync();
            // End: 

            // Shafi: Verify ownership record
            var owner = await listingManager.PaymentOwnerAsync(paymentMode.PaymentID, listing.ListingID, ownerGuid);
            if (owner == true && listing != null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        // Shafi: Create last updated
                        string ipAddress = this.HttpContext.Connection.RemoteIpAddress.ToString();
                        string userAgent = this.HttpContext.Request.Headers["User-Agent"]; ;
                        string referUrl = this.HttpContext.Request.Headers["Referer"];
                        string visitedURL = this.HttpContext.Request.Headers["Host"];
                        string userGuid = user.Id;
                        string email = user.Email;
                        // Shafi: Time zone
                        string mobile = user.PhoneNumber;
                        DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        string updatedDate = timeZoneDate.ToString("d-MM-yyyy");
                        string updatedTime = timeZoneDate.ToString("hh:mm:ss tt");
                        // End:

                        // Shafi: Save context
                        paymentMode.IPAddress = ipAddress;
                        listingContext.Update(paymentMode);
                        await listingContext.SaveChangesAsync();
                        // End:

                        string updatedUrl = this.HttpContext.Request.Headers["Host"] + "/SubscriptionsEdit/PaymentModes/Details/5" + listing.ListingID;
                        string activity = "Updated Payment Modes for " + listing.CompanyName + " with id " + listing.ListingID;

                        // Shafi: Get user in roles
                        IList<string> userInRoleName = await userManager.GetRolesAsync(user);
                        string roleName = userInRoleName.FirstOrDefault();
                        // End:

                        await audit.CreateListingLastUpdatedAsync(listing.ListingID, userGuid, email, mobile, ipAddress, roleName, "Payment Modes", updatedDate, updatedTime, updatedUrl, userAgent, activity);
                        // End:

                        // Shafi: Show success message in redirected view
                        TempData["SuccessMessage"] = "Payment Modes updated successfully.";
                        // End:

                        return Redirect("/SubscriptionsEdit/PaymentModes/Details/" + listing.ListingID);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PaymentModeExists(paymentMode.PaymentID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    return View(paymentMode);
                }
            }
            else
            {
                return NotFound();
            }
        }

        private bool PaymentModeExists(int id)
        {
            return listingContext.PaymentMode.Any(e => e.PaymentID == id);
        }
    }
}
