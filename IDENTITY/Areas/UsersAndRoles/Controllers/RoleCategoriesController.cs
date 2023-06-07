using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IDENTITY.Data;
using IDENTITY.Models;
using Microsoft.AspNetCore.Authorization;

namespace IDENTITY.Areas.UsersAndRoles.Controllers
{
    [Area("UsersAndRoles")]
    [Authorize]
    public class RoleCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoleCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoleCategories

        [Authorize(Policy = "Admin-RoleCategories-ViewAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.RoleCategory.ToListAsync());
        }

        // GET: RoleCategories/Details/5
        [Authorize(Policy = "Admin-RoleCategories-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleCategory = await _context.RoleCategory
                .FirstOrDefaultAsync(m => m.RoleCategoryID == id);
            if (roleCategory == null)
            {
                return NotFound();
            }

            return View(roleCategory);
        }

        // GET: RoleCategories/Create
        [Authorize(Policy = "Admin-RoleCategories-Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: RoleCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "Admin.RoleCategories.Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleCategoryID,Priority,CategoryName")] RoleCategory roleCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(roleCategory);
        }

        // GET: RoleCategories/Edit/5
        [Authorize(Policy = "Admin-RoleCategories-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleCategory = await _context.RoleCategory.FindAsync(id);
            if (roleCategory == null)
            {
                return NotFound();
            }
            return View(roleCategory);
        }

        // POST: RoleCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Policy = "Admin-RoleCategories-Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleCategoryID,Priority,CategoryName")] RoleCategory roleCategory)
        {
            if (id != roleCategory.RoleCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleCategoryExists(roleCategory.RoleCategoryID))
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
            return View(roleCategory);
        }

        // GET: RoleCategories/Delete/5
        [Authorize(Policy = "Admin-RoleCategories-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleCategory = await _context.RoleCategory
                .FirstOrDefaultAsync(m => m.RoleCategoryID == id);
            if (roleCategory == null)
            {
                return NotFound();
            }

            return View(roleCategory);
        }

        // POST: RoleCategories/Delete/5
        [Authorize(Policy = "Admin-RoleCategories-Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleCategory = await _context.RoleCategory.FindAsync(id);
            _context.RoleCategory.Remove(roleCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleCategoryExists(int id)
        {
            return _context.RoleCategory.Any(e => e.RoleCategoryID == id);
        }
    }
}
