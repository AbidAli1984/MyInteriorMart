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
    public class NatureOfBusinessesController : Controller
    {
        private readonly SharedDbContext _context;

        public NatureOfBusinessesController(SharedDbContext context)
        {
            _context = context;
        }

        // GET: Miscellaneous/NatureOfBusinesses
        [Authorize(Policy = "Admin-NatureofBusiness-ViewAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.NatureOfBusiness.ToListAsync());
        }

        // GET: Miscellaneous/NatureOfBusinesses/Details/5
        [Authorize(Policy = "Admin-NatureofBusiness-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natureOfBusiness = await _context.NatureOfBusiness
                .FirstOrDefaultAsync(m => m.NatureOfBusinessID == id);
            if (natureOfBusiness == null)
            {
                return NotFound();
            }

            return View(natureOfBusiness);
        }

        // GET: Miscellaneous/NatureOfBusinesses/Create
        [Authorize(Policy = "Admin-NatureofBusiness-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Miscellaneous/NatureOfBusinesses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-NatureofBusiness-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NatureOfBusinessID,Name")] NatureOfBusiness natureOfBusiness)
        {
            if (ModelState.IsValid)
            {
                _context.Add(natureOfBusiness);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(natureOfBusiness);
        }

        // GET: Miscellaneous/NatureOfBusinesses/Edit/5
        [Authorize(Policy = "Admin-NatureofBusiness-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natureOfBusiness = await _context.NatureOfBusiness.FindAsync(id);
            if (natureOfBusiness == null)
            {
                return NotFound();
            }
            return View(natureOfBusiness);
        }

        // POST: Miscellaneous/NatureOfBusinesses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-NatureofBusiness-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NatureOfBusinessID,Name")] NatureOfBusiness natureOfBusiness)
        {
            if (id != natureOfBusiness.NatureOfBusinessID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(natureOfBusiness);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NatureOfBusinessExists(natureOfBusiness.NatureOfBusinessID))
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
            return View(natureOfBusiness);
        }

        // GET: Miscellaneous/NatureOfBusinesses/Delete/5
        [Authorize(Policy = "Admin-NatureofBusiness-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var natureOfBusiness = await _context.NatureOfBusiness
                .FirstOrDefaultAsync(m => m.NatureOfBusinessID == id);
            if (natureOfBusiness == null)
            {
                return NotFound();
            }

            return View(natureOfBusiness);
        }

        // POST: Miscellaneous/NatureOfBusinesses/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-NatureofBusiness-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var natureOfBusiness = await _context.NatureOfBusiness.FindAsync(id);
            _context.NatureOfBusiness.Remove(natureOfBusiness);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NatureOfBusinessExists(int id)
        {
            return _context.NatureOfBusiness.Any(e => e.NatureOfBusinessID == id);
        }
    }
}
