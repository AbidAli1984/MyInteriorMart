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
    public class StatesController : Controller
    {
        private readonly SharedDbContext _context;
        private readonly IMemoryCache memoryCache;

        public StatesController(SharedDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            this.memoryCache = memoryCache;
        }

        // GET: Common/States
        [Authorize(Policy = "Admin-State-ViewAll")]
        [Route("/Common/States/Index/{countryId}")]
        public async Task<IActionResult> Index(int countryId)
        {
            ViewBag.CountryId = countryId;
            ViewBag.Message = TempData["Message"];
            

            // Shafi: Cach States
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            //List<State> states;
            //if (!memoryCache.TryGetValue("States", out states))
            //{
            //    memoryCache.Set("States", await _context.State.Include(i => i.Country).Where(i => i.CountryID == countryId).ToListAsync());
            //}
            //states = memoryCache.Get("States") as List<State>;
            //stopWatch.Stop();
            //ViewBag.StopWatch = stopWatch.Elapsed;
            // End:

            var states = await _context.State.Include(i => i.Country).Where(i => i.CountryID == countryId).ToListAsync();

            return View(states);
        }

        // POST: Common/States/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin-State-Create")]
        public async Task<IActionResult> Create(string name, int? countryID)
        {
            if (name != null && countryID != null)
            {
                State state = new State
                {
                    Name = name,
                    CountryID = countryID
                };

                var duplicateState = await _context.State.Where(i => i.CountryID == countryID).AnyAsync(i => i.Name == name);

                if (duplicateState != true)
                {
                    _context.Add(state);
                    await _context.SaveChangesAsync();
                    TempData["Message"] = $"State '{name}' created successfully.";
                    return Redirect($"/Common/States/Index/{countryID}");
                }
                else
                {
                    TempData["Message"] = $"Duplicate! State '{name}' already exists.";
                    return Redirect($"/Common/States/Index/{countryID}");
                }
            }
            else
            {
                TempData["Message"] = $"Oop! Something went wrong!";
                return Redirect($"/Common/States/Index/{countryID}");
            }
        }

        // GET: Common/States/Edit/5
        [Authorize(Policy = "Admin-State-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var state = await _context.State.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "ISO3Name", state.CountryID);
            return View(state);
        }

        // POST: Common/States/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin-State-Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("StateID,Name,CountryID")] State state)
        {
            if (id != state.StateID)
            {
                return NotFound();
            }

            string oldStateName = await _context.State.Where(i => i.StateID == id).Select(i => i.Name).FirstOrDefaultAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(state);
                    await _context.SaveChangesAsync();

                    // Shafi: Show city successfully edited message
                    // in index view with TempData["SuccessMessage"]
                    TempData["SuccessMessage"] = "State name changed from " + oldStateName + " to " + state.Name + " successfully.";
                    // End:
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StateExists(state.StateID))
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
            ViewData["CountryID"] = new SelectList(_context.Country, "CountryID", "ISO3Name", state.CountryID);
            return View(state);
        }

        private bool StateExists(int id)
        {
            return _context.State.Any(e => e.StateID == id);
        }
    }
}
