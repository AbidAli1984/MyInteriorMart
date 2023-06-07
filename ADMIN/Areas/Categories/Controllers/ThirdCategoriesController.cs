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
    public class ThirdCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;
        private readonly ICategory CategoryRepository;

        public ThirdCategoriesController(CategoriesDbContext context, ICategory categoryRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
        }

        // GET: Categories/ThirdCategories
        [Authorize(Policy = "Admin-ThirdCategory-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.ThirdCategory.OrderByDescending(i => i.SortOrder).Include(t => t.FirstCategory).Include(t => t.SecondCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Categories/ThirdCategories/Details/5
        [Authorize(Policy = "Admin-ThirdCategory-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thirdCategory = await _context.ThirdCategory
                .Include(t => t.FirstCategory)
                .Include(t => t.SecondCategory)
                .FirstOrDefaultAsync(m => m.ThirdCategoryID == id);
            if (thirdCategory == null)
            {
                return NotFound();
            }

            return View(thirdCategory);
        }

        // GET: Categories/ThirdCategories/Create
        [Authorize(Policy = "Admin-ThirdCategory-Create")]
        public IActionResult Create()
        {
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name");
            return View();
        }

        // POST: Categories/ThirdCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-ThirdCategory-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ThirdCategoryID,FirstCategoryID,SecondCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] ThirdCategory thirdCategory)
        {
            if (ModelState.IsValid)
            {
                if (await CategoryRepository.ThirdCategoryDuplicate(thirdCategory.SearchKeywordName) != true)
                {
                    _context.Add(thirdCategory);
                    await _context.SaveChangesAsync();
                    return Redirect($"/DeepCategories/Browse/Third/{thirdCategory.SecondCategoryID}");
                }
                else
                {
                    TempData["Message"] = "Duplicate 'Business Category Name!'";
                    return Redirect($"/DeepCategories/Browse/Third/{thirdCategory.SecondCategoryID}");
                }
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name", thirdCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name", thirdCategory.SecondCategoryID);

            TempData["Message"] = "Oop! Something went wrong! Check if there is more than 70 characters or any other problem in name.";
            return Redirect($"/DeepCategories/Browse/Third/{thirdCategory.SecondCategoryID}");
        }

        // GET: Categories/ThirdCategories/Edit/5
        [Authorize(Policy = "Admin-ThirdCategory-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thirdCategory = await _context.ThirdCategory.FindAsync(id);
            if (thirdCategory == null)
            {
                return NotFound();
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name", thirdCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name", thirdCategory.SecondCategoryID);
            return View(thirdCategory);
        }

        // POST: Categories/ThirdCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-ThirdCategory-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ThirdCategoryID,FirstCategoryID,SecondCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] ThirdCategory thirdCategory)
        {
            if (id != thirdCategory.ThirdCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thirdCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThirdCategoryExists(thirdCategory.ThirdCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Redirect($"/DeepCategories/Browse/Third/{thirdCategory.SecondCategoryID}");

                //return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name", thirdCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name", thirdCategory.SecondCategoryID);

            TempData["Message"] = "Oop! Something went wrong! Check if there is more than 70 characters or any other problem in name.";
            return Redirect($"/DeepCategories/Browse/Third/{thirdCategory.SecondCategoryID}");
        }

        // GET: Categories/ThirdCategories/Delete/5
        [Authorize(Policy = "Admin-ThirdCategory-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thirdCategory = await _context.ThirdCategory
                .Include(t => t.FirstCategory)
                .Include(t => t.SecondCategory)
                .FirstOrDefaultAsync(m => m.ThirdCategoryID == id);
            if (thirdCategory == null)
            {
                return NotFound();
            }

            return View(thirdCategory);
        }

        // POST: Categories/ThirdCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-ThirdCategory-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thirdCategory = await _context.ThirdCategory.FindAsync(id);
            _context.ThirdCategory.Remove(thirdCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepCategories/Browse/Third/{thirdCategory.SecondCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool ThirdCategoryExists(int id)
        {
            return _context.ThirdCategory.Any(e => e.ThirdCategoryID == id);
        }
    }
}
