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
    public class BannerPagesController : Controller
    {
        private readonly BannerDbContext _context;

        public BannerPagesController(BannerDbContext context)
        {
            _context = context;
        }

        // GET: Banner/BannerPages
        public async Task<IActionResult> Index()
        {
            return View(await _context.BannerPage.ToListAsync());
        }

        // GET: Banner/BannerPages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerPage = await _context.BannerPage
                .FirstOrDefaultAsync(m => m.BannerPageID == id);
            if (bannerPage == null)
            {
                return NotFound();
            }

            return View(bannerPage);
        }

        // GET: Banner/BannerPages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banner/BannerPages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BannerPageID,PageName")] BannerPage bannerPage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bannerPage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bannerPage);
        }

        // GET: Banner/BannerPages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerPage = await _context.BannerPage.FindAsync(id);
            if (bannerPage == null)
            {
                return NotFound();
            }
            return View(bannerPage);
        }

        // POST: Banner/BannerPages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BannerPageID,PageName")] BannerPage bannerPage)
        {
            if (id != bannerPage.BannerPageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bannerPage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerPageExists(bannerPage.BannerPageID))
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
            return View(bannerPage);
        }

        // GET: Banner/BannerPages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerPage = await _context.BannerPage
                .FirstOrDefaultAsync(m => m.BannerPageID == id);
            if (bannerPage == null)
            {
                return NotFound();
            }

            return View(bannerPage);
        }

        // POST: Banner/BannerPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bannerPage = await _context.BannerPage.FindAsync(id);
            _context.BannerPage.Remove(bannerPage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerPageExists(int id)
        {
            return _context.BannerPage.Any(e => e.BannerPageID == id);
        }
    }
}
