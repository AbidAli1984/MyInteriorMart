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
    public class KeywordsSixthCategoryController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public KeywordsSixthCategoryController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllKeywords/KeywordsSixthCategory/{sixthCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? sixthCatId)
        {
            // Get fourth categories and create ViewBags
            var sixthCat = await categoryContext.SixthCategory.Where(i => i.SixthCategoryID == sixthCatId).FirstOrDefaultAsync();
            ViewBag.FirstCategoryID = sixthCat.FirstCategoryID;
            ViewBag.SecondCategoryID = sixthCat.SecondCategoryID;
            ViewBag.ThirdCategoryID = sixthCat.ThirdCategoryID;
            ViewBag.FourthCategoryID = sixthCat.FourthCategoryID;
            ViewBag.FifthCategoryID = sixthCat.FifthCategoryID;
            ViewBag.SixthCategoryID = sixthCat.SixthCategoryID;
            // End:

            var result = await categoryContext.KeywordSixthCategory.Where(i => i.SixthCategoryID == sixthCatId).Include(t => t.FirstCategory).Include(t => t.SecondCategory).Include(t => t.ThirdCategory).Include(t => t.FourthCategory).Include(t => t.FifthCategory).Include(t => t.SixthCategory).ToListAsync();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? FirstCategoryID, int? SecondCategoryID, int? ThirdCategoryID, int? FourthCategoryID, int? FifthCategoryID, int? SixthCategoryID, string Keyword, string URL, string Title, string Description)
        {
            if (FirstCategoryID != null && SecondCategoryID != null && ThirdCategoryID != null && FourthCategoryID != null && FifthCategoryID != null && SixthCategoryID != null && Keyword != null && URL != null && Title != null && Description != null)
            {
                var keywordSixthCategory = new KeywordSixthCategory();
                keywordSixthCategory.FirstCategoryID = FirstCategoryID;
                keywordSixthCategory.SecondCategoryID = SecondCategoryID;
                keywordSixthCategory.ThirdCategoryID = ThirdCategoryID;
                keywordSixthCategory.FourthCategoryID = FourthCategoryID;
                keywordSixthCategory.FifthCategoryID = FifthCategoryID;
                keywordSixthCategory.SixthCategoryID = SixthCategoryID;
                keywordSixthCategory.Keyword = Keyword;
                keywordSixthCategory.URL = URL;
                keywordSixthCategory.Title = Title;
                keywordSixthCategory.Description = Description;

                categoryContext.Add(keywordSixthCategory);
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
