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
    public class FourthCategoriesController : Controller
    {
        private readonly CategoriesDbContext _context;
        private readonly ICategory CategoryRepository;

        public FourthCategoriesController(CategoriesDbContext context, ICategory categoryRepository)
        {
            _context = context;
            CategoryRepository = categoryRepository;
        }

        // GET: Categories/FourthCategories
        [Authorize(Policy = "Admin-FourthCategory-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var categoriesDbContext = _context.FourthCategory.OrderByDescending(f => f.SortOrder).Include(f => f.FirstCategory).Include(f => f.SecondCategory).Include(f => f.ThirdCategory);
            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Categories/FourthCategories/Details/5
        [Authorize(Policy = "Admin-FourthCategory-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fourthCategory = await _context.FourthCategory
                .Include(f => f.FirstCategory)
                .Include(f => f.SecondCategory)
                .Include(f => f.ThirdCategory)
                .FirstOrDefaultAsync(m => m.FourthCategoryID == id);
            if (fourthCategory == null)
            {
                return NotFound();
            }

            return View(fourthCategory);
        }

        // GET: Categories/FourthCategories/Create
        [Authorize(Policy = "Admin-FourthCategory-Create")]
        public IActionResult Create()
        {
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name");
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(i => i.Name), "ThirdCategoryID", "Name");
            return View();
        }

        // POST: Categories/FourthCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FourthCategory-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FourthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] FourthCategory fourthCategory)
        {
            if (ModelState.IsValid)
            {
                if (await CategoryRepository.FourthCategoryDuplicate(fourthCategory.SearchKeywordName) != true)
                {
                    _context.Add(fourthCategory);
                    await _context.SaveChangesAsync();
                    return Redirect($"/DeepCategories/Browse/Fourth/{fourthCategory.ThirdCategoryID}");
                }
                else
                {
                    TempData["Message"] = "Duplicate 'Business Category Name!'";
                    return Redirect($"/DeepCategories/Browse/Fourth/{fourthCategory.ThirdCategoryID}");
                }
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name", fourthCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name", fourthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(i => i.Name), "ThirdCategoryID", "Name", fourthCategory.ThirdCategoryID);

            TempData["Message"] = "Oop! Something went wrong! Check if there is more than 70 characters or any other problem in name.";
            return Redirect($"/DeepCategories/Browse/Fourth/{fourthCategory.ThirdCategoryID}");
        }

        // GET: Categories/FourthCategories/Edit/5
        [Authorize(Policy = "Admin-FourthCategory-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fourthCategory = await _context.FourthCategory.FindAsync(id);
            if (fourthCategory == null)
            {
                return NotFound();
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name", fourthCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name", fourthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(i => i.Name), "ThirdCategoryID", "Name", fourthCategory.ThirdCategoryID);
            return View(fourthCategory);
        }

        // POST: Categories/FourthCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-FourthCategory-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FourthCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,Name,SearchKeywordName,URL,SortOrder,Description,Keyword")] FourthCategory fourthCategory)
        {
            if (id != fourthCategory.FourthCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fourthCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FourthCategoryExists(fourthCategory.FourthCategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Redirect($"/DeepCategories/Browse/Fourth/{fourthCategory.ThirdCategoryID}");

                //return RedirectToAction(nameof(Index));
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory.OrderBy(i => i.Name), "FirstCategoryID", "Name", fourthCategory.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory.OrderBy(i => i.Name), "SecondCategoryID", "Name", fourthCategory.SecondCategoryID);
            ViewData["ThirdCategoryID"] = new SelectList(_context.ThirdCategory.OrderBy(i => i.Name), "ThirdCategoryID", "Name", fourthCategory.ThirdCategoryID);


            TempData["Message"] = "Oop! Something went wrong! Check if there is more than 70 characters or any other problem in name.";
            return Redirect($"/DeepCategories/Browse/Fourth/{fourthCategory.ThirdCategoryID}");
        }

        // GET: Categories/FourthCategories/Delete/5
        [Authorize(Policy = "Admin-FourthCategory-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fourthCategory = await _context.FourthCategory
                .Include(f => f.FirstCategory)
                .Include(f => f.SecondCategory)
                .Include(f => f.ThirdCategory)
                .FirstOrDefaultAsync(m => m.FourthCategoryID == id);
            if (fourthCategory == null)
            {
                return NotFound();
            }

            return View(fourthCategory);
        }

        // POST: Categories/FourthCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-FourthCategory-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fourthCategory = await _context.FourthCategory.FindAsync(id);
            _context.FourthCategory.Remove(fourthCategory);
            await _context.SaveChangesAsync();
            return Redirect($"/DeepCategories/Browse/Fourth/{fourthCategory.ThirdCategoryID}");
            //return RedirectToAction(nameof(Index));
        }

        private bool FourthCategoryExists(int id)
        {
            return _context.FourthCategory.Any(e => e.FourthCategoryID == id);
        }
    }
}
