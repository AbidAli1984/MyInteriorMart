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

namespace ADMIN.Areas.Keywords.Controllers
{
    [Area("Keywords")]
    public class KeywordFirstCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;

        public KeywordFirstCategoriesController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: Keywords/KeywordFirstCategories
        [Authorize(Policy = "Admin-FirstCategoryKeyword-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.KeywordFirstCategory.Include(k => k.FirstCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Keywords/KeywordFirstCategories/Details/5
        [Authorize(Policy = "Admin-FirstCategoryKeyword-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordFirstCategory = await _context.KeywordFirstCategory
                .Include(k => k.FirstCategory)
                .FirstOrDefaultAsync(m => m.KeywordFirstCategoryID == id);
            if (keywordFirstCategory == null)
            {
                return NotFound();
            }

            return View(keywordFirstCategory);
        }

        // GET: Keywords/KeywordFirstCategories/Create
        [Authorize(Policy = "Admin-FirstCategoryKeyword-Create")]
        public IActionResult Create()
        {
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name");
            return View();
        }

        // POST: Keywords/KeywordFirstCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FirstCategoryKeyword-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeywordFirstCategoryID,FirstCategoryID,Keyword,URL,Title,Description")] KeywordFirstCategory keywordFirstCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordFirstCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordFirstCategory.FirstCategoryID);
            return View(keywordFirstCategory);
        }

        // GET: Keywords/KeywordFirstCategories/Edit/5
        [Authorize(Policy = "Admin-FirstCategoryKeyword-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordFirstCategory = await _context.KeywordFirstCategory.FindAsync(id);
            if (keywordFirstCategory == null)
            {
                return NotFound();
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordFirstCategory.FirstCategoryID);
            return View(keywordFirstCategory);
        }

        // POST: Keywords/KeywordFirstCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FirstCategoryKeyword-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeywordFirstCategoryID,FirstCategoryID,Keyword,URL,Title,Description")] KeywordFirstCategory keywordFirstCategory)
        {
            if (id != keywordFirstCategory.KeywordFirstCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordFirstCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordFirstCategoryExists(keywordFirstCategory.KeywordFirstCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordFirstCategory.FirstCategoryID);
            return View(keywordFirstCategory);
        }

        // GET: Keywords/KeywordFirstCategories/Delete/5
        [Authorize(Policy = "Admin-FirstCategoryKeyword-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordFirstCategory = await _context.KeywordFirstCategory
                .Include(k => k.FirstCategory)
                .FirstOrDefaultAsync(m => m.KeywordFirstCategoryID == id);
            if (keywordFirstCategory == null)
            {
                return NotFound();
            }

            return View(keywordFirstCategory);
        }

        // POST: Keywords/KeywordFirstCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-FirstCategoryKeyword-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordFirstCategory = await _context.KeywordFirstCategory.FindAsync(id);
            _context.KeywordFirstCategory.Remove(keywordFirstCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool KeywordFirstCategoryExists(int id)
        {
            return _context.KeywordFirstCategory.Any(e => e.KeywordFirstCategoryID == id);
        }
    }
}
