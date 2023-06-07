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
    public class FifthCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;
        private readonly ICategory CategoryRepository;

        public FifthCategoriesController(CategoriesDbContext context, ICategory categoryRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
        }

        // GET: Categories/FifthCategories
        [Authorize(Policy = "Admin-FifthCategory-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var result = await _context.FifthCategory.OrderByDescending(f => f.SortOrder).Include(f => f.FirstCategory).Include(f => f.SecondCategory).Include(f => f.ThirdCategory).Include(f => f.FourthCategory).ToListAsync();

            return View(result);
        }

        // GET: Categories/FifthCategories/Details/5
        [Authorize(Policy = "Admin-FifthCategory-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fifthCategory = await _context.FifthCategory
                .Include(f => f.FirstCategory)
                .Include(f => f.FourthCategory)
                .Include(f => f.SecondCategory)
                .Include(f => f.ThirdCategory)
                .FirstOrDefaultAsync(m => m.FifthCategoryID == id);
            if (fifthCategory == null)
            {
                return NotFound();
            }

            return View(fifthCategory);
        }

        // GET: Categories/FifthCategories/Create
        [Authorize(Policy = "Admin-FifthCategory-Create")]
        public IActionResult Create()
        {
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(c => c.Name), "FirstCategoryID", "Name");
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory.OrderBy(c => c.Name), "FourthCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(c => c.Name), "SecondCategoryID", "Name");
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(c => c.Name), "ThirdCategoryID", "Name");
            return View();
        }

        // POST: Categories/FifthCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FifthCategory-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FifthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] FifthCategory fifthCategory)
        {
            if (ModelState.IsValid)
            {
                if (await CategoryRepository.FifthCategoryDuplicate(fifthCategory.SearchKeywordName) != true)
                {
                    _context.Add(fifthCategory);
                    await _context.SaveChangesAsync();
                    return Redirect($"/DeepCategories/Browse/Fifth/{fifthCategory.FourthCategoryID}");
                }
                else
                {
                    TempData["Message"] = "Duplicate 'Business Category Name!'";
                    return Redirect($"/DeepCategories/Browse/Fifth/{fifthCategory.FourthCategoryID}");
                }
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(c => c.Name), "FirstCategoryID", "Name", fifthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory.OrderBy(c => c.Name), "FourthCategoryID", "Name", fifthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(c => c.Name), "SecondCategoryID", "Name", fifthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(c => c.Name), "ThirdCategoryID", "Name", fifthCategory.ThirdCategoryID);

            TempData["Message"] = "Oop! Something went wrong! Check if there is more than 70 characters or any other problem in name.";
            return Redirect($"/DeepCategories/Browse/Fifth/{fifthCategory.FourthCategoryID}");
        }

        // GET: Categories/FifthCategories/Edit/5
        [Authorize(Policy = "Admin-FifthCategory-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fifthCategory = await _context.FifthCategory.FindAsync(id);
            if (fifthCategory == null)
            {
                return NotFound();
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(c => c.Name), "FirstCategoryID", "Name", fifthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory.OrderBy(c => c.Name), "FourthCategoryID", "Name", fifthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(c => c.Name), "SecondCategoryID", "Name", fifthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(c => c.Name), "ThirdCategoryID", "Name", fifthCategory.ThirdCategoryID);
            return View(fifthCategory);
        }

        // POST: Categories/FifthCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FifthCategory-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FifthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] FifthCategory fifthCategory)
        {
            if (id != fifthCategory.FifthCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fifthCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FifthCategoryExists(fifthCategory.FifthCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Redirect($"/DeepCategories/Browse/Fifth/{fifthCategory.FourthCategoryID}");

                //return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(c => c.Name), "FirstCategoryID", "Name", fifthCategory.FirstCategoryID);
            ViewData["FourthCategoryID"] = new SelectList(_context.FourthCategory.OrderBy(c => c.Name), "FourthCategoryID", "Name", fifthCategory.FourthCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(c => c.Name), "SecondCategoryID", "Name", fifthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(c => c.Name), "ThirdCategoryID", "Name", fifthCategory.ThirdCategoryID);

            TempData["Message"] = "Oop! Something went wrong! Check if there is more than 70 characters or any other problem in name.";
            return Redirect($"/DeepCategories/Browse/Fifth/{fifthCategory.FourthCategoryID}");
        }

        // GET: Categories/FifthCategories/Delete/5
        [Authorize(Policy = "Admin-FifthCategory-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fifthCategory = await _context.FifthCategory
                .Include(f => f.FirstCategory)
                .Include(f => f.FourthCategory)
                .Include(f => f.SecondCategory)
                .Include(f => f.ThirdCategory)
                .FirstOrDefaultAsync(m => m.FifthCategoryID == id);
            if (fifthCategory == null)
            {
                return NotFound();
            }

            return View(fifthCategory);
        }

        // POST: Categories/FifthCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-FifthCategory-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fifthCategory = await _context.FifthCategory.FindAsync(id);
            _context.FifthCategory.Remove(fifthCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepCategories/Browse/Fifth/{fifthCategory.FourthCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool FifthCategoryExists(int id)
        {
            return _context.FifthCategory.Any(e => e.FifthCategoryID == id);
        }
    }
}
