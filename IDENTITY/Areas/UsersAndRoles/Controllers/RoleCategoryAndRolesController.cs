using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using BAL.Services.Contracts;
using BOL.IDENTITY;

namespace IDENTITY.Areas.UsersAndRoles.Controllers
{
    [Area("UsersAndRoles")]
    [Authorize]
    public class RoleCategoryAndRolesController : Controller
    {
        private readonly IUserRoleService _userRoleService;

        public RoleCategoryAndRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        // GET: RoleCategoryAndRoles
        [Authorize(Policy = "Admin-RoleCatNRole-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = await _userRoleService.GetRoleCategoryAndRolesIncludeRoleCategory();
            return View(applicationDbContext);
        }

        // GET: RoleCategoryAndRoles/Details/5
        [Authorize(Policy = "Admin-RoleCatNRole-Read")]
        public async Task<IActionResult> Details(int? id)
        {
            var roleCategoryAndRole = await _userRoleService.GetRoleCategoryAndRoleIncludeRoleCategoryById(id);
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
            var unAssignedRoles = await _userRoleService.GetUnassignedRoles();

            ViewData["RoleCategoryID"] = new SelectList(await _userRoleService.GetRoleCategories(), "RoleCategoryID", "CategoryName");
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
                await _userRoleService.AddRoleCategoryAndRole(roleCategoryAndRole);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleCategoryID"] = new SelectList(await _userRoleService.GetRoleCategories(), "RoleCategoryID", "CategoryName", roleCategoryAndRole.RoleCategoryID);
            return View(roleCategoryAndRole);
        }

        // GET: RoleCategoryAndRoles/Edit/5
        [Authorize(Policy = "Admin-RoleCatNRole-Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            var roleCategoryAndRole = await _userRoleService.GetRoleCategoryAndRoleById(id);
            if (roleCategoryAndRole == null)
            {
                return NotFound();
            }
            ViewData["RoleCategoryID"] = new SelectList(await _userRoleService.GetRoleCategories(), "RoleCategoryID", "CategoryName", roleCategoryAndRole.RoleCategoryID);
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
                    await _userRoleService.UpdateRoleCategoryAndRole(roleCategoryAndRole);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var roleCatAndRole = await _userRoleService.GetRoleCategoryAndRoleById(roleCategoryAndRole.RoleCategoryAndRoleID);
                    if (roleCatAndRole == null)
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
            ViewData["RoleCategoryID"] = new SelectList(await _userRoleService.GetRoleCategories(), "RoleCategoryID", "CategoryName", roleCategoryAndRole.RoleCategoryID);
            return View(roleCategoryAndRole);
        }

        // GET: RoleCategoryAndRoles/Delete/5
        [Authorize(Policy = "Admin-RoleCatNRole-Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            var roleCategoryAndRole = await _userRoleService.GetRoleCategoryAndRoleIncludeRoleCategoryById(id);

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
            await _userRoleService.DeleteRoleCategoryAndRole(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
