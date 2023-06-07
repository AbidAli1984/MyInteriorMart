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
    public class KeywordsFifthCategoryController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public KeywordsFifthCategoryController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllKeywords/KeywordsFifthCategory/{fifthCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? fifthCatId)
        {
            // Get fourth categories and create ViewBags
            var fifthCat = await categoryContext.FifthCategory.Where(i => i.FifthCategoryID == fifthCatId).FirstOrDefaultAsync();
            ViewBag.FirstCategoryID = fifthCat.FirstCategoryID;
            ViewBag.SecondCategoryID = fifthCat.SecondCategoryID;
            ViewBag.ThirdCategoryID = fifthCat.ThirdCategoryID;
            ViewBag.FourthCategoryID = fifthCat.FourthCategoryID;
            ViewBag.FifthCategoryID = fifthCat.FifthCategoryID;
            // End:

            var result = await categoryContext.KeywordFifthCategory.Where(i => i.FifthCategoryID == fifthCatId).OrderBy(i => i.Keyword).Include(t => t.FirstCategory).Include(t => t.SecondCategory).Include(t => t.ThirdCategory).Include(t => t.FourthCategory).Include(t => t.FifthCategory).ToListAsync();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? FirstCategoryID, int? SecondCategoryID, int? ThirdCategoryID, int? FourthCategoryID, int? FifthCategoryID, string Keyword, string URL, string Title, string Description)
        {
            if (FirstCategoryID != null && SecondCategoryID != null && ThirdCategoryID != null && FourthCategoryID != null && FifthCategoryID != null && Keyword != null && URL != null && Title != null && Description != null)
            {
                var keywordFifthCategory = new KeywordFifthCategory();
                keywordFifthCategory.FirstCategoryID = FirstCategoryID;
                keywordFifthCategory.SecondCategoryID = SecondCategoryID;
                keywordFifthCategory.ThirdCategoryID = ThirdCategoryID;
                keywordFifthCategory.FourthCategoryID = FourthCategoryID;
                keywordFifthCategory.FifthCategoryID = FifthCategoryID;
                keywordFifthCategory.Keyword = Keyword;
                keywordFifthCategory.URL = URL;
                keywordFifthCategory.Title = Title;
                keywordFifthCategory.Description = Description;

                categoryContext.Add(keywordFifthCategory);
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
