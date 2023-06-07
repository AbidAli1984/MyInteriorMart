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
    public class ThirdCategoriesController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public ThirdCategoriesController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllCategories/ThirdCategories/{secondCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? secondCatId)
        {
            var result = categoryContext.ThirdCategory.Where(i => i.SecondCategoryID == secondCatId).OrderBy(i => i.SortOrder).Include(t => t.FirstCategory).Include(t => t.SecondCategory);
            return View(await result.ToListAsync());
        }
    }
}
