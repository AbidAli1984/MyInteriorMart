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
    public class KeywordThirdCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;

        public KeywordThirdCategoriesController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: Keywords/KeywordThirdCategories
        [Authorize(Policy = "Admin-ThirdCategoryKeyword-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.KeywordThirdCategory.Include(k => k.FirstCategory).Include(k => k.SecondCategory).Include(k => k.ThirdCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Keywords/KeywordThirdCategories/Details/5
        [Authorize(Policy = "Admin-ThirdCategoryKeyword-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordThirdCategory = await _context.KeywordThirdCategory
                .Include(k => k.FirstCategory)
                .Include(k => k.SecondCategory)
                .Include(k => k.ThirdCategory)
                .FirstOrDefaultAsync(m => m.KeywordThirdCategoryID == id);
            if (keywordThirdCategory == null)
            {
                return NotFound();
            }

            return View(keywordThirdCategory);
        }

        // GET: Keywords/KeywordThirdCategories/Create
        [Authorize(Policy = "Admin-ThirdCategoryKeyword-Create")]
        public IActionResult Create()
        {
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name");
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name");
            return View();
        }

        // POST: Keywords/KeywordThirdCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-ThirdCategoryKeyword-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeywordThirdCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,Keyword,URL,Title,Description")] KeywordThirdCategory keywordThirdCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordThirdCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordThirdCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordThirdCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordThirdCategory.ThirdCategoryID);
            return View(keywordThirdCategory);
        }

        // GET: Keywords/KeywordThirdCategories/Edit/5
        [Authorize(Policy = "Admin-ThirdCategoryKeyword-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordThirdCategory = await _context.KeywordThirdCategory.FindAsync(id);
            if (keywordThirdCategory == null)
            {
                return NotFound();
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordThirdCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordThirdCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordThirdCategory.ThirdCategoryID);
            return View(keywordThirdCategory);
        }

        // POST: Keywords/KeywordThirdCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-ThirdCategoryKeyword-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeywordThirdCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,Keyword,URL,Title,Description")] KeywordThirdCategory keywordThirdCategory)
        {
            if (id != keywordThirdCategory.KeywordThirdCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordThirdCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordThirdCategoryExists(keywordThirdCategory.KeywordThirdCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordThirdCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordThirdCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordThirdCategory.ThirdCategoryID);
            return View(keywordThirdCategory);
        }

        // GET: Keywords/KeywordThirdCategories/Delete/5
        [Authorize(Policy = "Admin-ThirdCategoryKeyword-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordThirdCategory = await _context.KeywordThirdCategory
                .Include(k => k.FirstCategory)
                .Include(k => k.SecondCategory)
                .Include(k => k.ThirdCategory)
                .FirstOrDefaultAsync(m => m.KeywordThirdCategoryID == id);
            if (keywordThirdCategory == null)
            {
                return NotFound();
            }

            return View(keywordThirdCategory);
        }

        // POST: Keywords/KeywordThirdCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-ThirdCategoryKeyword-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordThirdCategory = await _context.KeywordThirdCategory.FindAsync(id);
            _context.KeywordThirdCategory.Remove(keywordThirdCategory);
            await _context.SaveChangesAsync();

            return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");

            //return RedirectToAction(nameof(Index));
        }

        private bool KeywordThirdCategoryExists(int id)
        {
            return _context.KeywordThirdCategory.Any(e => e.KeywordThirdCategoryID == id);
        }
    }
}
