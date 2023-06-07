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
    public class SixthCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;
        private readonly ICategory CategoryRepository;

        public SixthCategoriesController(CategoriesDbContext context, ICategory categoryRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
        }

        // GET: Categories/SixthCategories
        [Authorize(Policy = "Admin-SixthCategory-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.SixthCategory.Include(s => s.FifthCategory).Include(s => s.FirstCategory).Include(s => s.FourthCategory).Include(s => s.SecondCategory).Include(s => s.ThirdCategory).OrderByDescending(s => s.SortOrder);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Categories/SixthCategories/Details/5
        [Authorize(Policy = "Admin-SixthCategory-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sixthCategory = await _context.SixthCategory
                .Include(s => s.FifthCategory)
                .Include(s => s.FirstCategory)
                .Include(s => s.FourthCategory)
                .Include(s => s.SecondCategory)
                .Include(s => s.ThirdCategory)
                .FirstOrDefaultAsync(m => m.SixthCategoryID == id);
            if (sixthCategory == null)
            {
                return NotFound();
            }

            return View(sixthCategory);
        }

        // GET: Categories/SixthCategories/Create
        [Authorize(Policy = "Admin-SixthCategory-Create")]
        public IActionResult Create()
        {
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory.OrderBy(i => i.Name), "FifthCategoryID", "Name");
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name");
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory.OrderBy(i => i.Name), "FourthCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name");
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(i => i.Name), "ThirdCategoryID", "Name");
            return View();
        }

        // POST: Categories/SixthCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-SixthCategory-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SixthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,FifthCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] SixthCategory sixthCategory)
        {
            if (ModelState.IsValid)
            {
                if (await CategoryRepository.SixthCategoryDuplicate(sixthCategory.SearchKeywordName) != true)
                {
                    _context.Add(sixthCategory);
                    await _context.SaveChangesAsync();
                    return Redirect($"/DeepCategories/Browse/Sixth/{sixthCategory.FifthCategoryID}");
                }
                else
                {
                    TempData["Message"] = "Duplicate 'Business Category Name!'";
                    return Redirect($"/DeepCategories/Browse/Sixth/{sixthCategory.FifthCategoryID}");
                }
            }
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory.OrderBy(i => i.Name), "FifthCategoryID", "Name", sixthCategory.FifthCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name", sixthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory.OrderBy(i => i.Name), "FourthCategoryID", "Name", sixthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name", sixthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(i => i.Name), "ThirdCategoryID", "Name", sixthCategory.ThirdCategoryID);

            TempData["Message"] = "Oop! Something went wrong! Check if there is more than 70 characters or any other problem in name.";
            return Redirect($"/DeepCategories/Browse/Sixth/{sixthCategory.FifthCategoryID}");
        }

        // GET: Categories/SixthCategories/Edit/5
        [Authorize(Policy = "Admin-SixthCategory-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sixthCategory = await _context.SixthCategory.FindAsync(id);
            if (sixthCategory == null)
            {
                return NotFound();
            }
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory.OrderBy(i => i.Name), "FifthCategoryID", "Name", sixthCategory.FifthCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name", sixthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory.OrderBy(i => i.Name), "FourthCategoryID", "Name", sixthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name", sixthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(i => i.Name), "ThirdCategoryID", "Name", sixthCategory.ThirdCategoryID);
            return View(sixthCategory);
        }

        // POST: Categories/SixthCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-SixthCategory-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SixthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,FifthCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] SixthCategory sixthCategory)
        {
            if (id != sixthCategory.SixthCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sixthCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SixthCategoryExists(sixthCategory.SixthCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Redirect($"/DeepCategories/Browse/Sixth/{sixthCategory.FifthCategoryID}");

                //return RedirectToAction(nameof(Index));
            }
            ViewData["FifthCategoryID"] = new SelectList(_context.FifthCategory.OrderBy(i => i.Name), "FifthCategoryID", "Name", sixthCategory.FifthCategoryID);
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name", sixthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory.OrderBy(i => i.Name), "FourthCategoryID", "Name", sixthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name", sixthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(i => i.Name), "ThirdCategoryID", "Name", sixthCategory.ThirdCategoryID);
            return View(sixthCategory);
        }

        // GET: Categories/SixthCategories/Delete/5
        [Authorize(Policy = "Admin-SixthCategory-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sixthCategory = await _context.SixthCategory
                .Include(s => s.FifthCategory)
                .Include(s => s.FirstCategory)
                .Include(s => s.FourthCategory)
                .Include(s => s.SecondCategory)
                .Include(s => s.ThirdCategory)
                .FirstOrDefaultAsync(m => m.SixthCategoryID == id);
            if (sixthCategory == null)
            {
                return NotFound();
            }

            return View(sixthCategory);
        }

        // POST: Categories/SixthCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-SixthCategory-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sixthCategory = await _context.SixthCategory.FindAsync(id);
            _context.SixthCategory.Remove(sixthCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepCategories/Browse/Sixth/{sixthCategory.FifthCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool SixthCategoryExists(int id)
        {
            return _context.SixthCategory.Any(e => e.SixthCategoryID == id);
        }
    }
}
