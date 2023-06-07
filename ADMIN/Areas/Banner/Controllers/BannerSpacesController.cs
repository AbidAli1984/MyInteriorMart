using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.BANNER;
using DAL.BANNER;

namespace ADMIN.Areas.Banner.Controllers
{
    [Area("Banner")]
    public class BannerSpacesController : Controller
    {
        private readonly BannerDbContext _context;

        public BannerSpacesController(BannerDbContext context)
        {
            _context = context;
        }

        // GET: Banner/BannerSpaces
        public async Task<IActionResult> Index()
        {
            var bannerDbContext = _context.BannerSpace.Include(b => b.BannerPage);
            return View(await bannerDbContext.ToListAsync());
        }

        // GET: Banner/BannerSpaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerSpace = await _context.BannerSpace
                .Include(b => b.BannerPage)
                .FirstOrDefaultAsync(m => m.BannerSpaceID == id);
            if (bannerSpace == null)
            {
                return NotFound();
            }

            return View(bannerSpace);
        }

        // GET: Banner/BannerSpaces/Create
        public IActionResult Create()
        {
            ViewData["BannerPageID"] = new SelectList(_context.BannerPage, "BannerPageID", "PageName");
            return View();
        }

        // POST: Banner/BannerSpaces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BannerSpaceID,BannerPageID,SpaceName,Width,Height")] BannerSpace bannerSpace)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bannerSpace);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BannerPageID"] = new SelectList(_context.BannerPage, "BannerPageID", "PageName", bannerSpace.BannerPageID);
            return View(bannerSpace);
        }

        // GET: Banner/BannerSpaces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerSpace = await _context.BannerSpace.FindAsync(id);
            if (bannerSpace == null)
            {
                return NotFound();
            }
            ViewData["BannerPageID"] = new SelectList(_context.BannerPage, "BannerPageID", "PageName", bannerSpace.BannerPageID);
            return View(bannerSpace);
        }

        // POST: Banner/BannerSpaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BannerSpaceID,BannerPageID,SpaceName,Width,Height")] BannerSpace bannerSpace)
        {
            if (id != bannerSpace.BannerSpaceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bannerSpace);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerSpaceExists(bannerSpace.BannerSpaceID))
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
            ViewData["BannerPageID"] = new SelectList(_context.BannerPage, "BannerPageID", "PageName", bannerSpace.BannerPageID);
            return View(bannerSpace);
        }

        // GET: Banner/BannerSpaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerSpace = await _context.BannerSpace
                .Include(b => b.BannerPage)
                .FirstOrDefaultAsync(m => m.BannerSpaceID == id);
            if (bannerSpace == null)
            {
                return NotFound();
            }

            return View(bannerSpace);
        }

        // POST: Banner/BannerSpaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bannerSpace = await _context.BannerSpace.FindAsync(id);
            _context.BannerSpace.Remove(bannerSpace);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerSpaceExists(int id)
        {
            return _context.BannerSpace.Any(e => e.BannerSpaceID == id);
        }
    }
}
