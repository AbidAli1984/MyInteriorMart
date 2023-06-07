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

namespace ADMIN.Areas.DeepCategories.Controllers
{
    [Area("DeepCategories")]
    [Authorize]
    public class BrowseController : Controller
    {
        private readonly ICategory categoryRepo;
        public BrowseController(ICategory categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [HttpGet]
        [Route("DeepCategories/Browse/Second/{firstCategoryId}")]
        public async Task<IActionResult> Second(int firstCategoryId)
        {
            var firstCategory = await categoryRepo.FirstCategoryDetailsAsync(firstCategoryId);
            ViewBag.FirstCategoryId = firstCategoryId;
            ViewBag.FirstCategoryName = firstCategory.Name;
            var result = await categoryRepo.GetSecondCategoriesDeepAsync(firstCategoryId);
            return View(result);
        }

        [HttpGet]
        [Route("DeepCategories/Browse/Third/{secondCategoryId}")]
        public async Task<IActionResult> Third(int secondCategoryId)
        {
            var secondCategory = await categoryRepo.SecondCategoryDetailsAsync(secondCategoryId);
            ViewBag.FirstCategoryId = secondCategory.FirstCategoryID;
            ViewBag.SecondCategoryId = secondCategory.SecondCategoryID;
            ViewBag.FirstCategoryName = secondCategory.FirstCategory.Name;
            ViewBag.SecondCategoryName = secondCategory.Name;
            var result = await categoryRepo.GetThirdCategoryDeepAsync(secondCategoryId);
            return View(result);
        }

        [HttpGet]
        [Route("DeepCategories/Browse/Fourth/{thirdCategoryId}")]
        public async Task<IActionResult> Fourth(int thirdCategoryId)
        {
            var thirdCategory = await categoryRepo.ThirdCategoryDetailsAsync(thirdCategoryId);
            ViewBag.FirstCategoryId = thirdCategory.FirstCategoryID;
            ViewBag.SecondCategoryId = thirdCategory.SecondCategoryID;
            ViewBag.ThirdCategoryId = thirdCategory.ThirdCategoryID;
            ViewBag.FirstCategoryName = thirdCategory.FirstCategory.Name;
            ViewBag.SecondCategoryName = thirdCategory.SecondCategory.Name;
            ViewBag.ThirdCategoryName = thirdCategory.Name;
            var result = await categoryRepo.GetFourthCategoryDeepAsync(thirdCategoryId);
            return View(result);
        }

        [HttpGet]
        [Route("DeepCategories/Browse/Fifth/{fourthCategoryId}")]
        public async Task<IActionResult> Fifth(int fourthCategoryId)
        {
            var fourthCategory = await categoryRepo.FourthCategoriesDetailsAsync(fourthCategoryId);
            ViewBag.FirstCategoryId = fourthCategory.FirstCategoryID;
            ViewBag.SecondCategoryId = fourthCategory.SecondCategoryID;
            ViewBag.ThirdCategoryId = fourthCategory.ThirdCategoryID;
            ViewBag.FourthCategoryId = fourthCategoryId;
            ViewBag.FirstCategoryName = fourthCategory.FirstCategory.Name;
            ViewBag.SecondCategoryName = fourthCategory.SecondCategory.Name;
            ViewBag.ThirdCategoryName = fourthCategory.ThirdCategory.Name;
            ViewBag.FourthCategoryName = fourthCategory.Name;
            var result = await categoryRepo.GetFifthCategoryDeepAsync(fourthCategoryId);
            return View(result);
        }

        [HttpGet]
        [Route("DeepCategories/Browse/Sixth/{fifthCategoryId}")]
        public async Task<IActionResult> Sixth(int fifthCategoryId)
        {
            var fifthCategory = await categoryRepo.FifthCategoriesDetailsAsync(fifthCategoryId);
            ViewBag.FirstCategoryId = fifthCategory.FirstCategoryID;
            ViewBag.SecondCategoryId = fifthCategory.SecondCategoryID;
            ViewBag.ThirdCategoryId = fifthCategory.ThirdCategoryID;
            ViewBag.FourthCategoryId = fifthCategory.FourthCategoryID;
            ViewBag.FifthCategoryId = fifthCategoryId;
            ViewBag.FirstCategoryName = fifthCategory.FirstCategory.Name;
            ViewBag.SecondCategoryName = fifthCategory.SecondCategory.Name;
            ViewBag.ThirdCategoryName = fifthCategory.ThirdCategory.Name;
            ViewBag.FourthCategoryName = fifthCategory.Name;
            ViewBag.FifthCategoryName = fifthCategory.Name;
            var result = await categoryRepo.GetSixthCategoryDeepAsync(fifthCategoryId);
            return View(result);
        }
    }
}
