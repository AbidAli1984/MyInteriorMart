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
    public class FifthCategoriesController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public FifthCategoriesController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllCategories/FifthCategories/{fourthCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? fourthCatId)
        {
            var result = await categoryContext.FifthCategory.Where(i => i.FourthCategoryID == fourthCatId).OrderBy(i => i.SortOrder).Include(t => t.FirstCategory).Include(t => t.SecondCategory).Include(t => t.ThirdCategory).Include(t => t.FourthCategory).ToListAsync();
            return View(result);
        }
    }
}
