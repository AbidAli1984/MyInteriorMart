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

namespace ADMIN.Areas.AllCategories.Controllers
{
    [Area("AllCategories")]
    [Authorize]
    public class FourthCategoriesController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public FourthCategoriesController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllCategories/FourthCategories/{thirdCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? thirdCatId)
        {
            var result = await categoryContext.FourthCategory.Where(i => i.ThirdCategoryID == thirdCatId).OrderBy(i => i.SortOrder).Include(t => t.FirstCategory).Include(t => t.SecondCategory).Include(t => t.ThirdCategory).ToListAsync();
            return View(result);
        }
    }
}
