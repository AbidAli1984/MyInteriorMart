using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.AUDITTRAIL;
using DAL.AUDIT;
using BAL.Claims.Listing;
using Microsoft.AspNetCore.Identity;
using DAL.LISTING;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using BAL.Messaging.Contracts;
using StringRandomizer;
using StringRandomizer.Options;
using StringRandomizer.Stores;
using DAL.Models;
using BAL.Services.Contracts;

namespace ADMIN.Areas.Claims.Controllers
{
    [Area("Claims")]
    [Authorize]
    public class ListingClaimsController : Controller
    {
        public IUserService _userService;
        private readonly IWebHostEnvironment HostingEnvironment;
        private readonly AuditDbContext _context;
        public IConfiguration Configuration;
        public INotificationService Notification;

        public ListingClaimsController(IUserService userService, AuditDbContext context, IWebHostEnvironment hostingEnvironment, IConfiguration configuration, INotificationService notification)
        {
            this._userService = userService;
            _context = context;
            HostingEnvironment = hostingEnvironment;
            Configuration = configuration;
            Notification = notification;
        }

        public string RandomStringGenerator()
        {
            return "test";
        }

        // GET: Claims/ListingClaims
        public async Task<IActionResult> Index()
        {
            return View(await _context.ListingClaim.ToListAsync());
        }

        public async Task<IActionResult> Mobile()
        {
            return View(await _context.ListingClaim.Where(i => i.ClaimType == "Mobile").ToListAsync());
        }

        public async Task<IActionResult> Email()
        {
            return View(await _context.ListingClaim.Where(i => i.ClaimType == "Email").ToListAsync());
        }

        public async Task<IActionResult> Documents()
        {
            return View(await _context.ListingClaim.Where(i => i.ClaimType == "Document").ToListAsync());
        }

        [HttpGet]
        [Route("/Claims/ListingClaims/CheckDocuments/{ListingId}/{ClaimId}")]
        public IActionResult CheckDocuments(int ListingId, int ClaimId)
        {
            ViewBag.ListingId = ListingId;
            ViewBag.ClaimId = ClaimId;
            return View();
        }

        [HttpGet]
        [Route("/Claims/ListingClaims/ApproveDocuments/{ClaimId}/{Reason}")]
        public async Task<IActionResult> ApproveDocuments(int ClaimId, string Reason)
        {
            var currentUser = await _userService.GetUserByUserNameOrEmail(User.Identity.Name);
            var documentScrutinizedBy = currentUser.Id;

            var i = 0;
            RegenerateUniqueLink:

            var randomizer = new Randomizer(7, new DefaultRandomizerOptions(hasNumbers: true, hasLowerAlphabets: true, hasUpperAlphabets: false), new DefaultRandomizerStore());
            string claimVerificationShortLink = randomizer.Next();

            var checkIfLinkAlradyExists = await _context.ListingClaim.AnyAsync(i => i.ClaimVerificationShortLink == claimVerificationShortLink);

            if(checkIfLinkAlradyExists == true)
            {
                i++;

                if(i <= 3)
                {
                    goto RegenerateUniqueLink;
                }
                else
                {
                    TempData["Message"] = "Sorry! Document could not be not verified. Reason is a unique verification link coul'd not be generated. Please try again.";
                    return Redirect("/Claims/ListingClaims/");
                }
            }
            else
            {
                var claim = await _context.ListingClaim.Where(i => i.ClaimID == ClaimId).FirstOrDefaultAsync();
                claim.DocumentApprovedDisapprovedReasonByStaff = Reason;
                claim.Status = "OTP Verification Pending";
                claim.DocumentScrutinizedByStaffGuid = documentScrutinizedBy;
                claim.ClaimVerificationShortLink = claimVerificationShortLink;

                _context.Update(claim);
                await _context.SaveChangesAsync();

                string ClaimedByUserGuid = claim.ClaimedBy;
                int OTP = claim.OTP;
                var user = await _userService.GetUserById(ClaimedByUserGuid);
                string email = user.Email;
                string mobile = user.PhoneNumber;

                if (Configuration["EnableClaimByMobile"] == "Enabled")
                {
                    string verficationShortUrl = $"myinteriormart.com/cvl/{claimVerificationShortLink}";

                    string otpVerificationLink = $"{Configuration["WebsiteUrl"]}/Claims/ListingClaims/DocumentOTPVerificationLink/{OTP}/{claim.ClaimedBy}/{claim.ClaimID}";

                    Notification.SendSMS(mobile, $"Congratulation! We have approved your documents and your OTP for Claim Listing is {OTP}. Click this link {verficationShortUrl} and enter this OTP. Thankyou for using My Interior Mart.");

                    Notification.SendEmail(email, "OTP from My Interior Mart", $"Congratulation! We have approved your documents, OTP for Claim Listing is {OTP}, never share your OTP with anyone. <a href='{verficationShortUrl}' target='_blank'>Please click this link to enter this OTP.</a> Thankyou for using My Interior Mart.");
                }

                return View();
            }
        }

        
        [Route("/cvl/{shortLink}")]
        [HttpGet]
        public async Task<IActionResult> ClaimVerificationLink(string shortLink)
        {
            var user = await _userService.GetUserByUserNameOrEmail(User.Identity.Name);

            if (shortLink != "")
            {
                var claim = await _context.ListingClaim.Where(i => i.ClaimVerificationShortLink == shortLink).FirstOrDefaultAsync();
                if (claim != null && claim.ClaimedBy == user.Id)
                {
                    return Redirect($"/Claims/Listing/DocumentOtp/{shortLink}");
                }
                else
                {
                    return NotFound(); ;
                }
            }
            else
            {
                return NotFound(); ;
            }
        }


