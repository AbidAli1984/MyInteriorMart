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
    public class DesignationsController : Controller
    {
        private readonly SharedDbContext _context;

        public DesignationsController(SharedDbContext context)
        {
            _context = context;
        }

        // GET: Miscellaneous/Designations
        [Authorize(Policy = "Admin-Designation-ViewAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Designation.ToListAsync());
        }

        // GET: Miscellaneous/Designations/Details/5
        [Authorize(Policy = "Admin-Designation-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designation
                .FirstOrDefaultAsync(m => m.DesignationID == id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        // GET: Miscellaneous/Designations/Create
        [Authorize(Policy = "Admin-Designation-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miscellaneous/Designations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Designation-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DesignationID,Name")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(designation);
        }

        // GET: Miscellaneous/Designations/Edit/5
        [Authorize(Policy = "Admin-Designation-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designation.FindAsync(id);
            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }

        // POST: Miscellaneous/Designations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Designation-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DesignationID,Name")] Designation designation)
        {
            if (id != designation.DesignationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignationExists(designation.DesignationID))
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
            return View(designation);
        }

        // GET: Miscellaneous/Designations/Delete/5
        [Authorize(Policy = "Admin-Designation-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designation
                .FirstOrDefaultAsync(m => m.DesignationID == id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        // POST: Miscellaneous/Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-Designation-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var designation = await _context.Designation.FindAsync(id);
            _context.Designation.Remove(designation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DesignationExists(int id)
        {
            return _context.Designation.Any(e => e.DesignationID == id);
        }
    }
}
