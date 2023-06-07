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
    public class EmailPlansController : Controller
    {
        private readonly BillingDbContext _context;

        public EmailPlansController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Billing/EmailPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.EmailPlans.ToListAsync());
        }

        // GET: Billing/EmailPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailPlans = await _context.EmailPlans
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (emailPlans == null)
            {
                return NotFound();
            }

            return View(emailPlans);
        }

        // GET: Billing/EmailPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/EmailPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanID,Name,Priority,Price")] EmailPlans emailPlans)
        {
            if (ModelState.IsValid)
            {
                _context.Add(emailPlans);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emailPlans);
        }

        // GET: Billing/EmailPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailPlans = await _context.EmailPlans.FindAsync(id);
            if (emailPlans == null)
            {
                return NotFound();
            }
            return View(emailPlans);
        }

        // POST: Billing/EmailPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,Name,Priority,Price")] EmailPlans emailPlans)
        {
            if (id != emailPlans.PlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emailPlans);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailPlansExists(emailPlans.PlanID))
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
            return View(emailPlans);
        }

        // GET: Billing/EmailPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailPlans = await _context.EmailPlans
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (emailPlans == null)
            {
                return NotFound();
            }

            return View(emailPlans);
        }

        // POST: Billing/EmailPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailPlans = await _context.EmailPlans.FindAsync(id);
            _context.EmailPlans.Remove(emailPlans);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailPlansExists(int id)
        {
            return _context.EmailPlans.Any(e => e.PlanID == id);
        }
    }
}
