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

namespace ADMIN.Areas.AllKeywords.Controllers
{
    [Area("AllKeywords")]
    [Authorize]
    public class KeywordsFirstCategoryController : Controller
    {
        private readonly CategoriesDbContext categoriesContext;

        public KeywordsFirstCategoryController(CategoriesDbContext categoriesContext)
        {
            this.categoriesContext = categoriesContext;
        }

        [Route("/AllKeywords/KeywordsFirstCategory/{firstCatID}")]
        [HttpGet]
        public async Task<IActionResult> Index(int firstCatID)
        {
            ViewBag.FirstCategoryID = firstCatID;

            return View(await categoriesContext.KeywordFirstCategory.Where(i => i.FirstCategoryID == firstCatID).Include(i => i.FirstCategory).OrderByDescending(i => i.KeywordFirstCategoryID).ToListAsync());
        }

        // POST: Keywords/KeywordFirstCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<JsonResult> Create(int? FirstCategoryID, string Keyword, string URL, string Title, string Description)
        {
            if (FirstCategoryID != null && Keyword != null && URL != null && Title != null && Description != null)
            {
                var keywordFirstCategory = new KeywordFirstCategory();
                keywordFirstCategory.FirstCategoryID = FirstCategoryID;
                keywordFirstCategory.Keyword = Keyword;
                keywordFirstCategory.URL = URL;
                keywordFirstCategory.Title = Title;
                keywordFirstCategory.Description = Description;

                await categoriesContext.AddAsync(keywordFirstCategory);
                await categoriesContext.SaveChangesAsync();

                return Json("success");
            }
            else
            {
                return Json("error");
            }

            
        }
    }
}
