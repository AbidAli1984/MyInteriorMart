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
    public class MagazinePlansController : Controller
    {
        private readonly BillingDbContext _context;

        public MagazinePlansController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Billing/MagazinePlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.MagazinePlan.ToListAsync());
        }

        // GET: Billing/MagazinePlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazinePlan = await _context.MagazinePlan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (magazinePlan == null)
            {
                return NotFound();
            }

            return View(magazinePlan);
        }

        // GET: Billing/MagazinePlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/MagazinePlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanID,Name,Priority,Price")] MagazinePlan magazinePlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(magazinePlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(magazinePlan);
        }

        // GET: Billing/MagazinePlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazinePlan = await _context.MagazinePlan.FindAsync(id);
            if (magazinePlan == null)
            {
                return NotFound();
            }
            return View(magazinePlan);
        }

        // POST: Billing/MagazinePlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,Name,Priority,Price")] MagazinePlan magazinePlan)
        {
            if (id != magazinePlan.PlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magazinePlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazinePlanExists(magazinePlan.PlanID))
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
            return View(magazinePlan);
        }

        // GET: Billing/MagazinePlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var magazinePlan = await _context.MagazinePlan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (magazinePlan == null)
            {
                return NotFound();
            }

            return View(magazinePlan);
        }

        // POST: Billing/MagazinePlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var magazinePlan = await _context.MagazinePlan.FindAsync(id);
            _context.MagazinePlan.Remove(magazinePlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagazinePlanExists(int id)
        {
            return _context.MagazinePlan.Any(e => e.PlanID == id);
        }
    }
}
