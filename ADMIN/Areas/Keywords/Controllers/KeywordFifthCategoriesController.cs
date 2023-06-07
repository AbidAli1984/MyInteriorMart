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
    public class KeywordFifthCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;

        public KeywordFifthCategoriesController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: Keywords/KeywordFifthCategories
        [Authorize(Policy = "Admin-FifthCategoryKeyword-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.KeywordFifthCategory.Include(k => k.FifthCategory).Include(k => k.FirstCategory).Include(k => k.FourthCategory).Include(k => k.SecondCategory).Include(k => k.ThirdCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Keywords/KeywordFifthCategories/Details/5
        [Authorize(Policy = "Admin-FifthCategoryKeyword-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordFifthCategory = await _context.KeywordFifthCategory
                .Include(k => k.FifthCategory)
                .Include(k => k.FirstCategory)
                .Include(k => k.FourthCategory)
                .Include(k => k.SecondCategory)
                .Include(k => k.ThirdCategory)
                .FirstOrDefaultAsync(m => m.KeywordFifthCategoryID == id);
            if (keywordFifthCategory == null)
            {
                return NotFound();
            }

            return View(keywordFifthCategory);
        }

        // GET: Keywords/KeywordFifthCategories/Create
        [Authorize(Policy = "Admin-FifthCategoryKeyword-Create")]
        public IActionResult Create()
        {
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory, "FifthCategoryID", "Name");
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name");
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name");
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name");
            return View();
        }

        // POST: Keywords/KeywordFifthCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FifthCategoryKeyword-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KeywordFifthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,FifthCategoryID,Keyword,URL,Title,Description")] KeywordFifthCategory keywordFifthCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordFifthCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory, "FifthCategoryID", "Name", keywordFifthCategory.FifthCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordFifthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name", keywordFifthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordFifthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordFifthCategory.ThirdCategoryID);
            return View(keywordFifthCategory);
        }

        // GET: Keywords/KeywordFifthCategories/Edit/5
        [Authorize(Policy = "Admin-FifthCategoryKeyword-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordFifthCategory = await _context.KeywordFifthCategory.FindAsync(id);
            if (keywordFifthCategory == null)
            {
                return NotFound();
            }
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory, "FifthCategoryID", "Name", keywordFifthCategory.FifthCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordFifthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name", keywordFifthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordFifthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordFifthCategory.ThirdCategoryID);
            return View(keywordFifthCategory);
        }

        // POST: Keywords/KeywordFifthCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FifthCategoryKeyword-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KeywordFifthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,FifthCategoryID,Keyword,URL,Title,Description")] KeywordFifthCategory keywordFifthCategory)
        {
            if (id != keywordFifthCategory.KeywordFifthCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordFifthCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordFifthCategoryExists(keywordFifthCategory.KeywordFifthCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
                //return RedirectToAction(nameof(Index));
            }
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory, "FifthCategoryID", "Name", keywordFifthCategory.FifthCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", keywordFifthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory, "FourthCategoryID", "Name", keywordFifthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", keywordFifthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory, "ThirdCategoryID", "Name", keywordFifthCategory.ThirdCategoryID);
            return View(keywordFifthCategory);
        }

        // GET: Keywords/KeywordFifthCategories/Delete/5
        [Authorize(Policy = "Admin-FifthCategoryKeyword-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordFifthCategory = await _context.KeywordFifthCategory
                .Include(k => k.FifthCategory)
                .Include(k => k.FirstCategory)
                .Include(k => k.FourthCategory)
                .Include(k => k.SecondCategory)
                .Include(k => k.ThirdCategory)
                .FirstOrDefaultAsync(m => m.KeywordFifthCategoryID == id);
            if (keywordFifthCategory == null)
            {
                return NotFound();
            }

            return View(keywordFifthCategory);
        }

        // POST: Keywords/KeywordFifthCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-FifthCategoryKeyword-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordFifthCategory = await _context.KeywordFifthCategory.FindAsync(id);
            _context.KeywordFifthCategory.Remove(keywordFifthCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool KeywordFifthCategoryExists(int id)
        {
            return _context.KeywordFifthCategory.Any(e => e.KeywordFifthCategoryID == id);
        }
    }
}
