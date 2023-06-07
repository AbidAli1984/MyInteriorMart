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
    public class KeywordsSecondCategoryController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public KeywordsSecondCategoryController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllKeywords/KeywordsSecondCategory/{secondCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? secondCatId)
        {
            // Shafi: Get first category
            var secondCategory = await categoryContext.SecondCategory.Where(c => c.SecondCategoryID == secondCatId).FirstOrDefaultAsync();
            ViewBag.FirstCategoryID = secondCategory.FirstCategoryID;
            ViewBag.SecondCategoryID = secondCatId;
            // End:

            var result = await categoryContext.KeywordSecondCategory.Where(i => i.SecondCategoryID == secondCatId).Include(s => s.FirstCategory).Include(s => s.SecondCategory).ToListAsync();
            return View(result);
        }

        public async Task<JsonResult> Create(int? FirstCategoryID, int? SecondCategoryID, string Keyword, string URL, string Title, string Description)
        {
            if (FirstCategoryID != null && SecondCategoryID != null && Keyword != null && URL != null && Title != null && Description != null)
            {
                var keywordSecondCategory = new KeywordSecondCategory();
                keywordSecondCategory.FirstCategoryID = FirstCategoryID;
                keywordSecondCategory.SecondCategoryID = SecondCategoryID;
                keywordSecondCategory.Keyword = Keyword;
                keywordSecondCategory.URL = URL;
                keywordSecondCategory.Title = Title;
                keywordSecondCategory.Description = Description;

                categoryContext.Add(keywordSecondCategory);
                await categoryContext.SaveChangesAsync();
                return Json("success");
            }
            else
            {
                return Json("error");
            }
        }
    }
}
