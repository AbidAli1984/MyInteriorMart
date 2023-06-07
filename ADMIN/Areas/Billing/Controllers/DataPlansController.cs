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
    public class DataPlansController : Controller
    {
        private readonly BillingDbContext _context;

        public DataPlansController(BillingDbContext context)
        {
            _context = context;
        }

        // GET: Billing/DataPlans
        public async Task<IActionResult> Index()
        {
            return View(await _context.DataPlan.ToListAsync());
        }

        // GET: Billing/DataPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPlan = await _context.DataPlan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (dataPlan == null)
            {
                return NotFound();
            }

            return View(dataPlan);
        }

        // GET: Billing/DataPlans/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Billing/DataPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanID,Name,Priority,Price")] DataPlan dataPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dataPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dataPlan);
        }

        // GET: Billing/DataPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPlan = await _context.DataPlan.FindAsync(id);
            if (dataPlan == null)
            {
                return NotFound();
            }
            return View(dataPlan);
        }

        // POST: Billing/DataPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanID,Name,Priority,Price")] DataPlan dataPlan)
        {
            if (id != dataPlan.PlanID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dataPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DataPlanExists(dataPlan.PlanID))
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
            return View(dataPlan);
        }

        // GET: Billing/DataPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dataPlan = await _context.DataPlan
                .FirstOrDefaultAsync(m => m.PlanID == id);
            if (dataPlan == null)
            {
                return NotFound();
            }

            return View(dataPlan);
        }

        // POST: Billing/DataPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dataPlan = await _context.DataPlan.FindAsync(id);
            _context.DataPlan.Remove(dataPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DataPlanExists(int id)
        {
            return _context.DataPlan.Any(e => e.PlanID == id);
        }
    }
}
