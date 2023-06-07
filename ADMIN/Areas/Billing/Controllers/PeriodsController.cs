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
    public class PeriodsController : Controller
    {
        private readonly BillingDbContext _context;

        public PeriodsController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Billing/Periods
        public async Task<IActionResult> Index()
        {
            return View(await _context.Period.ToListAsync());
        }

        // GET: Billing/Periods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _context.Period
                .FirstOrDefaultAsync(m => m.PeriodID == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // GET: Billing/Periods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/Periods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeriodID,Name,DurationInMonths")] Period period)
        {
            if (ModelState.IsValid)
            {
                _context.Add(period);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(period);
        }

        // GET: Billing/Periods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _context.Period.FindAsync(id);
            if (period == null)
            {
                return NotFound();
            }
            return View(period);
        }

        // POST: Billing/Periods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeriodID,Name,DurationInMonths")] Period period)
        {
            if (id != period.PeriodID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(period);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodExists(period.PeriodID))
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
            return View(period);
        }

        // GET: Billing/Periods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var period = await _context.Period
                .FirstOrDefaultAsync(m => m.PeriodID == id);
            if (period == null)
            {
                return NotFound();
            }

            return View(period);
        }

        // POST: Billing/Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var period = await _context.Period.FindAsync(id);
            _context.Period.Remove(period);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodExists(int id)
        {
            return _context.Period.Any(e => e.PeriodID == id);
        }
    }
}
