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
using Microsoft.Extensions.Caching.Memory;

namespace ADMIN.Areas.Common.Controllers
{
    [Area("Common")]
    public class CitiesController : Controller
    {
        private readonly SharedDbContext _context;
        private readonly IMemoryCache memoryCache;

        public CitiesController(SharedDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult ClearCache(string catcheKey)
        {
            memoryCache.Remove("Cities");
            return RedirectToAction("Index");
        }

        // GET: Common/Cities
        [Authorize(Policy = "Admin-City-ViewAll")]
        [Route("/Common/Cities/Index/{stateId}")]
        public async Task<IActionResult> Index(int stateId)
        {
            var state = await _context.State.Where(i => i.StateID == stateId).FirstOrDefaultAsync();
            ViewBag.CountryId = state.CountryID;
            ViewBag.StateId = state.StateID;
            ViewBag.Message = TempData["Message"];

            var cities = await _context.City.Include(c => c.State).Include(c => c.Country).Include(i => i.State).Where(i => i.StateID == stateId).ToListAsync();

            return View(cities);
        }

        // GET: Common/Cities/Create
        [Authorize(Policy = "Admin-City-Create")]
        public IActionResult Create()
        {
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "Name");
            ViewData["StateID"] = new SelectList(_context.State, "StateID", "Name");
            return View();
        }

        // POST: Common/Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-City-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? countryID, int? stateID, string name)
        {
            if (countryID != null && stateID != null && name != null)
            {
                City city = new City
                {
                    CountryID = countryID,
                    StateID = stateID,
                    Name = name
                    
                };

                var duplicate = await _context.City.Where(i => i.StateID == stateID).AnyAsync(i => i.Name == name);

                if (duplicate != true)
                {
                    _context.Add(city);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"City '{name}' created successfully.";
                    return Redirect($"/Common/Cities/Index/{stateID}");
                }
                else
                {
                    TempData["Message"] = $"Duplicate! City '{name}' already exists.";
                    return Redirect($"/Common/Cities/Index/{stateID}");
                }
            }
            else
            {
                TempData["Message"] = $"Oop! Something went wrong!";
                return Redirect($"/Common/States/Index/{countryID}");
            }
        }

        // GET: Common/Cities/Edit/5
        [Authorize(Policy = "Admin-City-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "ISO3Name", city.CountryID);
            ViewData["StateID"] = new SelectList(_context.State, "StateID", "Name", city.StateID);
            return View(city);
        }

        // POST: Common/Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-City-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityID,CountryID,StateID,Name")] City city)
        {
            if (id != city.CityID)
            {
                return NotFound();
            }

            string oldCityName = await _context.City.Where(i => i.CityID == id).Select(i => i.Name).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);

                    // Shafi: Show city successfully edited message
                    // in index view with TempData["SuccessMessage"]
                    TempData["SuccessMessage"] = "City name changed from " + oldCityName + " to " + city.Name + " successfully.";
                    // End:

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.CityID))
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
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "ISO3Name", city.CountryID);
            ViewData["StateID"] = new SelectList(_context.State, "StateID", "Name", city.StateID);
            return View(city);
        }

        private bool CityExists(int id)
        {
            return _context.City.Any(e => e.CityID == id);
        }
    }
}
