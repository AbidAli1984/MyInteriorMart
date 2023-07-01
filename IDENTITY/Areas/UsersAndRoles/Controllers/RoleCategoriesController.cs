using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BAL.Services.Contracts;
using BOL.IDENTITY;

namespace IDENTITY.Areas.UsersAndRoles.Controllers
{
    [Area("UsersAndRoles")]
    [Authorize]
    public class RoleCategoriesController : Controller
    {
        private readonly IUserRoleService _userRoleService;

        public RoleCategoriesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        // GET: RoleCategories

        [Authorize(Policy = "Admin-RoleCategories-ViewAll")]
        public async Task<IActionResult> Index()
        {
            return View(await _userRoleService.GetRoleCategories());
        }

        // GET: RoleCategories/Details/5
        [Authorize(Policy = "Admin-RoleCategories-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            var roleCategory = await _userRoleService.GetRoleCategoryById(id);// _context.RoleCategory
            if (roleCategory == null)
                return NotFound();

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
                await _userRoleService.AddRoleCategory(roleCategory);
                return RedirectToAction(nameof(Index));
            }
            return View(roleCategory);
        }

        // GET: RoleCategories/Edit/5
        [Authorize(Policy = "Admin-RoleCategories-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var roleCategory = await _userRoleService.GetRoleCategoryById(id);
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
                    await _userRoleService.UpdateRoleCategory(roleCategory);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var roleCat = await _userRoleService.GetRoleCategoryById(roleCategory.RoleCategoryID);
                    if (roleCat == null)
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
            var roleCategory = await _userRoleService.GetRoleCategoryById(id);
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
            await _userRoleService.DeleteRoleCategory(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