        [HttpGet]
        [Route("/Claims/ListingClaims/RejectDocuments/{ClaimId}/{Reason}")]
        public async Task<IActionResult> RejectDocuments(int ClaimId, string Reason)
        {
            var currentUser = await _userService.GetUserByUserNameOrEmail(User.Identity.Name);
            var documentScrutinizedBy = currentUser.Id;

            var claim = await _context.ListingClaim.Where(i => i.ClaimID == ClaimId).FirstOrDefaultAsync();
            claim.DocumentApprovedDisapprovedReasonByStaff = Reason;
            claim.Status = "Claim Rejected";
            claim.DocumentScrutinizedByStaffGuid = documentScrutinizedBy;
            _context.Update(claim);
            await _context.SaveChangesAsync();

            var user = await _userService.GetUserById(claim.ClaimedBy);
            string email = user.Email;
            string mobile = user.PhoneNumber;

            if (Configuration["EnableClaimByMobile"] == "Enabled")
            {
                Notification.SendSMS(mobile, $"Sorry! We have rejected your documents for Claim Listing. Reason is {Reason}. You can retry by uploading correct documents. Thankyou for using My Interior Mart.");

                Notification.SendEmail(email, "Sorry! We have rejected your documents.", $"Sorry! We have rejected your documents for Claim Listing. Reason is {Reason}. You can retry by uploading correct documents. Thankyou for using My Interior Mart.");
            }

            return View();
        }

        [HttpGet]
        [Route("/Claims/ListingClaims/OpenDocument/{ListingId}/{fileName}")]
        public IActionResult OpenDocuments(int ListingId, string fileName)
        {
            string folderPath = HostingEnvironment.ContentRootPath + $"\\AppData\\{ListingId}\\";

            var file = folderPath + fileName;

            var testFile = System.IO.File.ReadAllBytes(file);

            return File(testFile, "image/jpeg");
        }

        // GET: Claims/ListingClaims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listingClaim = await _context.ListingClaim
                .FirstOrDefaultAsync(m => m.ClaimID == id);
            if (listingClaim == null)
            {
                return NotFound();
            }

            return View(listingClaim);
        }

        // GET: Claims/ListingClaims/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Claims/ListingClaims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClaimID,DateTime,ListingID,ClaimedBy,ClaimType,Mobile,Email,Message,Status,OTP,OTPVerified,VerificationDate")] ListingClaim listingClaim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listingClaim);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listingClaim);
        }

        // GET: Claims/ListingClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listingClaim = await _context.ListingClaim.FindAsync(id);
            if (listingClaim == null)
            {
                return NotFound();
            }
            return View(listingClaim);
        }

        // POST: Claims/ListingClaims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClaimID,DateTime,ListingID,ClaimedBy,ClaimType,Mobile,Email,Message,Status,OTP,OTPVerified,VerificationDate")] ListingClaim listingClaim)
        {
            if (id != listingClaim.ClaimID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listingClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingClaimExists(listingClaim.ClaimID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(listingClaim);
        }

        // GET: Claims/ListingClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listingClaim = await _context.ListingClaim
                .FirstOrDefaultAsync(m => m.ClaimID == id);
            if (listingClaim == null)
            {
                return NotFound();
            }

            return View(listingClaim);
        }

        // POST: Claims/ListingClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listingClaim = await _context.ListingClaim.FindAsync(id);
            _context.ListingClaim.Remove(listingClaim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListingClaimExists(int id)
        {
            return _context.ListingClaim.Any(e => e.ClaimID == id);
        }
    }
}
