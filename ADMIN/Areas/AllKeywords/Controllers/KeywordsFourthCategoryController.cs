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
    public class KeywordsFourthCategoryController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public KeywordsFourthCategoryController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllKeywords/KeywordsFourthCategory/{fourthCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? fourthCatId)
        {
            // Get fourth categories and create ViewBags
            var fourthCat = await categoryContext.FourthCategory.Where(i => i.FourthCategoryID == fourthCatId).FirstOrDefaultAsync();
            ViewBag.FirstCategoryID = fourthCat.FirstCategoryID;
            ViewBag.SecondCategoryID = fourthCat.SecondCategoryID;
            ViewBag.ThirdCategoryID = fourthCat.ThirdCategoryID;
            ViewBag.FourthCategoryID = fourthCat.FourthCategoryID;
            // End:

            var result = await categoryContext.KeywordFourthCategory.Where(i => i.FourthCategoryID == fourthCatId).Include(t => t.FirstCategory).Include(t => t.SecondCategory).Include(t => t.ThirdCategory).Include(t => t.FourthCategory).ToListAsync();
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? FirstCategoryID, int? SecondCategoryID, int? ThirdCategoryID, int? FourthCategoryID, string Keyword, string URL, string Title, string Description)
        {
            if (FirstCategoryID != null && SecondCategoryID != null && ThirdCategoryID != null && FourthCategoryID != null && Keyword != null && URL != null && Title != null && Description != null)
            {
                var keywordFourthCategory = new KeywordFourthCategory();
                keywordFourthCategory.FirstCategoryID = FirstCategoryID;
                keywordFourthCategory.SecondCategoryID = SecondCategoryID;
                keywordFourthCategory.ThirdCategoryID = ThirdCategoryID;
                keywordFourthCategory.FourthCategoryID = FourthCategoryID;
                keywordFourthCategory.Keyword = Keyword;
                keywordFourthCategory.URL = URL;
                keywordFourthCategory.Title = Title;
                keywordFourthCategory.Description = Description;

                categoryContext.Add(keywordFourthCategory);
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