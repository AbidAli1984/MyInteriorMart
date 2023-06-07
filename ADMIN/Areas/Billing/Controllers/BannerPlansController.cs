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
    public class BannerPlansController : Controller
    {
        private readonly BillingDbContext _context;

        public BannerPlansController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Billing/BannerPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.BannerPlan.ToListAsync());
        }

        // GET: Billing/BannerPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerPlan = await _context.BannerPlan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (bannerPlan == null)
            {
                return NotFound();
            }

            return View(bannerPlan);
        }

        // GET: Billing/BannerPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/BannerPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanID,Name,Priority,MonthlyPrice")] BannerPlan bannerPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bannerPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bannerPlan);
        }

        // GET: Billing/BannerPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerPlan = await _context.BannerPlan.FindAsync(id);
            if (bannerPlan == null)
            {
                return NotFound();
            }
            return View(bannerPlan);
        }

        // POST: Billing/BannerPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,Name,Priority,MonthlyPrice")] BannerPlan bannerPlan)
        {
            if (id != bannerPlan.PlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bannerPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerPlanExists(bannerPlan.PlanID))
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
            return View(bannerPlan);
        }

        // GET: Billing/BannerPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerPlan = await _context.BannerPlan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (bannerPlan == null)
            {
                return NotFound();
            }

            return View(bannerPlan);
        }

        // POST: Billing/BannerPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bannerPlan = await _context.BannerPlan.FindAsync(id);
            _context.BannerPlan.Remove(bannerPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerPlanExists(int id)
        {
            return _context.BannerPlan.Any(e => e.PlanID == id);
        }
    }
}
