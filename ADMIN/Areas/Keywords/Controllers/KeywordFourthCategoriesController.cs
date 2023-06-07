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
    public class KeywordFourthCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;

        public KeywordFourthCategoriesController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: Keywords/KeywordFourthCategories
        [Authorize(Policy = "Admin-FourthCategoryKeyword-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.KeywordFourthCategory.Include(k => k.FirstCategory).Include(k => k.FourthCategory).Include(k => k.SecondCategory).Include(k => k.ThirdCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Keywords/KeywordFourthCategories/Details/5
        [Authorize(Policy = "")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordFourthCategory = await _context.KeywordFourthCategory
                .Include(k => k.FirstCategory)
                .Include(k => k.FourthCategory)
                .Include(k => k.SecondCategory)
                .Include(k => k.ThirdCategory)
                .FirstOrDefaultAsync(m => m.KeywordFourthCategoryID == id);
            if (keywordFourthCategory == null)
            {
                return NotFound();
            }

            return View(keywordFourthCategory);
        }

        // GET: Keywords/KeywordFourthCategories/Create
        [Authorize(Policy = "Admin-FourthCategoryKeyword-Read")]
        public IActionResult Create()
        {
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name");
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name");
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name");
            return View();
        }

        // POST: Keywords/KeywordFourthCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FourthCategoryKeyword-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeywordFourthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,Keyword,URL,Title,Description")] KeywordFourthCategory keywordFourthCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordFourthCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordFourthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name", keywordFourthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordFourthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordFourthCategory.ThirdCategoryID);
            return View(keywordFourthCategory);
        }

        // GET: Keywords/KeywordFourthCategories/Edit/5
        [Authorize(Policy = "Admin-FourthCategoryKeyword-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordFourthCategory = await _context.KeywordFourthCategory.FindAsync(id);
            if (keywordFourthCategory == null)
            {
                return NotFound();
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordFourthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name", keywordFourthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordFourthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordFourthCategory.ThirdCategoryID);
            return View(keywordFourthCategory);
        }

        // POST: Keywords/KeywordFourthCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FourthCategoryKeyword-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeywordFourthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,Keyword,URL,Title,Description")] KeywordFourthCategory keywordFourthCategory)
        {
            if (id != keywordFourthCategory.KeywordFourthCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordFourthCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordFourthCategoryExists(keywordFourthCategory.KeywordFourthCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordFourthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name", keywordFourthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordFourthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordFourthCategory.ThirdCategoryID);
            return View(keywordFourthCategory);
        }

        // GET: Keywords/KeywordFourthCategories/Delete/5
        [Authorize(Policy = "Admin-FourthCategoryKeyword-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordFourthCategory = await _context.KeywordFourthCategory
                .Include(k => k.FirstCategory)
                .Include(k => k.FourthCategory)
                .Include(k => k.SecondCategory)
                .Include(k => k.ThirdCategory)
                .FirstOrDefaultAsync(m => m.KeywordFourthCategoryID == id);
            if (keywordFourthCategory == null)
            {
                return NotFound();
            }

            return View(keywordFourthCategory);
        }

        // POST: Keywords/KeywordFourthCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-FourthCategoryKeyword-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordFourthCategory = await _context.KeywordFourthCategory.FindAsync(id);
            _context.KeywordFourthCategory.Remove(keywordFourthCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool KeywordFourthCategoryExists(int id)
        {
            return _context.KeywordFourthCategory.Any(e => e.KeywordFourthCategoryID == id);
        }
    }
}
