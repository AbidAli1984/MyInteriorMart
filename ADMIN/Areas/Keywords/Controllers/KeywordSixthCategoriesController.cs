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
    public class KeywordSixthCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;

        public KeywordSixthCategoriesController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: Keywords/KeywordSixthCategories
        [Authorize(Policy = "Admin-SixthCategoryKeyword-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.KeywordSixthCategory.Include(k => k.FifthCategory).Include(k => k.FirstCategory).Include(k => k.FourthCategory).Include(k => k.SecondCategory).Include(k => k.SixthCategory).Include(k => k.ThirdCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Keywords/KeywordSixthCategories/Details/5
        [Authorize(Policy = "Admin-SixthCategoryKeyword-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordSixthCategory = await _context.KeywordSixthCategory
                .Include(k => k.FifthCategory)
                .Include(k => k.FirstCategory)
                .Include(k => k.FourthCategory)
                .Include(k => k.SecondCategory)
                .Include(k => k.SixthCategory)
                .Include(k => k.ThirdCategory)
                .FirstOrDefaultAsync(m => m.KeywordSixthCategoryID == id);
            if (keywordSixthCategory == null)
            {
                return NotFound();
            }

            return View(keywordSixthCategory);
        }

        // GET: Keywords/KeywordSixthCategories/Create
        [Authorize(Policy = "Admin-SixthCategoryKeyword-Create")]
        public IActionResult Create()
        {
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory, "FifthCategoryID", "Name");
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name");
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name");
            ViewData["SixthCategoryID"] = new SelectList(_context.SixthCategory, "SixthCategoryID", "Name");
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name");
            return View();
        }

        // POST: Keywords/KeywordSixthCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-SixthCategoryKeyword-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeywordSixthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,FifthCategoryID,SixthCategoryID,Keyword,URL,Title,Description")] KeywordSixthCategory keywordSixthCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordSixthCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory, "FifthCategoryID", "Name", keywordSixthCategory.FifthCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordSixthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name", keywordSixthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordSixthCategory.SecondCategoryID);
            ViewData["SixthCategoryID"] = new SelectList(_context.SixthCategory, "SixthCategoryID", "Name", keywordSixthCategory.SixthCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordSixthCategory.ThirdCategoryID);
            return View(keywordSixthCategory);
        }

        // GET: Keywords/KeywordSixthCategories/Edit/5
        [Authorize(Policy = "Admin-SixthCategoryKeyword-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordSixthCategory = await _context.KeywordSixthCategory.FindAsync(id);
            if (keywordSixthCategory == null)
            {
                return NotFound();
            }
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory, "FifthCategoryID", "Name", keywordSixthCategory.FifthCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordSixthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name", keywordSixthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordSixthCategory.SecondCategoryID);
            ViewData["SixthCategoryID"] = new SelectList(_context.SixthCategory, "SixthCategoryID", "Name", keywordSixthCategory.SixthCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordSixthCategory.ThirdCategoryID);
            return View(keywordSixthCategory);
        }

        // POST: Keywords/KeywordSixthCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-SixthCategoryKeyword-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeywordSixthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,FifthCategoryID,SixthCategoryID,Keyword,URL,Title,Description")] KeywordSixthCategory keywordSixthCategory)
        {
            if (id != keywordSixthCategory.KeywordSixthCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordSixthCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordSixthCategoryExists(keywordSixthCategory.KeywordSixthCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory, "FifthCategoryID", "Name", keywordSixthCategory.FifthCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordSixthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name", keywordSixthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordSixthCategory.SecondCategoryID);
            ViewData["SixthCategoryID"] = new SelectList(_context.SixthCategory, "SixthCategoryID", "Name", keywordSixthCategory.SixthCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordSixthCategory.ThirdCategoryID);
            return View(keywordSixthCategory);
        }

        // GET: Keywords/KeywordSixthCategories/Delete/5
        [Authorize(Policy = "Admin-SixthCategoryKeyword-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordSixthCategory = await _context.KeywordSixthCategory
                .Include(k => k.FifthCategory)
                .Include(k => k.FirstCategory)
                .Include(k => k.FourthCategory)
                .Include(k => k.SecondCategory)
                .Include(k => k.SixthCategory)
                .Include(k => k.ThirdCategory)
                .FirstOrDefaultAsync(m => m.KeywordSixthCategoryID == id);
            if (keywordSixthCategory == null)
            {
                return NotFound();
            }

            return View(keywordSixthCategory);
        }

        // POST: Keywords/KeywordSixthCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-SixthCategoryKeyword-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordSixthCategory = await _context.KeywordSixthCategory.FindAsync(id);
            _context.KeywordSixthCategory.Remove(keywordSixthCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool KeywordSixthCategoryExists(int id)
        {
            return _context.KeywordSixthCategory.Any(e => e.KeywordSixthCategoryID == id);
        }
    }
}
