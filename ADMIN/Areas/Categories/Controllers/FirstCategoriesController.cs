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
using BAL.Category;

namespace ADMIN.Areas.Categories.Controllers
{
    [Area("Categories")]
    public class FirstCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;
        private readonly ICategory CategoryRepository;

        public FirstCategoriesController(CategoriesDbContext context, ICategory categoryRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
        }

        // GET: Categories/FirstCategories
        [Authorize(Policy = "Admin-FirstCategory-ViewAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.FirstCategory.OrderByDescending(i => i.SortOrder).ToListAsync());
        }

        // GET: Categories/FirstCategories/Details/5
        [Authorize(Policy = "Admin-FirstCategory-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firstCategory = await _context.FirstCategory
                .FirstOrDefaultAsync(m => m.FirstCategoryID == id);
            if (firstCategory == null)
            {
                return NotFound();
            }

            return View(firstCategory);
        }

        // GET: Categories/FirstCategories/Create
        [Authorize(Policy = "Admin-FirstCategory-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/FirstCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FirstCategory-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] FirstCategory firstCategory)
        {
            if (ModelState.IsValid)
            {

                if(await CategoryRepository.FirstCategoryDuplicate(firstCategory.SearchKeywordName) != true)
                {
                    _context.Add(firstCategory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Message"] = "Duplicate 'Business Category Name!'";
                    return RedirectToAction(nameof(Index));
                }
            }

            TempData["Message"] = "'Duplicate Business Category Name!'";
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/FirstCategories/Edit/5
        [Authorize(Policy = "Admin-FirstCategory-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firstCategory = await _context.FirstCategory.FindAsync(id);
            if (firstCategory == null)
            {
                return NotFound();
            }
            return View(firstCategory);
        }

        // POST: Categories/FirstCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FirstCategory-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] FirstCategory firstCategory)
        {
            if (id != firstCategory.FirstCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(firstCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FirstCategoryExists(firstCategory.FirstCategoryID))
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
            return View(firstCategory);
        }

        // GET: Categories/FirstCategories/Delete/5
        [Authorize(Policy = "Admin-FirstCategory-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firstCategory = await _context.FirstCategory
                .FirstOrDefaultAsync(m => m.FirstCategoryID == id);
            if (firstCategory == null)
            {
                return NotFound();
            }

            return View(firstCategory);
        }

        // POST: Categories/FirstCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-FirstCategory-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var firstCategory = await _context.FirstCategory.FindAsync(id);
            _context.FirstCategory.Remove(firstCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FirstCategoryExists(int id)
        {
            return _context.FirstCategory.Any(e => e.FirstCategoryID == id);
        }
    }
}
