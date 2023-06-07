using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.PLAN;
using DAL.LISTING;
using DAL.BILLING;

namespace ADMIN.Areas.Billing.Controllers
{
    [Area("Billing")]
    public class PlansController : Controller
    {
        private readonly BillingDbContext _context;

        public PlansController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Billing/Plans
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plan.ToListAsync());
        }

        // GET: Billing/Plans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // GET: Billing/Plans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/Plans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanID,Name,Priority,MonthlyPrice,Categories,Offers,Products,JobPostings,RecentProjects,City,SmsNotifications,EmailNotifications,UserHistory,Analytics,MembershipBadge,PhotoGallery,MultipleLocations,OneHourService,Team,MonopolyProducts,Branches,ClientLogo,PartnerProfile,MessagesInbox,BrandShowcase,PreferredPlaces,Wallet,WorkingHours,PaymentMethods,CustomServices,SocialSites,Certification,Profile,Specialisation,Address,MultipleAssemblies,MultipleCities,Company,Communication,SocialShareButtons,Reviews,LikeDislike,FollowListing,CSSClassName")] Plan plan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        // GET: Billing/Plans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan.FindAsync(id);
            if (plan == null)
            {
                return NotFound();
            }
            return View(plan);
        }

        // POST: Billing/Plans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,Name,Priority,MonthlyPrice,Categories,Offers,Products,JobPostings,RecentProjects,City,SmsNotifications,EmailNotifications,UserHistory,Analytics,MembershipBadge,PhotoGallery,MultipleLocations,OneHourService,Team,MonopolyProducts,Branches,ClientLogo,PartnerProfile,MessagesInbox,BrandShowcase,PreferredPlaces,Wallet,WorkingHours,PaymentMethods,CustomServices,SocialSites,Certification,Profile,Specialisation,Address,MultipleAssemblies,MultipleCities,Company,Communication,SocialShareButtons,Reviews,LikeDislike,FollowListing,CSSClassName")] Plan plan)
        {
            if (id != plan.PlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanExists(plan.PlanID))
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
            return View(plan);
        }

        // GET: Billing/Plans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plan = await _context.Plan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (plan == null)
            {
                return NotFound();
            }

            return View(plan);
        }

        // POST: Billing/Plans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plan = await _context.Plan.FindAsync(id);
            _context.Plan.Remove(plan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanExists(int id)
        {
            return _context.Plan.Any(e => e.PlanID == id);
        }
    }
}
