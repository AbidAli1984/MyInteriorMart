using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.SHARED;
using DAL.SHARED;

namespace ADMIN.Areas.Categories.Controllers
{
    [Area("Categories")]
    public class HomeController : Controller
    {
        private readonly SharedDbContext _context;

        public HomeController(SharedDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Countries"] = new SelectList(_context.Country, "CountryID", "Name");
            return View();
        }
    }
}
