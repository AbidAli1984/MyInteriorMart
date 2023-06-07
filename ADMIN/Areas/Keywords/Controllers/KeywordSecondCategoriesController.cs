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
    public class KeywordSecondCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;

        public KeywordSecondCategoriesController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: Keywords/KeywordSecondCategories
        [Authorize(Policy = "Admin-SecondCategoryKeyword-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.KeywordSecondCategory.Include(k => k.FirstCategory).Include(k => k.SecondCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Keywords/KeywordSecondCategories/Details/5
        [Authorize(Policy = "Admin-SecondCategoryKeyword-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordSecondCategory = await _context.KeywordSecondCategory
                .Include(k => k.FirstCategory)
                .Include(k => k.SecondCategory)
                .FirstOrDefaultAsync(m => m.KeywordSecondCategoryID == id);
            if (keywordSecondCategory == null)
            {
                return NotFound();
            }

            return View(keywordSecondCategory);
        }

        // GET: Keywords/KeywordSecondCategories/Create
        [Authorize(Policy = "Admin-SecondCategoryKeyword-Create")]
        public IActionResult Create()
        {
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name");
            return View();
        }

        // POST: Keywords/KeywordSecondCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-SecondCategoryKeyword-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeywordSecondCategoryID,FirstCategoryID,SecondCategoryID,Keyword,URL,Title,Description")] KeywordSecondCategory keywordSecondCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordSecondCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordSecondCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordSecondCategory.SecondCategoryID);
            return View(keywordSecondCategory);
        }

        // GET: Keywords/KeywordSecondCategories/Edit/5
        [Authorize(Policy = "Admin-SecondCategoryKeyword-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordSecondCategory = await _context.KeywordSecondCategory.FindAsync(id);
            if (keywordSecondCategory == null)
            {
                return NotFound();
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordSecondCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordSecondCategory.SecondCategoryID);
            return View(keywordSecondCategory);
        }

        // POST: Keywords/KeywordSecondCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-SecondCategoryKeyword-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeywordSecondCategoryID,FirstCategoryID,SecondCategoryID,Keyword,URL,Title,Description")] KeywordSecondCategory keywordSecondCategory)
        {
            if (id != keywordSecondCategory.KeywordSecondCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordSecondCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordSecondCategoryExists(keywordSecondCategory.KeywordSecondCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordSecondCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordSecondCategory.SecondCategoryID);
            return View(keywordSecondCategory);
        }

        // GET: Keywords/KeywordSecondCategories/Delete/5
        [Authorize(Policy = "Admin-SecondCategoryKeyword-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordSecondCategory = await _context.KeywordSecondCategory
                .Include(k => k.FirstCategory)
                .Include(k => k.SecondCategory)
                .FirstOrDefaultAsync(m => m.KeywordSecondCategoryID == id);
            if (keywordSecondCategory == null)
            {
                return NotFound();
            }

            return View(keywordSecondCategory);
        }

        // POST: Keywords/KeywordSecondCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-SecondCategoryKeyword-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordSecondCategory = await _context.KeywordSecondCategory.FindAsync(id);
            _context.KeywordSecondCategory.Remove(keywordSecondCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool KeywordSecondCategoryExists(int id)
        {
            return _context.KeywordSecondCategory.Any(e => e.KeywordSecondCategoryID == id);
        }
    }
}
