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
    public class BannerTypesController : Controller
    {
        private readonly BannerDbContext _context;

        public BannerTypesController(BannerDbContext context)
        {
            _context = context;
        }

        // GET: Banner/BannerTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BannerType.ToListAsync());
        }

        // GET: Banner/BannerTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerType = await _context.BannerType
                .FirstOrDefaultAsync(m => m.BannerTypeID == id);
            if (bannerType == null)
            {
                return NotFound();
            }

            return View(bannerType);
        }

        // GET: Banner/BannerTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banner/BannerTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BannerTypeID,Type")] BannerType bannerType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bannerType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bannerType);
        }

        // GET: Banner/BannerTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerType = await _context.BannerType.FindAsync(id);
            if (bannerType == null)
            {
                return NotFound();
            }
            return View(bannerType);
        }

        // POST: Banner/BannerTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BannerTypeID,Type")] BannerType bannerType)
        {
            if (id != bannerType.BannerTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bannerType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerTypeExists(bannerType.BannerTypeID))
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
            return View(bannerType);
        }

        // GET: Banner/BannerTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bannerType = await _context.BannerType
                .FirstOrDefaultAsync(m => m.BannerTypeID == id);
            if (bannerType == null)
            {
                return NotFound();
            }

            return View(bannerType);
        }

        // POST: Banner/BannerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bannerType = await _context.BannerType.FindAsync(id);
            _context.BannerType.Remove(bannerType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerTypeExists(int id)
        {
            return _context.BannerType.Any(e => e.BannerTypeID == id);
        }
    }
}
