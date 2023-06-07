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
    public class KeywordsThirdCategoryController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public KeywordsThirdCategoryController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllKeywords/KeywordsThirdCategory/{thirdCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? thirdCatId)
        {
            // Get third categories and create ViewBags
            var thirdCat = await categoryContext.ThirdCategory.Where(i => i.ThirdCategoryID == thirdCatId).FirstOrDefaultAsync();
            ViewBag.FirstCategoryID = thirdCat.FirstCategoryID;
            ViewBag.SecondCategoryID = thirdCat.SecondCategoryID;
            ViewBag.ThirdCategoryID = thirdCat.ThirdCategoryID;
            // End:

            var result = categoryContext.KeywordThirdCategory.Where(i => i.ThirdCategoryID == thirdCatId).Include(t => t.FirstCategory).Include(t => t.SecondCategory).Include(t => t.ThirdCategory);
            return View(await result.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? FirstCategoryID, int? SecondCategoryID, int? ThirdCategoryID, string Keyword, string URL, string Title, string Description)
        {
            if (FirstCategoryID != null && SecondCategoryID != null && ThirdCategoryID != null && Keyword !=null && URL != null && Title != null && Description != null)
            {
                var keywordThirdCategory = new KeywordThirdCategory();
                keywordThirdCategory.FirstCategoryID = FirstCategoryID;
                keywordThirdCategory.SecondCategoryID = SecondCategoryID;
                keywordThirdCategory.ThirdCategoryID = ThirdCategoryID;
                keywordThirdCategory.Keyword = Keyword;
                keywordThirdCategory.URL = URL;
                keywordThirdCategory.Title = Title;
                keywordThirdCategory.Description = Description;

                categoryContext.Add(keywordThirdCategory);
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
