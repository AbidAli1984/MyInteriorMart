using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.AUDITTRAIL;
using DAL.AUDIT;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;

namespace ADMIN.Areas.Audit.Controllers
{
    [Area("Audit")]
    public class ListingLastUpdatedsController : Controller
    {
        private readonly AuditDbContext _context;
        private readonly IMemoryCache memoryCache;

        public ListingLastUpdatedsController(AuditDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            this.memoryCache = memoryCache;
        }

        // GET: Audit/ListingLastUpdateds
        [Authorize(Policy = "Admin-ListingHistory-ViewAll")]
        public async Task<IActionResult> Index()
        {

            // Shafi: Cach Cities
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<ListingLastUpdated> listingLastUpdated;
            if (!memoryCache.TryGetValue("ListingLastUpdated", out listingLastUpdated))
            {
                memoryCache.Set("ListingLastUpdated", await _context.ListingLastUpdated.OrderByDescending(h => h.LastUpdatedID).ToListAsync());
            }
            listingLastUpdated = memoryCache.Get("ListingLastUpdated") as List<ListingLastUpdated>;
            stopWatch.Stop();
            ViewBag.StopWatch = stopWatch.Elapsed;
            // End:

            return View(listingLastUpdated);
        }

        // GET: Audit/ListingLastUpdateds/Details/5
        [Authorize(Policy = "Admin-ListingHistory-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listingLastUpdated = await _context.ListingLastUpdated
                .FirstOrDefaultAsync(m => m.LastUpdatedID == id);
            if (listingLastUpdated == null)
            {
                return NotFound();
            }

            return View(listingLastUpdated);
        }

        // GET: Audit/ListingLastUpdateds/Create
        [Authorize(Policy = "Admin-ListingHistory-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Audit/ListingLastUpdateds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-ListingHistory-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastUpdatedID,ListingID,UpdatedByGuid,Email,Mobile,IPAddress,UserRole,SectionUpdated,UpdatedDate,UpdatedTime,UpdatedURL,UserAgent,Activity")] ListingLastUpdated listingLastUpdated)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listingLastUpdated);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listingLastUpdated);
        }

        // GET: Audit/ListingLastUpdateds/Edit/5
        [Authorize(Policy = "Admin-ListingHistory-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listingLastUpdated = await _context.ListingLastUpdated.FindAsync(id);
            if (listingLastUpdated == null)
            {
                return NotFound();
            }
            return View(listingLastUpdated);
        }

        // POST: Audit/ListingLastUpdateds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-ListingHistory-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LastUpdatedID,ListingID,UpdatedByGuid,Email,Mobile,IPAddress,UserRole,SectionUpdated,UpdatedDate,UpdatedTime,UpdatedURL,UserAgent,Activity")] ListingLastUpdated listingLastUpdated)
        {
            if (id != listingLastUpdated.LastUpdatedID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listingLastUpdated);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingLastUpdatedExists(listingLastUpdated.LastUpdatedID))
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
            return View(listingLastUpdated);
        }

        // GET: Audit/ListingLastUpdateds/Delete/5
        [Authorize(Policy = "Admin-ListingHistory-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listingLastUpdated = await _context.ListingLastUpdated
                .FirstOrDefaultAsync(m => m.LastUpdatedID == id);
            if (listingLastUpdated == null)
            {
                return NotFound();
            }

            return View(listingLastUpdated);
        }

        // POST: Audit/ListingLastUpdateds/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-ListingHistory-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listingLastUpdated = await _context.ListingLastUpdated.FindAsync(id);
            _context.ListingLastUpdated.Remove(listingLastUpdated);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListingLastUpdatedExists(int id)
        {
            return _context.ListingLastUpdated.Any(e => e.LastUpdatedID == id);
        }
    }
}
