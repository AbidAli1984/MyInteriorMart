using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.BRANDS;
using DAL.CATEGORIES;

namespace ADMIN.Areas.BrandManagement.Controllers
{
    [Area("BrandManagement")]
    public class BrandCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;

        public BrandCategoriesController(CategoriesDbContext context)
        {
            _context = context;
        }

        // GET: BrandManagement/BrandCategories
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.BrandsCategory.Include(b => b.Brand).Include(b => b.FirstCategory).Include(b => b.SecondCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: BrandManagement/BrandCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandCategory = await _context.BrandsCategory
                .Include(b => b.Brand)
                .Include(b => b.FirstCategory)
                .Include(b => b.SecondCategory)
                .FirstOrDefaultAsync(m => m.BrandCategoryID == id);
            if (brandCategory == null)
            {
                return NotFound();
            }

            return View(brandCategory);
        }

        // GET: BrandManagement/BrandCategories/Create
        public IActionResult Create()
        {
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "Name");
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name");
            return View();
        }

        // POST: BrandManagement/BrandCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BrandCategoryID,BrandID,FirstCategoryID,SecondCategoryID,BrandCategoryName")] BrandCategory brandCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brandCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "Name", brandCategory.BrandID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", brandCategory.FirstCategoryID);
            ViewData["Name"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Description", brandCategory.SecondCategoryID);
            return View(brandCategory);
        }

        // GET: BrandManagement/BrandCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandCategory = await _context.BrandsCategory.FindAsync(id);
            if (brandCategory == null)
            {
                return NotFound();
            }
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID", brandCategory.BrandID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", brandCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", brandCategory.SecondCategoryID);
            return View(brandCategory);
        }

        // POST: BrandManagement/BrandCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BrandCategoryID,BrandID,FirstCategoryID,SecondCategoryID,BrandCategoryName")] BrandCategory brandCategory)
        {
            if (id != brandCategory.BrandCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brandCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandCategoryExists(brandCategory.BrandCategoryID))
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
            ViewData["BrandID"] = new SelectList(_context.Brand, "BrandID", "BrandID", brandCategory.BrandID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Description", brandCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Description", brandCategory.SecondCategoryID);
            return View(brandCategory);
        }

        // GET: BrandManagement/BrandCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brandCategory = await _context.BrandsCategory
                .Include(b => b.Brand)
                .Include(b => b.FirstCategory)
                .Include(b => b.SecondCategory)
                .FirstOrDefaultAsync(m => m.BrandCategoryID == id);
            if (brandCategory == null)
            {
                return NotFound();
            }

            return View(brandCategory);
        }

        // POST: BrandManagement/BrandCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brandCategory = await _context.BrandsCategory.FindAsync(id);
            _context.BrandsCategory.Remove(brandCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandCategoryExists(int id)
        {
            return _context.BrandsCategory.Any(e => e.BrandCategoryID == id);
        }
    }
}
