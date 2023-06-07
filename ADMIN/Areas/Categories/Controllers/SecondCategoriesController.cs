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
    public class SecondCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;
        private readonly ICategory CategoryRepository;

        public SecondCategoriesController(CategoriesDbContext context, ICategory categoryRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
        }

        // GET: Categories/SecondCategories
        [Authorize(Policy = "Admin-SecondCategory-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.SecondCategory.OrderByDescending(i => i.SortOrder).Include(s => s.FirstCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Categories/SecondCategories/Details/5
        [Authorize(Policy = "Admin-SecondCategory-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondCategory = await _context.SecondCategory
                .Include(s => s.FirstCategory)
                .FirstOrDefaultAsync(m => m.SecondCategoryID == id);
            if (secondCategory == null)
            {
                return NotFound();
            }

            return View(secondCategory);
        }

        // GET: Categories/SecondCategories/Create
        [Authorize(Policy = "Admin-SecondCategory-Create")]
        public IActionResult Create()
        {
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.SortOrder), "FirstCategoryID", "Name");
            return View();
        }

        // POST: Categories/SecondCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-SecondCategory-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SecondCategoryID,FirstCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] SecondCategory secondCategory)
        {
            if (ModelState.IsValid)
            {
                if (await CategoryRepository.SecondCategoryDuplicate(secondCategory.SearchKeywordName) != true)
                {
                    _context.Add(secondCategory);
                    await _context.SaveChangesAsync();
                    return Redirect($"/DeepCategories/Browse/Second/{secondCategory.FirstCategoryID}");
                }
                else
                {
                    TempData["Message"] = "Duplicate 'Business Category Name!'";
                    return Redirect($"/DeepCategories/Browse/Second/{secondCategory.FirstCategoryID}");
                }
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.SortOrder), "FirstCategoryID", "Description", secondCategory.FirstCategoryID);

            TempData["Message"] = "Oop! Something went wrong! Check if there is more than 70 characters or any other problem in name.";
            return Redirect($"/DeepCategories/Browse/Second/{secondCategory.FirstCategoryID}");
        }

        // GET: Categories/SecondCategories/Edit/5
        [Authorize(Policy = "Admin-SecondCategory-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondCategory = await _context.SecondCategory.FindAsync(id);
            if (secondCategory == null)
            {
                return NotFound();
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.SortOrder), "FirstCategoryID", "Name", secondCategory.FirstCategoryID);
            return View(secondCategory);
        }

        // POST: Categories/SecondCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-SecondCategory-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SecondCategoryID,FirstCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] SecondCategory secondCategory)
        {
            if (id != secondCategory.SecondCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secondCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecondCategoryExists(secondCategory.SecondCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return Redirect($"/DeepCategories/Browse/Second/{secondCategory.FirstCategoryID}");
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.SortOrder), "FirstCategoryID", "Name", secondCategory.FirstCategoryID);

            TempData["Message"] = "Oop! Something went wrong! Check if there is more than 70 characters or any other problem in name.";
            return Redirect($"/DeepCategories/Browse/Second/{secondCategory.FirstCategoryID}");
        }

        // GET: Categories/SecondCategories/Delete/5
        [Authorize(Policy = "Admin-SecondCategory-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondCategory = await _context.SecondCategory
                .Include(s => s.FirstCategory)
                .FirstOrDefaultAsync(m => m.SecondCategoryID == id);
            if (secondCategory == null)
            {
                return NotFound();
            }

            return View(secondCategory);
        }

        // POST: Categories/SecondCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-SecondCategory-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secondCategory = await _context.SecondCategory.FindAsync(id);
            _context.SecondCategory.Remove(secondCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepCategories/Browse/Second/{secondCategory.FirstCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool SecondCategoryExists(int id)
        {
            return _context.SecondCategory.Any(e => e.SecondCategoryID == id);
        }
    }
}
