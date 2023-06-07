using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.CATEGORIES;
using DAL.CATEGORIES;
using Microsoft.AspNetCore.Authorization;

namespace ADMIN.Areas.CMS.Controllers
{
    [Area("CMS")]
    public class PagesController : Controller
    {
        private readonly CategoriesDbContext _context;

        public PagesController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: CMS/Pages
        [Authorize(Policy = "Admin-Page-ViewAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pages.ToListAsync());
        }

        // GET: CMS/Pages/Details/5
        [Authorize(Policy = "Admin-Page-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pages = await _context.Pages
                .FirstOrDefaultAsync(m => m.PageID == id);
            if (pages == null)
            {
                return NotFound();
            }

            return View(pages);
        }

        // GET: CMS/Pages/Create
        [Authorize(Policy = "Admin-Page-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CMS/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Page-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageID,PageName,Priority,URL,Description,Keywords")] Pages pages)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pages);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pages);
        }

        // GET: CMS/Pages/Edit/5
        [Authorize(Policy = "Admin-Page-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pages = await _context.Pages.FindAsync(id);
            if (pages == null)
            {
                return NotFound();
            }
            return View(pages);
        }

        // POST: CMS/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Page-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PageID,PageName,Priority,URL,Description,Keywords")] Pages pages)
        {
            if (id != pages.PageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pages);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PagesExists(pages.PageID))
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
            return View(pages);
        }

        // GET: CMS/Pages/Delete/5
        [Authorize(Policy = "Admin-Page-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pages = await _context.Pages
                .FirstOrDefaultAsync(m => m.PageID == id);
            if (pages == null)
            {
                return NotFound();
            }

            return View(pages);
        }

        // POST: CMS/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-Page-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pages = await _context.Pages.FindAsync(id);
            _context.Pages.Remove(pages);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PagesExists(int id)
        {
            return _context.Pages.Any(e => e.PageID == id);
        }
    }
}
