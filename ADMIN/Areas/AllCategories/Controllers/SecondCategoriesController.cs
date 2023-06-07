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
    public class SecondCategoriesController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public SecondCategoriesController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllCategories/SecondCategories/{firstCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? firstCatId)
        {
            var result = await categoryContext.SecondCategory.Where(i => i.FirstCategoryID == firstCatId).OrderBy(i => i.SortOrder).Include(s => s.FirstCategory).ToListAsync();
            return View(result);
        }
    }
}
