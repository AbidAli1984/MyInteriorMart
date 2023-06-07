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
    public class SMSPlansController : Controller
    {
        private readonly BillingDbContext _context;

        public SMSPlansController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Billing/SMSPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.SMSPlans.ToListAsync());
        }

        // GET: Billing/SMSPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSPlans = await _context.SMSPlans
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (sMSPlans == null)
            {
                return NotFound();
            }

            return View(sMSPlans);
        }

        // GET: Billing/SMSPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/SMSPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanID,Name,Priority,Price")] SMSPlans sMSPlans)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sMSPlans);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sMSPlans);
        }

        // GET: Billing/SMSPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSPlans = await _context.SMSPlans.FindAsync(id);
            if (sMSPlans == null)
            {
                return NotFound();
            }
            return View(sMSPlans);
        }

        // POST: Billing/SMSPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,Name,Priority,Price")] SMSPlans sMSPlans)
        {
            if (id != sMSPlans.PlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sMSPlans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SMSPlansExists(sMSPlans.PlanID))
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
            return View(sMSPlans);
        }

        // GET: Billing/SMSPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sMSPlans = await _context.SMSPlans
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (sMSPlans == null)
            {
                return NotFound();
            }

            return View(sMSPlans);
        }

        // POST: Billing/SMSPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sMSPlans = await _context.SMSPlans.FindAsync(id);
            _context.SMSPlans.Remove(sMSPlans);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SMSPlansExists(int id)
        {
            return _context.SMSPlans.Any(e => e.PlanID == id);
        }
    }
}
