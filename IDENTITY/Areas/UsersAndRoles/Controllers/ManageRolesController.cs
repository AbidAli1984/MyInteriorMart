using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDENTITY.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOL.CLAIMS;
using IDENTITY.Data;
using DAL.Models;
using BAL.Services.Contracts;

namespace IDENTITY.Areas.UsersAndRoles.Controllers
{
    [Area("UsersAndRoles")]
    [Authorize]
    public class ManageRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext applicationContext;

        public ManageRolesController(RoleManager<IdentityRole> roleManager, IUserService userService,
            UserManager<ApplicationUser> userManager, ApplicationDbContext applicationContext)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this._userService = userService;
            this.applicationContext = applicationContext;
        }

        // Shafi: List all roles
        [Authorize(Policy = "Admin-Roles-ViewAll")]
        public async Task<IActionResult> Index()
        {
            var result = await roleManager.Roles.ToListAsync();

            return View(result);
        }
        // End:


        // Shafi Wrote: Begin create role
        [HttpGet]
        [Authorize(Policy = "Admin-Roles-Create")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Admin-Roles-Create")]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        // End:

        // Shafi Wrote: Begin edit role
        [HttpGet]
        [Authorize(Policy = "Admin-Roles-Edit")]
        public async Task<ActionResult> EditRole(string id)
        {
            if (id == null)
            {
                ViewBag.ErrorMessage = $"Please give ID of role to edit.";
                return View("NotFound");
            }

            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {id} could not be found.";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            // Shafi Wrote: Get role name by id
            string roleName = roleManager.Roles.Where(i => i.Id == id).Select(i => i.Name).FirstOrDefault();
            // End:

            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "Admin-Roles-Edit")]
        public async Task<ActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);

            // Shafi Wrote: Get old role name by id
            string roleOldName = roleManager.Roles.Where(i => i.Id == model.RoleId).Select(i => i.Name).FirstOrDefault();
            // End:

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with id = {model.RoleId} could not be found.";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }
        // End:

        // Shafi Wrote: Assign role
        [HttpGet]
        [Authorize(Policy = "Admin-Roles-AssignRoleToUser")]
        public async Task<ActionResult> AssignRoleAsync()
        {
            ViewBag.Users = new SelectList(await _userService.GetUsers(), "Id", "UserName");
            ViewBag.Roles = new SelectList(roleManager.Roles.ToList(), "Name", "Name");

            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Admin-Roles-AssignRoleToUser")]
        public async Task<ActionResult> AssignRole(AssignRoleViewModel model)
        {
            // Shafi Wrote: Get User Hastag Id and Role Name
            ViewBag.Users = new SelectList(await _userService.GetUsers(), "Id", "UserName");
            ViewBag.Roles = new SelectList(roleManager.Roles.ToList(), "Id", "Name");
            // End:

            // Shafi Wrote: Create new user object based on User Hastag ID
            var user = await _userService.GetUserById(model.UserId);
            // End:

            // Shafi Wrote: Get user Email ID
            var email = user.UserName;
            // End:

            // Shafi Wrote: Assign user to role if not already in this role
            try
            {
                bool isInRole = await userManager.IsInRoleAsync(user, model.RoleName);
                if (isInRole)
                {
                    ViewBag.Warning = $"{email} already in {model.RoleName} role";
                }
                else
                {
                    await userManager.AddToRoleAsync(user, model.RoleName);
                    ViewBag.Success = $"{model.RoleName} role assigned to {email}";
                }

                ViewBag.Users = new SelectList(await _userService.GetUsers(), "Id", "UserName");
                ViewBag.Roles = new SelectList(roleManager.Roles.ToList(), "Id", "Name");

                return View();
            }
            catch (Exception error)
            {
                // Shafi Written: Show Message
                ViewBag.Error = $"Click on Assign User To Role button";
                ViewBag.HideError = error;
                // End:

                // Shafi Wrote: Get User Hastag Id and Role Name
                ViewBag.Users = new SelectList(await _userService.GetUsers(), "Id", "UserName");
                ViewBag.Roles = new SelectList(roleManager.Roles.ToList(), "Id", "Name");
                // End:

                return View();
            }
            // End:
        }
        // End:

        // Shafi Wrote: Remove user from single role
        // Shafi Wrote: This ActionResult can be used to remove a single user from multiple roles
        [HttpGet]
        [Authorize(Policy = "Admin-Roles-RemoveUserFromRole")]
        [Route("~/SuperAdministrator/ManageRoles/RemoveUserFromRole/{userName}-{roleName}")]
        public async Task<ActionResult> RemoveUserFromRole(string userName, string roleName)
        {
            // Shafi Wrote: Find user by his email or user id
            var user = await _userService.GetUserByUserNameOrEmail(userName);
            // End:

            // Shafi Wrote: Get role id to pass as a parameter in RedirectionAction
            IdentityRole role = await roleManager.FindByNameAsync(roleName);
            var roleHashCode = role.Id;
            // End:

            // If either userName or roleName is null then show alert message and redirect to ListUsersInRole action result
            if (userName == null || roleName == null)
            {
                TempData["RemoveUserFromRoleMessage"] = $"Opps! Either User Name or Role Name is missing from URL parameters. Please try again by refreshing this page or contact the Administrator.";
                TempData["AlertColor"] = "bg-red";
                TempData["AlertIcon "] = "zmdi-block";
                TempData["RoleName"] = roleName;

                return Redirect($"~/SuperAdministrator/ManageRoles/ListUsersInRole/{roleHashCode}");
            }
            // End:

            // Shafi Wrote: Return string of role names to supply as parameter to RemoveFromRolesAsync() method
            string[] listRoleNames;
            listRoleNames = new string[1];
            listRoleNames[0] = roleName;
            // End:

            // Shafi Wrote: remove user from listRoleNames
            var removeUserFromRole = userManager.RemoveFromRolesAsync(user, listRoleNames);
            // End"

            // Shafi Wrote: Check if user has removed from listRoleNames
            try
            {
                // If user removed then show successfull message and redirect to ListUsersInRole action result
                if (removeUserFromRole.Result.Succeeded)
                {
                    TempData["RemoveUserFromRoleMessage"] = $"User {userName} successfully removed from role {listRoleNames[0]}";
                    TempData["AlertColor"] = "bg-green";
                    TempData["AlertIcon "] = "zmdi-thumb-up";
                    TempData["RoleName"] = roleName;

                    return Redirect($"~/SuperAdministrator/ManageRoles/ListUsersInRole/{roleHashCode}");
                }
                // End:

                // // Shafi Wrote: If user failed to be removed then show Opps! message and redirect to ListUsersInRole action result
                else
                {
                    TempData["RemoveUserFromRoleMessage"] = $"Opps! Something went wrong User {userName} could not be removed from role {listRoleNames[0]}";
                    TempData["AlertColor"] = "bg-deep-purple";
                    TempData["AlertIcon "] = "zmdi-notifications";
                    TempData["RoleName"] = roleName;

                    return Redirect($"~/SuperAdministrator/ManageRoles/ListUsersInRole/{roleHashCode}");
                }
                // End:
            }
            // Shafi Wrote: If somthing else happened the trace error message and redirect to ListUsersInRole action result
            catch (Exception error)
            {
                TempData["RemoveUserFromRoleMessage"] = error.Message;
                TempData["AlertColor"] = "bg-red";
                TempData["AlertIcon "] = "zmdi-block";
                TempData["RoleName"] = roleName;

                return Redirect($"~/SuperAdministrator/ManageRoles/ListUsersInRole/{roleHashCode}");
            }
            // End:

        }
        // End:


        // Shafi Wrote: List users in a particular role
        [Route("~/SuperAdministrator/ManageRoles/ListUsersInRole/{roleHashCode}")]
        [Authorize(Policy = "Admin-Roles-ListUsersInRole")]
        [HttpGet]
        public async Task<IActionResult> ListUsersInRole(string roleHashCode)
        {
            // Shafi Wrote: Find role by roleHashCode asynchronously
            var role = await roleManager.FindByIdAsync(roleHashCode);
            // End:

            // Shafi Wrote: Get role name to show in view
            TempData["RoleName"] = role.Name;
            // End:

            // Shafi Wrote: Check if role is not null
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role  with ID = {roleHashCode} cannot be found.";
                return View("NotFound");
            }

            // Shafi Wrote: Assign values to ShowUserByRoleViewModel
            var model = new ShowUsersByRoleViewModel
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            // End:

            // Shafi Wrote: Get list of users in role
            var users = await _userService.GetUsers();
            foreach (var user in users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.UserName.Add(user.UserName);
                }
            }
            // End:

            return View(model);
        }
        // End
    }
}