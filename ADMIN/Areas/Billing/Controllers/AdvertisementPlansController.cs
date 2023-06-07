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
    public class AdvertisementPlansController : Controller
    {
        private readonly BillingDbContext _context;

        public AdvertisementPlansController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Billing/AdvertisementPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdvertisementPlan.ToListAsync());
        }

        // GET: Billing/AdvertisementPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementPlan = await _context.AdvertisementPlan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (advertisementPlan == null)
            {
                return NotFound();
            }

            return View(advertisementPlan);
        }

        // GET: Billing/AdvertisementPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/AdvertisementPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanID,Name,Priority,Price")] AdvertisementPlan advertisementPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertisementPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(advertisementPlan);
        }

        // GET: Billing/AdvertisementPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementPlan = await _context.AdvertisementPlan.FindAsync(id);
            if (advertisementPlan == null)
            {
                return NotFound();
            }
            return View(advertisementPlan);
        }

        // POST: Billing/AdvertisementPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,Name,Priority,Price")] AdvertisementPlan advertisementPlan)
        {
            if (id != advertisementPlan.PlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisementPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementPlanExists(advertisementPlan.PlanID))
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
            return View(advertisementPlan);
        }

        // GET: Billing/AdvertisementPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisementPlan = await _context.AdvertisementPlan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (advertisementPlan == null)
            {
                return NotFound();
            }

            return View(advertisementPlan);
        }

        // POST: Billing/AdvertisementPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisementPlan = await _context.AdvertisementPlan.FindAsync(id);
            _context.AdvertisementPlan.Remove(advertisementPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementPlanExists(int id)
        {
            return _context.AdvertisementPlan.Any(e => e.PlanID == id);
        }
    }
}
