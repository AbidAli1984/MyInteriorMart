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
    public class SixthCategoriesController : Controller
    {
        private readonly CategoriesDbContext categoryContext;

        public SixthCategoriesController(CategoriesDbContext categoryContext)
        {
            this.categoryContext = categoryContext;
        }

        [Route("/AllCategories/SixthCategories/{fifthCatId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int? fifthCatId)
        {
            var result = await categoryContext.SixthCategory.Where(i => i.FifthCategoryID == fifthCatId).OrderBy(i => i.SortOrder).Include(t => t.FirstCategory).Include(t => t.SecondCategory).Include(t => t.ThirdCategory).Include(t => t.FourthCategory).Include(t => t.FifthCategory).ToListAsync();
            return View(result);
        }
    }
}
