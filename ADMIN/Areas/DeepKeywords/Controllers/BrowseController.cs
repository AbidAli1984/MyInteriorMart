using BAL.Keyword;
using BOL.CATEGORIES;
using DAL.CATEGORIES;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADMIN.Areas.DeepKeywords.Controllers
{
    [Area("DeepKeywords")]
    [Authorize]
    public class BrowseController : Controller
    {
        private readonly IKeywords keywordsRepo;
        private readonly CategoriesDbContext categoryContext;
        public BrowseController(IKeywords keywordsRepo, CategoriesDbContext categoryContext)
        {
            this.keywordsRepo = keywordsRepo;
            this.categoryContext = categoryContext;
        }

        [HttpGet]
        [Route("DeepKeywords/Browse/First/{firstCatId}")]
        public async Task<IActionResult> First(int firstCatId)
        {
            var result = await keywordsRepo.GetKeywordFirstCategoryDeepAsync(firstCatId);
            ViewBag.CategoryName = await categoryContext.FirstCategory.Where(i => i.FirstCategoryID == firstCatId).Select(i => i.Name).FirstOrDefaultAsync();

            ViewBag.FirstCategoryName = await categoryContext.FirstCategory.Where(i => i.FirstCategoryID == firstCatId).Select(i => i.Name).FirstOrDefaultAsync();

            ViewBag.FirstCategoryId = firstCatId;
            return View(result);
        }

        // POST: Keywords/KeywordFirstCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("DeepKeywords/Browse/First/CreateKeywordFirstCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKeywordFirstCategory([Bind("KeywordFirstCategoryID,FirstCategoryID,Keyword,URL,Title,Description")] KeywordFirstCategory keywordFirstCategory)
        {
            // Shafi: Check if keyword exist in first category keyword
            if (await keywordsRepo.CheckIfKeywordFirstCategoryExist(keywordFirstCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFirstCategory.Keyword}' already exist in First Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in second category keyword
            if (await keywordsRepo.CheckIfKeywordSecondCategoryExist(keywordFirstCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFirstCategory.Keyword}' already exist in Second Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in third category keyword
            if (await keywordsRepo.CheckIfKeywordThirdCategoryExist(keywordFirstCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFirstCategory.Keyword}' already exist in Third Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fourth category keyword
            if (await keywordsRepo.CheckIfKeywordFourthCategoryExist(keywordFirstCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFirstCategory.Keyword}' already exist in Fourth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fifth category keyword
            if (await keywordsRepo.CheckIfKeywordFifthCategoryExist(keywordFirstCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFirstCategory.Keyword}' already exist in Fifth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in sixth category keyword
            if (await keywordsRepo.CheckIfKeywordSixthCategoryExist(keywordFirstCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFirstCategory.Keyword}' already exist in Sixth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
            }
            // End:

            if (ModelState.IsValid)
            {
                categoryContext.Add(keywordFirstCategory);
                await categoryContext.SaveChangesAsync();

                TempData["MessageSuccess"] = $"Keyword '{keywordFirstCategory.Keyword}' added successfully.";

                return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
            }

            TempData["MessageError"] = $"Error: All fields are required.";

            return Redirect($"/DeepKeywords/Browse/First/{keywordFirstCategory.FirstCategoryID}");
        }

        [HttpGet]
        [Route("DeepKeywords/Browse/Second/{secondCatId}")]
        public async Task<IActionResult> Second(int secondCatId)
        {
            var result = await keywordsRepo.GetKeywordSecondCategoryDeepAsync(secondCatId);
            var SecondCategory = await categoryContext.SecondCategory.Where(i => i.SecondCategoryID == secondCatId).Include(i => i.FirstCategory).FirstOrDefaultAsync();
            ViewBag.CategoryName = SecondCategory.Name;
            ViewBag.FirstCategoryName = SecondCategory.FirstCategory.Name;
            ViewBag.SecondCategoryName = SecondCategory.Name;

            ViewBag.FirstCategoryId = await categoryContext.SecondCategory.Where(i => i.SecondCategoryID == secondCatId).Select(i => i.FirstCategoryID).FirstOrDefaultAsync();
            ViewBag.SecondCategoryId = secondCatId;
            return View(result);
        }

        // POST: Keywords/KeywordFirstCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("DeepKeywords/Browse/Second/CreateKeywordSecondCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKeywordSecondCategory([Bind("KeywordFirstCategoryID,FirstCategoryID,SecondCategoryID,Keyword,URL,Title,Description")] KeywordSecondCategory keywordSecondCategory)
        {

            // Shafi: Check if keyword exist in first category keyword
            if(await keywordsRepo.CheckIfKeywordFirstCategoryExist(keywordSecondCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSecondCategory.Keyword}' already exist in First Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in second category keyword
            if (await keywordsRepo.CheckIfKeywordSecondCategoryExist(keywordSecondCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSecondCategory.Keyword}' already exist in Second Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in third category keyword
            if (await keywordsRepo.CheckIfKeywordThirdCategoryExist(keywordSecondCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSecondCategory.Keyword}' already exist in Third Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fourth category keyword
            if (await keywordsRepo.CheckIfKeywordFourthCategoryExist(keywordSecondCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSecondCategory.Keyword}' already exist in Fourth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fifth category keyword
            if (await keywordsRepo.CheckIfKeywordFifthCategoryExist(keywordSecondCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSecondCategory.Keyword}' already exist in Fifth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in sixth category keyword
            if (await keywordsRepo.CheckIfKeywordSixthCategoryExist(keywordSecondCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSecondCategory.Keyword}' already exist in Sixth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
            }
            // End:

            if (ModelState.IsValid)
            {
                categoryContext.Add(keywordSecondCategory);
                await categoryContext.SaveChangesAsync();

                TempData["MessageSuccess"] = $"Keyword '{keywordSecondCategory.Keyword}' added successfully.";

                return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
            }

            TempData["MessageError"] = $"Error: All fields are required.";

            return Redirect($"/DeepKeywords/Browse/Second/{keywordSecondCategory.SecondCategoryID}");
        }

        [HttpGet]
        [Route("DeepKeywords/Browse/Third/{thirdCatId}")]
        public async Task<IActionResult> Third(int thirdCatId)
        {
            var result = await keywordsRepo.GetKeywordThirdCategoryDeepAsync(thirdCatId);
            var ThirdCategory = await categoryContext.ThirdCategory.Where(i => i.ThirdCategoryID == thirdCatId).Include(i => i.FirstCategory).Include(i => i.SecondCategory).FirstOrDefaultAsync();
            ViewBag.CategoryName = ThirdCategory.Name;

            ViewBag.FirstCategoryName = ThirdCategory.FirstCategory.Name;
            ViewBag.SecondCategoryName = ThirdCategory.SecondCategory.Name;
            ViewBag.ThirdCategoryName = ThirdCategory.Name;

            ViewBag.FirstCategoryId = await categoryContext.ThirdCategory.Where(i => i.ThirdCategoryID == thirdCatId).Select(i => i.FirstCategoryID).FirstOrDefaultAsync();
            ViewBag.SecondCategoryId = await categoryContext.ThirdCategory.Where(i => i.ThirdCategoryID == thirdCatId).Select(i => i.SecondCategoryID).FirstOrDefaultAsync();
            ViewBag.ThirdCategoryId = thirdCatId;
            return View(result);
        }

        // POST: Keywords/KeywordFirstCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("DeepKeywords/Browse/Third/CreateKeywordThirdCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKeywordThirdCategory([Bind("KeywordFirstCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,Keyword,URL,Title,Description")] KeywordThirdCategory keywordThirdCategory)
        {
            // Shafi: Check if keyword exist in first category keyword
            if (await keywordsRepo.CheckIfKeywordFirstCategoryExist(keywordThirdCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordThirdCategory.Keyword}' already exist in First Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in second category keyword
            if (await keywordsRepo.CheckIfKeywordSecondCategoryExist(keywordThirdCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordThirdCategory.Keyword}' already exist in Second Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in third category keyword
            if (await keywordsRepo.CheckIfKeywordThirdCategoryExist(keywordThirdCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordThirdCategory.Keyword}' already exist in Third Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fourth category keyword
            if (await keywordsRepo.CheckIfKeywordFourthCategoryExist(keywordThirdCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordThirdCategory.Keyword}' already exist in Fourth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fifth category keyword
            if (await keywordsRepo.CheckIfKeywordFifthCategoryExist(keywordThirdCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordThirdCategory.Keyword}' already exist in Fifth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in sixth category keyword
            if (await keywordsRepo.CheckIfKeywordSixthCategoryExist(keywordThirdCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordThirdCategory.Keyword}' already exist in Sixth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");
            }
            // End:

            if (ModelState.IsValid)
            {
                categoryContext.Add(keywordThirdCategory);
                await categoryContext.SaveChangesAsync();

                TempData["MessageSuccess"] = $"Keyword '{keywordThirdCategory.Keyword}' added successfully.";

                return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");
            }

            TempData["MessageError"] = $"Error: All fields are required.";

            return Redirect($"/DeepKeywords/Browse/Third/{keywordThirdCategory.ThirdCategoryID}");
        }

        [HttpGet]
        [Route("DeepKeywords/Browse/Fourth/{fourthCatId}")]
        public async Task<IActionResult> Fourth(int fourthCatId)
        {
            var result = await keywordsRepo.GetKeywordFourthCategoryDeepAsync(fourthCatId);
            var FourthCategory = await categoryContext.FourthCategory.Where(i => i.FourthCategoryID == fourthCatId).Include(i => i.FirstCategory).Include(i => i.SecondCategory).Include(i => i.ThirdCategory).FirstOrDefaultAsync();
            ViewBag.CategoryName = FourthCategory.Name;
            ViewBag.FirstCategoryName = FourthCategory.FirstCategory.Name;
            ViewBag.SecondCategoryName = FourthCategory.SecondCategory.Name;
            ViewBag.ThirdCategoryName = FourthCategory.ThirdCategory.Name;
            ViewBag.FourthCategoryName = FourthCategory.Name;

            ViewBag.FirstCategoryId = await categoryContext.FourthCategory.Where(i => i.FourthCategoryID == fourthCatId).Select(i => i.FirstCategoryID).FirstOrDefaultAsync();
            ViewBag.SecondCategoryId = await categoryContext.FourthCategory.Where(i => i.FourthCategoryID == fourthCatId).Select(i => i.SecondCategoryID).FirstOrDefaultAsync();
            ViewBag.ThirdCategoryId = await categoryContext.FourthCategory.Where(i => i.FourthCategoryID == fourthCatId).Select(i => i.ThirdCategoryID).FirstOrDefaultAsync();
            ViewBag.FourthCategoryId = await categoryContext.FourthCategory.Where(i => i.FourthCategoryID == fourthCatId).Select(i => i.FourthCategoryID).FirstOrDefaultAsync();
            return View(result);
        }

        // POST: Keywords/KeywordFirstCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("DeepKeywords/Browse/Fourth/CreateKeywordFourthCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKeywordFourthCategory([Bind("KeywordFirstCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,Keyword,URL,Title,Description")] KeywordFourthCategory keywordFourthCategory)
        {
            // Shafi: Check if keyword exist in first category keyword
            if (await keywordsRepo.CheckIfKeywordFirstCategoryExist(keywordFourthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFourthCategory.Keyword}' already exist in First Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in second category keyword
            if (await keywordsRepo.CheckIfKeywordSecondCategoryExist(keywordFourthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFourthCategory.Keyword}' already exist in Second Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in third category keyword
            if (await keywordsRepo.CheckIfKeywordThirdCategoryExist(keywordFourthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFourthCategory.Keyword}' already exist in Third Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fourth category keyword
            if (await keywordsRepo.CheckIfKeywordFourthCategoryExist(keywordFourthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFourthCategory.Keyword}' already exist in Fourth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fifth category keyword
            if (await keywordsRepo.CheckIfKeywordFifthCategoryExist(keywordFourthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFourthCategory.Keyword}' already exist in Fifth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in sixth category keyword
            if (await keywordsRepo.CheckIfKeywordSixthCategoryExist(keywordFourthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFourthCategory.Keyword}' already exist in Sixth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
            }
            // End:

            if (ModelState.IsValid)
            {
                categoryContext.Add(keywordFourthCategory);
                await categoryContext.SaveChangesAsync();

                TempData["MessageSuccess"] = $"Keyword '{keywordFourthCategory.Keyword}' added successfully.";

                return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
            }

            TempData["MessageError"] = $"Error: All fields are required.";

            return Redirect($"/DeepKeywords/Browse/Fourth/{keywordFourthCategory.FourthCategoryID}");
        }

        [HttpGet]
        [Route("DeepKeywords/Browse/Fifth/{fifthCatId}")]
        public async Task<IActionResult> Fifth(int fifthCatId)
        {
            var result = await keywordsRepo.GetKeywordFifthCategoryDeepAsync(fifthCatId);
            var FifthCategory = await categoryContext.FifthCategory.Where(i => i.FifthCategoryID == fifthCatId).Include(i => i.FirstCategory).Include(i => i.SecondCategory).Include(i => i.ThirdCategory).Include(i => i.FourthCategory).FirstOrDefaultAsync();
            ViewBag.CategoryName = FifthCategory.Name;
            ViewBag.FirstCategoryName = FifthCategory.FirstCategory.Name;
            ViewBag.SecondCategoryName = FifthCategory.SecondCategory.Name;
            ViewBag.ThirdCategoryName = FifthCategory.ThirdCategory.Name;
            ViewBag.FourthCategoryName = FifthCategory.FourthCategory.Name;
            ViewBag.FifthCategoryName = FifthCategory.Name;

            ViewBag.FirstCategoryId = await categoryContext.FifthCategory.Where(i => i.FifthCategoryID == fifthCatId).Select(i => i.FirstCategoryID).FirstOrDefaultAsync();
            ViewBag.SecondCategoryId = await categoryContext.FifthCategory.Where(i => i.FifthCategoryID == fifthCatId).Select(i => i.SecondCategoryID).FirstOrDefaultAsync();
            ViewBag.ThirdCategoryId = await categoryContext.FifthCategory.Where(i => i.FifthCategoryID == fifthCatId).Select(i => i.ThirdCategoryID).FirstOrDefaultAsync();
            ViewBag.FourthCategoryId = await categoryContext.FifthCategory.Where(i => i.FifthCategoryID == fifthCatId).Select(i => i.FourthCategoryID).FirstOrDefaultAsync();
            ViewBag.FifthCategoryId = fifthCatId;
            return View(result);
        }

        // POST: Keywords/KeywordFirstCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("DeepKeywords/Browse/Fifth/CreateKeywordFifthCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKeywordFifthCategory([Bind("KeywordFirstCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,FifthCategoryID,Keyword,URL,Title,Description")] KeywordFifthCategory keywordFifthCategory)
        {
            // Shafi: Check if keyword exist in first category keyword
            if (await keywordsRepo.CheckIfKeywordFirstCategoryExist(keywordFifthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFifthCategory.Keyword}' already exist in First Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in second category keyword
            if (await keywordsRepo.CheckIfKeywordSecondCategoryExist(keywordFifthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFifthCategory.Keyword}' already exist in Second Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in third category keyword
            if (await keywordsRepo.CheckIfKeywordThirdCategoryExist(keywordFifthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFifthCategory.Keyword}' already exist in Third Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fourth category keyword
            if (await keywordsRepo.CheckIfKeywordFourthCategoryExist(keywordFifthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFifthCategory.Keyword}' already exist in Fourth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fifth category keyword
            if (await keywordsRepo.CheckIfKeywordFifthCategoryExist(keywordFifthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFifthCategory.Keyword}' already exist in Fifth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in sixth category keyword
            if (await keywordsRepo.CheckIfKeywordSixthCategoryExist(keywordFifthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordFifthCategory.Keyword}' already exist in Sixth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
            }
            // End:

            if (ModelState.IsValid)
            {
                categoryContext.Add(keywordFifthCategory);
                await categoryContext.SaveChangesAsync();

                TempData["MessageSuccess"] = $"Keyword '{keywordFifthCategory.Keyword}' added successfully.";

                return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
            }

            TempData["MessageError"] = $"Error: All fields are required.";

            return Redirect($"/DeepKeywords/Browse/Fifth/{keywordFifthCategory.FifthCategoryID}");
        }

        [HttpGet]
        [Route("DeepKeywords/Browse/Sixth/{sixthCatId}")]
        public async Task<IActionResult> Sixth(int sixthCatId)
        {
            var result = await keywordsRepo.GetKeywordSixthCategoryDeepAsync(sixthCatId);
            var SixthCategory = await categoryContext.SixthCategory.Where(i => i.SixthCategoryID == sixthCatId).Include(i => i.FirstCategory).Include(i => i.SecondCategory).Include(i => i.ThirdCategory).Include(i => i.FourthCategory).Include(i => i.FifthCategory).FirstOrDefaultAsync();
            ViewBag.CategoryName = SixthCategory.Name;
            ViewBag.FirstCategoryName = SixthCategory.FirstCategory.Name;
            ViewBag.SecondCategoryName = SixthCategory.SecondCategory.Name;
            ViewBag.ThirdCategoryName = SixthCategory.ThirdCategory.Name;
            ViewBag.FourthCategoryName = SixthCategory.FourthCategory.Name;
            ViewBag.FifthCategoryName = SixthCategory.FifthCategory.Name;
            ViewBag.SixthCategoryName = SixthCategory.Name;

            ViewBag.FirstCategoryId = await categoryContext.SixthCategory.Where(i => i.SixthCategoryID == sixthCatId).Select(i => i.FirstCategoryID).FirstOrDefaultAsync();
            ViewBag.SecondCategoryId = await categoryContext.SixthCategory.Where(i => i.SixthCategoryID == sixthCatId).Select(i => i.SecondCategoryID).FirstOrDefaultAsync();
            ViewBag.ThirdCategoryId = await categoryContext.SixthCategory.Where(i => i.SixthCategoryID == sixthCatId).Select(i => i.ThirdCategoryID).FirstOrDefaultAsync();
            ViewBag.FourthCategoryId = await categoryContext.SixthCategory.Where(i => i.SixthCategoryID == sixthCatId).Select(i => i.FourthCategoryID).FirstOrDefaultAsync();
            ViewBag.FifthCategoryId = await categoryContext.SixthCategory.Where(i => i.SixthCategoryID == sixthCatId).Select(i => i.FifthCategoryID).FirstOrDefaultAsync();
            ViewBag.SixthCategoryId = sixthCatId;
            return View(result);
        }

        // POST: Keywords/KeywordFirstCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("DeepKeywords/Browse/Sixth/CreateKeywordSixthCategory")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateKeywordSixthCategory([Bind("KeywordFirstCategoryID,FirstCategoryID,SecondCategoryID,ThirdCategoryID,FourthCategoryID,FifthCategoryID,SixthCategoryID,Keyword,URL,Title,Description")] KeywordSixthCategory keywordSixthCategory)
        {
            // Shafi: Check if keyword exist in first category keyword
            if (await keywordsRepo.CheckIfKeywordFirstCategoryExist(keywordSixthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSixthCategory.Keyword}' already exist in First Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in second category keyword
            if (await keywordsRepo.CheckIfKeywordSecondCategoryExist(keywordSixthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSixthCategory.Keyword}' already exist in Second Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in third category keyword
            if (await keywordsRepo.CheckIfKeywordThirdCategoryExist(keywordSixthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSixthCategory.Keyword}' already exist in Third Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fourth category keyword
            if (await keywordsRepo.CheckIfKeywordFourthCategoryExist(keywordSixthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSixthCategory.Keyword}' already exist in Fourth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in fifth category keyword
            if (await keywordsRepo.CheckIfKeywordFifthCategoryExist(keywordSixthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSixthCategory.Keyword}' already exist in Fifth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
            }
            // End:

            // Shafi: Check if keyword exist in sixth category keyword
            if (await keywordsRepo.CheckIfKeywordSixthCategoryExist(keywordSixthCategory.Keyword) == true)
            {
                TempData["MessageError"] = $"Duplicate Keyword Error! '{keywordSixthCategory.Keyword}' already exist in Sixth Category Keywords.";
                return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
            }
            // End:

            if (ModelState.IsValid)
            {
                categoryContext.Add(keywordSixthCategory);
                await categoryContext.SaveChangesAsync();

                TempData["MessageSuccess"] = $"Keyword '{keywordSixthCategory.Keyword}' added successfully.";

                return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
            }

            TempData["MessageError"] = $"Error: All fields are required.";

            return Redirect($"/DeepKeywords/Browse/Sixth/{keywordSixthCategory.SixthCategoryID}");
        }
    }
}
