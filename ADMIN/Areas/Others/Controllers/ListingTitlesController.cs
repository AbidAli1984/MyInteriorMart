using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.CATEGORIES;
using DAL.CATEGORIES;
using BAL.Category;

namespace ADMIN.Areas.Others.Controllers
{
    [Area("Others")]
    public class ListingTitlesController : Controller
    {
        private readonly ICategory CategoryRepo;
        private readonly CategoriesDbContext _context;

        public ListingTitlesController(CategoriesDbContext context, ICategory categoryRepo)
        {
            _context = context;
            CategoryRepo = categoryRepo;
        }

        // GET: Others/ListingTitles
        [Route("/Others/ListingTitles/Index/{secondCategoryId}")]
        public async Task<IActionResult> Index(int? secondCategoryId)
        {
            var categoriesDbContext = _context.ListingTitle.Where(i => i.SecondCategoryID == secondCategoryId).Include(l => l.FirstCategory).Include(l => l.SecondCategory);

            if (secondCategoryId != null)
            {
                var secondCategory = await CategoryRepo.SecondCategoryDetailsAsync(secondCategoryId.Value);

                ViewBag.FirstCategoryShafiId = secondCategory.FirstCategoryID;

                ViewBag.FirstCategoryId = secondCategory.FirstCategoryID;
                ViewBag.SecondCategoryId = secondCategory.SecondCategoryID;
                ViewBag.FirstCategoryName = secondCategory.FirstCategory.Name;
                ViewBag.SecondCategoryName = secondCategory.Name;
            }

            return View(await categoriesDbContext.ToListAsync());
        }

        // GET: Others/ListingTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listingTitle = await _context.ListingTitle
                .Include(l => l.FirstCategory)
                .Include(l => l.SecondCategory)
                .FirstOrDefaultAsync(m => m.TitleID == id);
            if (listingTitle == null)
            {
                return NotFound();
            }

            return View(listingTitle);
        }

        // GET: Others/ListingTitles/Create
        public IActionResult Create()
        {
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name");
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name");

            return View();
        }

        // POST: Others/ListingTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TitleID,FirstCategoryID,SecondCategoryID,Name,URL,SortOrder,Description,Title,Keyword")] ListingTitle listingTitle)
        {
            var duplicateKeyword = await _context.ListingTitle.Where(i => i.Name == listingTitle.Name).FirstOrDefaultAsync();

            if(duplicateKeyword == null)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(listingTitle);
                    await _context.SaveChangesAsync();
                    string redirectURL = $"~/Others/ListingTitles/Index/{listingTitle.SecondCategoryID}";

                    TempData["MessageSuccess"] = $"Success! Featured keyword {listingTitle.Name} created.";

                    return Redirect(redirectURL);
                }
                ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", listingTitle.FirstCategoryID);

                ViewBag.SecondCategoryShafiId = listingTitle.SecondCategoryID;

                ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", listingTitle.SecondCategoryID);

                TempData["MessageError"] = $"Oops! Something went wrong, please try again.";

                return View(listingTitle);
            }
            else
            {
                ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", listingTitle.SecondCategoryID);

                TempData["MessageError"] = "Oop! Duplicate featured keyword exists.";
                string redirectURL = $"~/Others/ListingTitles/Index/{listingTitle.SecondCategoryID}";
                return Redirect(redirectURL);
            }
        }

        // GET: Others/ListingTitles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listingTitle = await _context.ListingTitle.FindAsync(id);
            if (listingTitle == null)
            {
                return NotFound();
            }

            ViewBag.SecondCategoryShafiId = listingTitle.SecondCategoryID;

            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", listingTitle.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", listingTitle.SecondCategoryID);
            return View(listingTitle);
        }

        // POST: Others/ListingTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TitleID,FirstCategoryID,SecondCategoryID,Name,URL,SortOrder,Description,Title,Keyword")] ListingTitle listingTitle)
        {
            if (id != listingTitle.TitleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listingTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingTitleExists(listingTitle.TitleID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                ViewBag.SecondCategoryShafiId = listingTitle.SecondCategoryID;

                string redirectURL = $"~/Others/ListingTitles/Index/{listingTitle.SecondCategoryID}";
                return Redirect(redirectURL);
            }
            ViewData["FirstCategoryID"] = new SelectList(_context.FirstCategory, "FirstCategoryID", "Name", listingTitle.FirstCategoryID);
            ViewData["SecondCategoryID"] = new SelectList(_context.SecondCategory, "SecondCategoryID", "Name", listingTitle.SecondCategoryID);
            return View(listingTitle);
        }

        // GET: Others/ListingTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listingTitle = await _context.ListingTitle
                .Include(l => l.FirstCategory)
                .Include(l => l.SecondCategory)
                .FirstOrDefaultAsync(m => m.TitleID == id);
            if (listingTitle == null)
            {
                return NotFound();
            }

            ViewBag.SecondCategoryShafiId = listingTitle.SecondCategoryID;

            ViewBag.FirstCategoryId = listingTitle.FirstCategoryID;
            ViewBag.SecondCategoryId = listingTitle.SecondCategoryID;
            ViewBag.FirstCategoryName = listingTitle.FirstCategory.Name;
            ViewBag.SecondCategoryName = listingTitle.Name;

            return View(listingTitle);
        }

        // POST: Others/ListingTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listingTitle = await _context.ListingTitle.FindAsync(id);
            _context.ListingTitle.Remove(listingTitle);
            await _context.SaveChangesAsync();

            string redirectURL = $"~/Others/ListingTitles/Index/{listingTitle.SecondCategoryID}";
            return Redirect(redirectURL);
        }

        private bool ListingTitleExists(int id)
        {
            return _context.ListingTitle.Any(e => e.TitleID == id);
        }
    }
}
