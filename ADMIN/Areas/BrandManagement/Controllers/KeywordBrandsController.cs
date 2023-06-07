using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.BRANDS;
using DAL.CATEGORIES;
using Microsoft.AspNetCore.Authorization;

namespace ADMIN.Areas.BrandManagement.Controllers
{
    [Area("BrandManagement")]
    [Authorize]
    public class KeywordBrandsController : Controller
    {
        private readonly CategoriesDbContext _context;

        public KeywordBrandsController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: BrandManagement/KeywordBrands
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.KeywordBrand.Include(k => k.BrandCategory).Include(k => k.FirstCategory).Include(k => k.SecondCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: BrandManagement/KeywordBrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordBrand = await _context.KeywordBrand
                .Include(k => k.BrandCategory)
                .Include(k => k.FirstCategory)
                .Include(k => k.SecondCategory)
                .FirstOrDefaultAsync(m => m.BrandKeywordID == id);
            if (keywordBrand == null)
            {
                return NotFound();
            }

            return View(keywordBrand);
        }

        // GET: BrandManagement/KeywordBrands/Create
        public IActionResult Create()
        {
            ViewData["BrandCategoryID"] = new SelectList(_context.BrandsCategory.Include(i => i.Brand), "BrandCategoryID", "BrandCategoryName");
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name");
            return View();
        }

        // POST: BrandManagement/KeywordBrands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandKeywordID,FirstCategoryID,SecondCategoryID,BrandCategoryID,Keyword")] KeywordBrand keywordBrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keywordBrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandCategoryID"] = new SelectList(_context.BrandsCategory, "BrandCategoryID", "BrandCategoryName", keywordBrand.BrandCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Description", keywordBrand.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Description", keywordBrand.SecondCategoryID);
            return View(keywordBrand);
        }

        // GET: BrandManagement/KeywordBrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordBrand = await _context.KeywordBrand.FindAsync(id);
            if (keywordBrand == null)
            {
                return NotFound();
            }
            ViewData["BrandCategoryID"] = new SelectList(_context.BrandsCategory, "BrandCategoryID", "BrandCategoryName", keywordBrand.BrandCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Description", keywordBrand.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Description", keywordBrand.SecondCategoryID);
            return View(keywordBrand);
        }

        // POST: BrandManagement/KeywordBrands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandKeywordID,FirstCategoryID,SecondCategoryID,BrandCategoryID,Keyword")] KeywordBrand keywordBrand)
        {
            if (id != keywordBrand.BrandKeywordID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keywordBrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeywordBrandExists(keywordBrand.BrandKeywordID))
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
            ViewData["BrandCategoryID"] = new SelectList(_context.BrandsCategory, "BrandCategoryID", "BrandCategoryName", keywordBrand.BrandCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Description", keywordBrand.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Description", keywordBrand.SecondCategoryID);
            return View(keywordBrand);
        }

        // GET: BrandManagement/KeywordBrands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var keywordBrand = await _context.KeywordBrand
                .Include(k => k.BrandCategory)
                .Include(k => k.FirstCategory)
                .Include(k => k.SecondCategory)
                .FirstOrDefaultAsync(m => m.BrandKeywordID == id);
            if (keywordBrand == null)
            {
                return NotFound();
            }

            return View(keywordBrand);
        }

        // POST: BrandManagement/KeywordBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keywordBrand = await _context.KeywordBrand.FindAsync(id);
            _context.KeywordBrand.Remove(keywordBrand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeywordBrandExists(int id)
        {
            return _context.KeywordBrand.Any(e => e.BrandKeywordID == id);
        }
    }
}
