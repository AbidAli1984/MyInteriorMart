using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.BANNER;
using DAL.BANNER;
using Microsoft.AspNetCore.Authorization;

namespace ADMIN.Areas.Slides.Controllers
{
    [Area("Slides")]
    public class ManageController : Controller
    {
        private readonly BannerDbContext _context;

        public ManageController(BannerDbContext context)
        {
            _context = context;
        }

        // GET: Slides/Manage
        [Authorize(Policy = "Admin-Slideshow-ViewAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Slideshow.ToListAsync());
        }

        // GET: Slides/Manage/Details/5
        [Authorize(Policy = "Admin-Slideshow-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideshow = await _context.Slideshow
                .FirstOrDefaultAsync(m => m.SlideID == id);
            if (slideshow == null)
            {
                return NotFound();
            }

            return View(slideshow);
        }

        // GET: Slides/Manage/Create
        [Authorize(Policy = "Admin-Slideshow-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Slides/Manage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Slideshow-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SlideID,AltAttribute,Title,TargetURL,Priority")] Slideshow slideshow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(slideshow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(slideshow);
        }

        // GET: Slides/Manage/Edit/5
        [Authorize(Policy = "Admin-Slideshow-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideshow = await _context.Slideshow.FindAsync(id);
            if (slideshow == null)
            {
                return NotFound();
            }
            return View(slideshow);
        }

        // POST: Slides/Manage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Slideshow-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SlideID,AltAttribute,Title,TargetURL,Priority")] Slideshow slideshow)
        {
            if (id != slideshow.SlideID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(slideshow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SlideshowExists(slideshow.SlideID))
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
            return View(slideshow);
        }

        // GET: Slides/Manage/Delete/5
        [Authorize(Policy = "Admin-Slideshow-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var slideshow = await _context.Slideshow
                .FirstOrDefaultAsync(m => m.SlideID == id);
            if (slideshow == null)
            {
                return NotFound();
            }

            return View(slideshow);
        }

        // POST: Slides/Manage/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-Slideshow-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slideshow = await _context.Slideshow.FindAsync(id);
            _context.Slideshow.Remove(slideshow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SlideshowExists(int id)
        {
            return _context.Slideshow.Any(e => e.SlideID == id);
        }
    }
}
