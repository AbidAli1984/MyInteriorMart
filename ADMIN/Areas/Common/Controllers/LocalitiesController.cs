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

            var sharedDbContext = _context.Locality.Include(l => l.Pincode).Include(l => l.Station).Include(l => l.Station.City.State).Include(l => l.Station.City.State.Country).Where(i => i.PincodeID == pincodeId);
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

            var locality = await _context.Locality
                .Include(l => l.Pincode)
                .Include(l => l.Station)
                .FirstOrDefaultAsync(m => m.LocalityID == id);
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
                Locality locality = new Locality
                {
                    LocalityName = localityName,
                    StationID = stationId,
                    PincodeID = pincodeId
                };

                var duplicate = await _context.Locality.Where(i => i.PincodeID == pincodeId).AnyAsync(i => i.LocalityName == localityName);

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

            var locality = await _context.Locality.FindAsync(id);
            if (locality == null)
            {
                return NotFound();
            }
            ViewData["PincodeID"] = new SelectList(_context.Pincode, "PincodeID", "PincodeID", locality.PincodeID);
            ViewData["StationID"] = new SelectList(_context.Station, "StationID", "Name", locality.StationID);
            return View(locality);
        }

        // POST: Common/Localities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Area-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LocalityID,LocalityName,StationID,PincodeID")] Locality locality)
        {
            if (id != locality.LocalityID)
            {
                return NotFound();
            }

            string oldLocalityName = await _context.Locality.Where(i => i.LocalityID == id).Select(i => i.LocalityName.ToString()).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(locality);
                    await _context.SaveChangesAsync();
                    // Shafi: Show city successfully edited message
                    // in index view with TempData["SuccessMessage"]
                    TempData["SuccessMessage"] = "Locality changed from " + oldLocalityName + " to " + locality.LocalityName + " successfully.";
                    // End:
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocalityExists(locality.LocalityID))
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
            ViewData["StationID"] = new SelectList(_context.Station, "StationID", "Name", locality.StationID);
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

            var locality = await _context.Locality
                .Include(l => l.Pincode)
                .Include(l => l.Station)
                .FirstOrDefaultAsync(m => m.LocalityID == id);
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
            var locality = await _context.Locality.FindAsync(id);

            // Shafi: Show successfully deleted message in index page via TempData["Deleted"]
            TempData["Deleted"] = "Locality " + locality.LocalityName + " deleted successfully.";
            // End:

            _context.Locality.Remove(locality);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool LocalityExists(int id)
        {
            return _context.Locality.Any(e => e.LocalityID == id);
        }
    }
}
