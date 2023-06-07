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
    public class PincodesController : Controller
    {
        private readonly SharedDbContext _context;

        public PincodesController(SharedDbContext context)
        {
            _context = context;
        }

        // GET: Common/Pincodes
        [Authorize(Policy = "Admin-Pincode-ViewAll")]
        [Route("/Common/Pincodes/Index/{stationId}")]
        public async Task<IActionResult> Index(int stationId)
        {
            var station = await _context.Station.Where(i => i.StationID == stationId).FirstOrDefaultAsync();
            ViewBag.StationId = station.StationID;
            ViewBag.Message = TempData["Message"];

            // Shafi: Display assembly deleted successfully message in index view
            ViewBag.Deleted = TempData["Deleted"];
            // End:

            var sharedDbContext = _context.Pincode.Include(p => p.Station).Include(p => p.Station.City).Include(p => p.Station.City.State).Include(p => p.Station.City.State.Country).Where(i => i.StationID == stationId);
            return View(await sharedDbContext.ToListAsync());
        }

        // GET: Common/Pincodes/Details/5
        [Authorize(Policy = "Admin-Pincode-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pincode = await _context.Pincode
                .Include(p => p.Station)
                .FirstOrDefaultAsync(m => m.PincodeID == id);
            if (pincode == null)
            {
                return NotFound();
            }

            return View(pincode);
        }

        // GET: Common/Pincodes/Create
        [Authorize(Policy = "Admin-Pincode-Create")]
        public IActionResult Create()
        {
            // Shafi: Display country dropdown list in create view
            ViewData["Countries"] = new SelectList(_context.Country, "CountryID", "Name");
            // End:

            ViewData["StationID"] = new SelectList(_context.Station, "StationID", "Name");
            return View();
        }

        // POST: Common/Pincodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Pincode-Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? pincodeNumber, int? stationID)
        {
            if (pincodeNumber != null && stationID != null)
            {
                Pincode pincode = new Pincode
                {
                    PincodeNumber = pincodeNumber.Value,
                    StationID = stationID
                };

                var duplicate = await _context.Pincode.Where(i => i.StationID == stationID).AnyAsync(i => i.PincodeNumber == pincodeNumber);

                if (duplicate != true)
                {
                    _context.Add(pincode);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"Pincode '{pincodeNumber}' created successfully.";
                    return Redirect($"/Common/Pincodes/Index/{stationID}");
                }
                else
                {
                    TempData["Message"] = $"Duplicate! Pincode '{pincodeNumber}' already exists.";
                    return Redirect($"/Common/Pincodes/Index/{stationID}");
                }
            }
            else
            {
                TempData["Message"] = $"Oop! Something went wrong!";
                return Redirect($"/Common/Pincodes/Index/{stationID}");
            }
        }

        // GET: Common/Pincodes/Edit/5
        [Authorize(Policy = "Admin-Pincode-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pincode = await _context.Pincode.FindAsync(id);
            if (pincode == null)
            {
                return NotFound();
            }
            ViewData["StationID"] = new SelectList(_context.Station, "StationID", "Name", pincode.StationID);
            return View(pincode);
        }

        // POST: Common/Pincodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Policy = "Admin-Pincode-Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PincodeID,PincodeNumber,StationID")] Pincode pincode)
        {
            if (id != pincode.PincodeID)
            {
                return NotFound();
            }

            string oldPincode = await _context.Pincode.Where(i => i.PincodeID == id).Select(i => i.PincodeNumber.ToString()).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pincode);
                    await _context.SaveChangesAsync();
                    // Shafi: Show city successfully edited message
                    // in index view with TempData["SuccessMessage"]
                    TempData["SuccessMessage"] = "Pincode changed from " + oldPincode + " to " + pincode.PincodeNumber + " successfully.";
                    // End:
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PincodeExists(pincode.PincodeID))
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
            ViewData["StationID"] = new SelectList(_context.Station, "StationID", "Name", pincode.StationID);
            return View(pincode);
        }

        // GET: Common/Pincodes/Delete/5
        [Authorize(Policy = "Admin-Pincode-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pincode = await _context.Pincode
                .Include(p => p.Station)
                .FirstOrDefaultAsync(m => m.PincodeID == id);
            if (pincode == null)
            {
                return NotFound();
            }

            return View(pincode);
        }

        // POST: Common/Pincodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "Admin-Pincode-Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pincode = await _context.Pincode.FindAsync(id);

            // Shafi: Show successfully deleted message in index page via TempData["Deleted"]
            TempData["Deleted"] = "Pincode " + pincode.PincodeNumber + " deleted successfully.";
            // End:

            _context.Pincode.Remove(pincode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PincodeExists(int id)
        {
            return _context.Pincode.Any(e => e.PincodeID == id);
        }
    }
}
