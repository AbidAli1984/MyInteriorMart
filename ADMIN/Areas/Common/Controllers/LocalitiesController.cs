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
    public class LocalitiesController : Controller
    {
        private readonly SharedDbContext _context;

        public LocalitiesController(SharedDbContext context)
        {
            _context = context;
        }

        // GET: Common/Localities
        [Authorize(Policy = "Admin-Area-ViewAll")]
        [Route("/Common/Localities/Index/{pincodeId}")]
        public async Task<IActionResult> Index(int pincodeId)
        {
            var pincode = await _context.Pincode.Where(i => i.PincodeID == pincodeId).FirstOrDefaultAsync();
            ViewBag.PincodeId = pincode.PincodeID;
            ViewBag.StationId = pincode.StationID;
            ViewBag.Message = TempData["Message"];

            // Shafi: Display locality deleted successfully message in index view
            ViewBag.Deleted = TempData["Deleted"];
            // End:

            var sharedDbContext = _context.Area.Include(l => l.Pincode).Include(l => l.Location).Include(l => l.Location.City.State).Include(l => l.Location.City.State.Country).Where(i => i.PincodeID == pincodeId);
            return View(await sharedDbContext.ToListAsync());
        }

        // GET: Common/Localities/Details/5
        [Authorize(Policy = "Admin-Area-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locality = await _context.Area
                .Include(l => l.Pincode)
                .Include(l => l.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locality == null)
            {
                return NotFound();
            }

            return View(locality);
        }

        // POST: Common/Localities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Area-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string localityName, int? stationId, int? pincodeId)
        {
            if (localityName != null && stationId != null && pincodeId != null)
            {
                Area locality = new Area
                {
                    Name = localityName,
                    LocationId = stationId,
                    PincodeID = pincodeId
                };

                var duplicate = await _context.Area.Where(i => i.PincodeID == pincodeId).AnyAsync(i => i.Name == localityName);

                if (duplicate != true)
                {
                    _context.Add(locality);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"Locality '{localityName}' created successfully.";
                    return Redirect($"/Common/Localities/Index/{pincodeId}");
                }
                else
                {
                    TempData["Message"] = $"Duplicate! Locality '{localityName}' already exists.";
                    return Redirect($"/Common/Localities/Index/{pincodeId}");
                }
            }
            else
            {
                TempData["Message"] = $"Oop! Something went wrong!";
                return Redirect($"/Common/Localities/Index/{pincodeId}");
            }
        }

        // GET: Common/Localities/Edit/5
        [Authorize(Policy = "Admin-Area-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locality = await _context.Area.FindAsync(id);
            if (locality == null)
            {
                return NotFound();
            }
            ViewData["PincodeID"] = new SelectList(_context.Pincode, "PincodeID", "PincodeID", locality.PincodeID);
            ViewData["StationID"] = new SelectList(_context.Location, "StationID", "Name", locality.LocationId);
            return View(locality);
        }

        // POST: Common/Localities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Area-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocalityID,LocalityName,StationID,PincodeID")] Area locality)
        {
            if (id != locality.Id)
            {
                return NotFound();
            }

            string oldLocalityName = await _context.Area.Where(i => i.Id == id).Select(i => i.Name.ToString()).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locality);
                    await _context.SaveChangesAsync();
                    // Shafi: Show city successfully edited message
                    // in index view with TempData["SuccessMessage"]
                    TempData["SuccessMessage"] = "Locality changed from " + oldLocalityName + " to " + locality.Name + " successfully.";
                    // End:
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalityExists(locality.Id))
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
            ViewData["PincodeID"] = new SelectList(_context.Pincode, "PincodeID", "PincodeID", locality.PincodeID);
            ViewData["StationID"] = new SelectList(_context.Location, "StationID", "Name", locality.LocationId);
            return View(locality);
        }

        // GET: Common/Localities/Delete/5
        [Authorize(Policy = "Admin-Area-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locality = await _context.Area
                .Include(l => l.Pincode)
                .Include(l => l.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (locality == null)
            {
                return NotFound();
            }

            return View(locality);
        }

        // POST: Common/Localities/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-Area-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locality = await _context.Area.FindAsync(id);

            // Shafi: Show successfully deleted message in index page via TempData["Deleted"]
            TempData["Deleted"] = "Locality " + locality.Name + " deleted successfully.";
            // End:

            _context.Area.Remove(locality);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool LocalityExists(int id)
        {
            return _context.Area.Any((System.Linq.Expressions.Expression<Func<Area, bool>>)(e => e.Id == id));
        }
    }
}
