using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.AUDITTRAIL;
using DAL.AUDIT;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;

namespace ADMIN.Areas.Audit.Controllers
{
    [Area("Audit")]
    [Authorize]
    public class SuggestionsController : Controller
    {
        private readonly AuditDbContext _context;

        public SuggestionsController(AuditDbContext context)
        {
            _context = context;
        }

        // GET: Audit/Suggestions
        [Authorize(Policy = "Admin-Suggestion-ViewAll")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _context.Suggestions.AsNoTracking().OrderByDescending(x => x.SuggestionID);
            var model = await PagingList.CreateAsync(query, 5, page);
            return View(model);
        }

        // GET: Audit/Suggestions/Details/5
        [Authorize(Policy = "Admin-Suggestion-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestions = await _context.Suggestions
                .FirstOrDefaultAsync(m => m.SuggestionID == id);
            if (suggestions == null)
            {
                return NotFound();
            }

            return View(suggestions);
        }

        // GET: Audit/Suggestions/Edit/5
        [Authorize(Policy = "Admin-Suggestion-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestions = await _context.Suggestions.FindAsync(id);
            if (suggestions == null)
            {
                return NotFound();
            }
            return View(suggestions);
        }

        // POST: Audit/Suggestions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Suggestion-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SuggestionID,Date,Name,Email,Mobile,Suggestion")] Suggestions suggestions)
        {
            if (id != suggestions.SuggestionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suggestions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuggestionsExists(suggestions.SuggestionID))
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
            return View(suggestions);
        }

        // GET: Audit/Suggestions/Delete/5
        [Authorize(Policy = "Admin-Suggestion-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suggestions = await _context.Suggestions
                .FirstOrDefaultAsync(m => m.SuggestionID == id);
            if (suggestions == null)
            {
                return NotFound();
            }

            return View(suggestions);
        }

        // POST: Audit/Suggestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-Suggestion-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var suggestions = await _context.Suggestions.FindAsync(id);
            _context.Suggestions.Remove(suggestions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet, ActionName("DeleteList")]
        [Authorize(Policy = "Admin-Suggestion-DeleteList")]
        public async Task<IActionResult> DeleteList(string itemList)
        {
            int[] idArray = Array.ConvertAll(itemList.Split(','), int.Parse);

            foreach(var id in idArray)
            {
                var suggestions = await _context.Suggestions.FindAsync(id);
                _context.Suggestions.Remove(suggestions);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuggestionsExists(int id)
        {
            return _context.Suggestions.Any(e => e.SuggestionID == id);
        }
    }
}
