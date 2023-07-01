using BAL.Services.Contracts;
using BOL.IDENTITY;
using DAL.Models;
using DAL.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            this._userRoleRepository = userRoleRepository;
        }

        public async Task<bool> RoleHasClaimType(string userGuid, string ClaimType)
        {
            var user = await userManager.FindByIdAsync(userGuid);

            if (user == null)
                return false;

            var usersRole = await userManager.GetRolesAsync(user);
            List<string> roleNames = usersRole.ToList();
            List<string> roleHasClaimTypes = new List<string>();

            foreach (var name in roleNames)
            {
                var role = await roleManager.FindByNameAsync(name);
                var claimTypesForThisRole = await _userRoleRepository.GetRoleClaimTypesByRoleId(role.Id);

                if (claimTypesForThisRole != null)
                {
                    foreach (var type in claimTypesForThisRole)
                    {
                        roleHasClaimTypes.Add(type);
                    }
                }
            }

            return roleHasClaimTypes.Contains(ClaimType);
        }

        public async Task<bool> RoleHasClaim(string userGuid, string ClaimType, string ClaimValue)
        {
            var user = await userManager.FindByIdAsync(userGuid);

            if (user != null)
            {
                var usersRole = await userManager.GetRolesAsync(user);
                List<string> roleNames = usersRole.ToList();
                List<string> roleHasClaim = new List<string>();

                foreach (var name in roleNames)
                {
                    var role = await roleManager.FindByNameAsync(name);
                    var claim = await _userRoleRepository.GetRoleClaimNameByRoleIdClaimTypeAndClaimValue(role.Id, ClaimType, ClaimValue);

                    if (claim != null)
                    {
                        roleHasClaim.Add(claim);
                    }
                }

                return roleHasClaim.Contains(ClaimValue);
            }
            else
            {
                return false;
            }
        }

        public async Task<int> GetRolesCount()
        {
            return (await _userRoleRepository.GetUserRoles()).Count();
        }

        #region RoleCategory
        public async Task<List<RoleCategory>> GetRoleCategories()
        {
            return await _userRoleRepository.GetRoleCategories();
        }

        public async Task<RoleCategory> GetRoleCategoryById(int? roleCategoryId)
        {
            if (roleCategoryId == null)
                return null;
            return await _userRoleRepository.GetRoleCategoryById(roleCategoryId);
        }

        public async Task<RoleCategory> AddRoleCategory(RoleCategory roleCategory)
        {
            return await _userRoleRepository.AddRoleCategory(roleCategory);
        }

        public async Task<RoleCategory> UpdateRoleCategory(RoleCategory roleCategory)
        {
            return await _userRoleRepository.UpdateRoleCategory(roleCategory);
        }

        public async Task<bool> DeleteRoleCategory(int id)
        {
            return await _userRoleRepository.DeleteRoleCategory(id); ;
        }
        #endregion

        #region RoleCategoryAndRole
        public async Task<RoleCategoryAndRole> GetRoleCategoryAndRoleById(int? id)
        {
            if (id == null)
                return null;
            return await _userRoleRepository.GetRoleCategoryAndRoleById(id);
        }

        public async Task<List<RoleCategoryAndRole>> GetRoleCategoryAndRolesIncludeRoleCategory()
        {
            return await _userRoleRepository.GetRoleCategoryAndRolesIncludeRoleCategory();
        }

        public async Task<RoleCategoryAndRole> GetRoleCategoryAndRoleIncludeRoleCategoryById(int? id)
        {
            if (id == null)
                return null;
            return await _userRoleRepository.GetRoleCategoryAndRoleIncludeRoleCategoryById(id);
        }

        public async Task<List<IdentityRole>> GetUnassignedRoles()
        {
            var assignedRoles = (await _userRoleRepository.GetRoleCategoryAndRoles()).Select(i => i.RoleID).ToList();
            var allRoles = await _userRoleRepository.GetRoles();
            return allRoles.Where(i => !assignedRoles.Any(e => i.Id.Contains(e))).ToList();
        }

        public async Task<RoleCategoryAndRole> AddRoleCategoryAndRole(RoleCategoryAndRole roleCategoryAndRole)
        {
            return await _userRoleRepository.AddRoleCategoryAndRole(roleCategoryAndRole);
        }

        public async Task<RoleCategoryAndRole> UpdateRoleCategoryAndRole(RoleCategoryAndRole roleCategoryAndRole)
        {
            return await _userRoleRepository.UpdateRoleCategoryAndRole(roleCategoryAndRole);
        }

        public async Task<bool> DeleteRoleCategoryAndRole(int id)
        {
            return await _userRoleRepository.DeleteRoleCategoryAndRole(id); ;
        }
        #endregion
    }
}
