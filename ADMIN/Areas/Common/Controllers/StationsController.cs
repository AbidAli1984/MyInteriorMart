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

namespace ADMIN.Areas.Common.Controllers
{
    [Area("Common")]
    public class StationsController : Controller
    {
        private readonly SharedDbContext _context;

        public StationsController(SharedDbContext context)
        {
            _context = context;
        }

        // GET: Common/Stations
        [Authorize(Policy = "Admin-Assembly-ViewAll")]
        [Route("/Common/Stations/Index/{cityId}")]
        public async Task<IActionResult> Index(int cityId)
        {
            var city = await _context.City.Where(i => i.CityID == cityId).FirstOrDefaultAsync();
            ViewBag.CityId = city.CityID;
            ViewBag.Message = TempData["Message"];

            // Shafi: Display assembly deleted successfully message in index view
            ViewBag.Deleted = TempData["Deleted"];
            // End:

            var sharedDbContext = _context.Location.Include(s => s.City).Include(p => p.City.State).Include(p => p.City.State.Country).Where(i => i.CityID == cityId);
            return View(await sharedDbContext.ToListAsync());
        }

        // GET: Common/Stations/Details/5
        [Authorize(Policy = "Admin-Assembly-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Location
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // GET: Common/Stations/Create
        [Authorize(Policy = "Admin-Assembly-Create")]
        public IActionResult Create()
        {
            ViewData["Countries"] = new SelectList(_context.Country, "CountryID", "Name");
            ViewData["CityID"] = new SelectList(_context.City, "CityID", "Name");
            return View();
        }

        // POST: Common/Stations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Assembly-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string name, int? cityID)
        {
            if (name != null && cityID != null)
            {
                Location station = new Location
                {
                    Name = name,
                    CityID = cityID
                };

                var duplicate = await _context.Location.Where(i => i.CityID == cityID).AnyAsync(i => i.Name == name);

                if (duplicate != true)
                {
                    _context.Add(station);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"Station '{name}' created successfully.";
                    return Redirect($"/Common/Stations/Index/{cityID}");
                }
                else
                {
                    TempData["Message"] = $"Duplicate! City '{name}' already exists.";
                    return Redirect($"/Common/Stations/Index/{cityID}");
                }
            }
            else
            {
                TempData["Message"] = $"Oop! Something went wrong!";
                return Redirect($"/Common/Stations/Index/{cityID}");
            }
        }

        // GET: Common/Stations/Edit/5
        [Authorize(Policy = "Admin-Assembly-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Location.FindAsync(id);
            if (station == null)
            {
                return NotFound();
            }
            ViewData["CityID"] = new SelectList(_context.City, "CityID", "Name", station.CityID);
            return View(station);
        }

        // POST: Common/Stations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Assembly-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StationID,Name,CityID")] Location station)
        {
            if (id != station.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(station);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StationExists(station.Id))
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
            ViewData["CityID"] = new SelectList(_context.City, "CityID", "Name", station.CityID);
            return View(station);
        }

        // GET: Common/Stations/Delete/5
        [Authorize(Policy = "Admin-Assembly-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var station = await _context.Location
                .Include(s => s.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (station == null)
            {
                return NotFound();
            }

            return View(station);
        }

        // POST: Common/Stations/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-Assembly-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var station = await _context.Location.FindAsync(id);

            // Shafi: Show successfully deleted message in index page via TempData["Deleted"]
            TempData["Deleted"] = "Assembly " + station.Name + " deleted successfully.";
            // End:

            _context.Location.Remove(station);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StationExists(int id)
        {
            return _context.Location.Any((System.Linq.Expressions.Expression<Func<Location, bool>>)(e => e.Id == id));
        }
    }
}
