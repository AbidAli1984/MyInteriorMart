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
    public class RoleCategoryAndRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoleCategoryAndRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RoleCategoryAndRoles
        [Authorize(Policy = "Admin-RoleCatNRole-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RoleCategoryAndRole.Include(r => r.RoleCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RoleCategoryAndRoles/Details/5
        [Authorize(Policy = "Admin-RoleCatNRole-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleCategoryAndRole = await _context.RoleCategoryAndRole
                .Include(r => r.RoleCategory)
                .FirstOrDefaultAsync(m => m.RoleCategoryAndRoleID == id);
            if (roleCategoryAndRole == null)
            {
                return NotFound();
            }

            return View(roleCategoryAndRole);
        }

        // GET: RoleCategoryAndRoles/Create
        [Authorize(Policy = "Admin-RoleCatNRole-Create")]
        public async Task<IActionResult> Create()
        {
            var assignedRoles = await _context.RoleCategoryAndRole.Select(i => i.RoleID).ToListAsync();
            var allRoles = await _context.Roles.ToListAsync();
            var unAssignedRoles = allRoles.Where(i => !assignedRoles.Any(e => i.Id.Contains(e))).ToList();

            ViewData["RoleCategoryID"] = new SelectList(_context.RoleCategory, "RoleCategoryID", "CategoryName");
            ViewData["Roles"] = new SelectList(unAssignedRoles, "Id", "Name");
            return View();
        }

        // POST: RoleCategoryAndRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin-RoleCatNRole-Create")]
        public async Task<IActionResult> Create([Bind("RoleCategoryAndRoleID,RoleCategoryID,RoleID")] RoleCategoryAndRole roleCategoryAndRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roleCategoryAndRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleCategoryID"] = new SelectList(_context.RoleCategory, "RoleCategoryID", "CategoryName", roleCategoryAndRole.RoleCategoryID);
            return View(roleCategoryAndRole);
        }

        // GET: RoleCategoryAndRoles/Edit/5
        [Authorize(Policy = "Admin-RoleCatNRole-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleCategoryAndRole = await _context.RoleCategoryAndRole.FindAsync(id);
            if (roleCategoryAndRole == null)
            {
                return NotFound();
            }
            ViewData["RoleCategoryID"] = new SelectList(_context.RoleCategory, "RoleCategoryID", "CategoryName", roleCategoryAndRole.RoleCategoryID);
            return View(roleCategoryAndRole);
        }

        // POST: RoleCategoryAndRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleCategoryAndRoleID,RoleCategoryID,RoleID")] RoleCategoryAndRole roleCategoryAndRole)
        {
            if (id != roleCategoryAndRole.RoleCategoryAndRoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roleCategoryAndRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleCategoryAndRoleExists(roleCategoryAndRole.RoleCategoryAndRoleID))
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
            ViewData["RoleCategoryID"] = new SelectList(_context.RoleCategory, "RoleCategoryID", "CategoryName", roleCategoryAndRole.RoleCategoryID);
            return View(roleCategoryAndRole);
        }

        // GET: RoleCategoryAndRoles/Delete/5
        [Authorize(Policy = "Admin-RoleCatNRole-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleCategoryAndRole = await _context.RoleCategoryAndRole
                .Include(r => r.RoleCategory)
                .FirstOrDefaultAsync(m => m.RoleCategoryAndRoleID == id);
            if (roleCategoryAndRole == null)
            {
                return NotFound();
            }

            return View(roleCategoryAndRole);
        }

        // POST: RoleCategoryAndRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Admin-RoleCatNRole-Create")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roleCategoryAndRole = await _context.RoleCategoryAndRole.FindAsync(id);
            _context.RoleCategoryAndRole.Remove(roleCategoryAndRole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleCategoryAndRoleExists(int id)
        {
            return _context.RoleCategoryAndRole.Any(e => e.RoleCategoryAndRoleID == id);
        }
    }
}
