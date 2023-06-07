using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.SHARED;
using DAL.SHARED;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;

namespace ADMIN.Areas.Common.Controllers
{
    [Area("Common")]
    public class CountriesController : Controller
    {
        private readonly SharedDbContext _context;

        public CountriesController(SharedDbContext context)
        {
            _context = context;
        }

        // GET: Common/Countries
        [Authorize(Policy = "Admin-Country-ViewAll")]
        public async Task<IActionResult> Index()
        {
            // Shafi: Display pincode created successfully message in index view
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            // End:

            return View(await _context.Country.ToListAsync());
        }

        // GET: Common/Countries/Create
        [Authorize(Policy = "Admin-Country-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Common/Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin-Country-Create")]
        public async Task<IActionResult> Create([Bind("CountryID,Name,SortName,ISO3Name,Capital,Currency,PhoneCode")] Country country)
        {
            
            if (ModelState.IsValid)
            {
                // Shafi: Check if country already exists in pincode
                if (await _context.Country.AnyAsync(c => c.Name.Contains(country.Name)))
                {
                    ViewBag.Message = country.Name + " already exists.";
                    return View();
                }
                // End:

                // Shafi: Save country if all is well
                _context.Add(country);
                await _context.SaveChangesAsync();
                // End:

                // Shafi: Show successfully creatd message in index page via TempData["SuccessMessage"]
                TempData["SuccessMessage"] = country.Name + " created successfully.";
                // End:

                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        // GET: Common/Countries/Edit/5
        [Authorize(Policy = "Admin-Country-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Common/Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin-Country-Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("CountryID,Name,SortName,ISO3Name,Capital,Currency,PhoneCode")] Country country)
        {
            if (id != country.CountryID)
            {
                return NotFound();
            }

            string oldCountryName = await _context.Country.Where(i => i.CountryID == id).Select(i => i.Name).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    // Shafi: Show city successfully edited message
                    // in index view with TempData["SuccessMessage"]
                    TempData["SuccessMessage"] = "City name changed from " + oldCountryName + " to " + country.Name + " successfully.";
                    // End:
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(country.CountryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }
        

        private bool CountryExists(int id)
        {
            return _context.Country.Any(e => e.CountryID == id);
        }
    }
}
