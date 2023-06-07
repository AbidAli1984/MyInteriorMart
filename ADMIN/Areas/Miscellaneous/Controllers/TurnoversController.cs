using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.SHARED;
using DAL.SHARED;
using Microsoft.AspNetCore.Authorization;

namespace ADMIN.Areas.Miscellaneous.Controllers
{
    [Area("Miscellaneous")]
    [Authorize]
    public class TurnoversController : Controller
    {
        private readonly SharedDbContext _context;

        public TurnoversController(SharedDbContext context)
        {
            _context = context;
        }

        // GET: Miscellaneous/Turnovers
        [Authorize(Policy = "Admin-Turnover-ViewAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Turnover.ToListAsync());
        }

        // GET: Miscellaneous/Turnovers/Details/5
        [Authorize(Policy = "Admin-Turnover-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnover = await _context.Turnover
                .FirstOrDefaultAsync(m => m.TurnoverID == id);
            if (turnover == null)
            {
                return NotFound();
            }

            return View(turnover);
        }

        // GET: Miscellaneous/Turnovers/Create
        [Authorize(Policy = "Admin-Turnover-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miscellaneous/Turnovers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Turnover-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurnoverID,Name")] Turnover turnover)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turnover);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turnover);
        }

        // GET: Miscellaneous/Turnovers/Edit/5
        [Authorize(Policy = "Admin-Turnover-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnover = await _context.Turnover.FindAsync(id);
            if (turnover == null)
            {
                return NotFound();
            }
            return View(turnover);
        }

        // POST: Miscellaneous/Turnovers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Turnover-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurnoverID,Name")] Turnover turnover)
        {
            if (id != turnover.TurnoverID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turnover);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnoverExists(turnover.TurnoverID))
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
            return View(turnover);
        }

        // GET: Miscellaneous/Turnovers/Delete/5
        [Authorize(Policy = "Admin-Turnover-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnover = await _context.Turnover
                .FirstOrDefaultAsync(m => m.TurnoverID == id);
            if (turnover == null)
            {
                return NotFound();
            }

            return View(turnover);
        }

        // POST: Miscellaneous/Turnovers/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-Turnover-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turnover = await _context.Turnover.FindAsync(id);
            _context.Turnover.Remove(turnover);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoverExists(int id)
        {
            return _context.Turnover.Any(e => e.TurnoverID == id);
        }
    }
}
