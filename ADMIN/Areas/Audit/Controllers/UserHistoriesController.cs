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
    public class UserHistoriesController : Controller
    {
        private readonly AuditDbContext _context;
        private readonly IMemoryCache memoryCache;

        public UserHistoriesController(AuditDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            this.memoryCache = memoryCache;
        }

        // GET: Audit/UserHistories
        [Authorize(Policy = "Admin-UserHistory-ViewAll")]
        public async Task<IActionResult> Index()
        {
            // Shafi: Cach Cities
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            List<UserHistory> userHistory;
            if (!memoryCache.TryGetValue("UserHistory", out userHistory))
            {
                memoryCache.Set("UserHistory", await _context.UserHistory.OrderByDescending(h => h.HistoryID).ToListAsync());
            }
            userHistory = memoryCache.Get("UserHistory") as List<UserHistory>;
            stopWatch.Stop();
            ViewBag.StopWatch = stopWatch.Elapsed;
            // End:

            return View(userHistory);
        }

        // GET: Audit/UserHistories/Details/5
        [Authorize(Policy = "Admin-UserHistory-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userHistory = await _context.UserHistory
                .FirstOrDefaultAsync(m => m.HistoryID == id);
            if (userHistory == null)
            {
                return NotFound();
            }

            return View(userHistory);
        }

        // GET: Audit/UserHistories/Create
        [Authorize(Policy = "Admin-UserHistory-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Audit/UserHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-UserHistory-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoryID,UserGuid,Email,Mobile,IPAddress,UserRole,VisitDate,VisitTime,UserAgent,ReferrerURL,VisitedURL,Activity")] UserHistory userHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userHistory);
        }

        // GET: Audit/UserHistories/Edit/5
        [Authorize(Policy = "Admin-UserHistory-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userHistory = await _context.UserHistory.FindAsync(id);
            if (userHistory == null)
            {
                return NotFound();
            }
            return View(userHistory);
        }

        // POST: Audit/UserHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-UserHistory-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoryID,UserGuid,Email,Mobile,IPAddress,UserRole,VisitDate,VisitTime,UserAgent,ReferrerURL,VisitedURL,Activity")] UserHistory userHistory)
        {
            if (id != userHistory.HistoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserHistoryExists(userHistory.HistoryID))
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
            return View(userHistory);
        }

        // GET: Audit/UserHistories/Delete/5
        [Authorize(Policy = "Admin-UserHistory-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userHistory = await _context.UserHistory
                .FirstOrDefaultAsync(m => m.HistoryID == id);
            if (userHistory == null)
            {
                return NotFound();
            }

            return View(userHistory);
        }

        // POST: Audit/UserHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-UserHistory-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userHistory = await _context.UserHistory.FindAsync(id);
            _context.UserHistory.Remove(userHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserHistoryExists(int id)
        {
            return _context.UserHistory.Any(e => e.HistoryID == id);
        }
    }
}
