using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.LISTING;
using DAL.LISTING;

namespace ADMIN.Controllers
{
    public class ListingsController : Controller
    {
        private readonly ListingDbContext _context;

        public ListingsController(ListingDbContext context)
        {
            _context = context;
        }

        // GET: Listings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Listing.ToListAsync());
        }

        // GET: Listings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listing
                .FirstOrDefaultAsync(m => m.ListingID == id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // GET: Listings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Listings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ListingID,OwnerGuid,IPAddress,CreatedDate,CreatedTime,Name,Gender,CompanyName,YearOfEstablishment,NumberOfEmployees,Designation,NatureOfBusiness,Turnover,ListingURL")] Listing listing)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listing);
        }

        // GET: Listings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listing.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }

        // POST: Listings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListingID,OwnerGuid,IPAddress,CreatedDate,CreatedTime,Name,Gender,CompanyName,YearOfEstablishment,NumberOfEmployees,Designation,NatureOfBusiness,Turnover,ListingURL")] Listing listing)
        {
            if (id != listing.ListingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListingExists(listing.ListingID))
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
            return View(listing);
        }

        // GET: Listings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listing = await _context.Listing
                .FirstOrDefaultAsync(m => m.ListingID == id);
            if (listing == null)
            {
                return NotFound();
            }

            return View(listing);
        }

        // POST: Listings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listing = await _context.Listing.FindAsync(id);
            _context.Listing.Remove(listing);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListingExists(int id)
        {
            return _context.Listing.Any(e => e.ListingID == id);
        }
    }
}
