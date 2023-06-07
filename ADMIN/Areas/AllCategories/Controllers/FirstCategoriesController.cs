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
    public class FirstCategoriesController : Controller
    {
        private readonly CategoriesDbContext categoriesContext;

        public FirstCategoriesController(CategoriesDbContext categoriesContext)
        {
            this.categoriesContext = categoriesContext;
        }
        public async Task<IActionResult> Index()
        {
            return View(await categoriesContext.FirstCategory.OrderBy(i => i.SortOrder).ToListAsync());
        }
    }
}
