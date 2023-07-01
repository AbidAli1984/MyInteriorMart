using BOL.IDENTITY;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IUserRoleService
    {
        Task<int> GetRolesCount();
        Task<bool> RoleHasClaimType(string userGuid, string ClaimType);
        Task<bool> RoleHasClaim(string userGuid, string ClaimType, string ClaimValue);

        #region RoleCategory
        Task<List<RoleCategory>> GetRoleCategories();
        Task<RoleCategory> GetRoleCategoryById(int? roleCategoryId);
        Task<RoleCategory> AddRoleCategory(RoleCategory roleCategory);
        Task<RoleCategory> UpdateRoleCategory(RoleCategory roleCategory);
        Task<bool> DeleteRoleCategory(int id);
        #endregion

        #region RoleCategoryAndRole
        Task<RoleCategoryAndRole> GetRoleCategoryAndRoleById(int? id);
        Task<List<RoleCategoryAndRole>> GetRoleCategoryAndRolesIncludeRoleCategory();
        Task<RoleCategoryAndRole> GetRoleCategoryAndRoleIncludeRoleCategoryById(int? id);
        Task<List<IdentityRole>> GetUnassignedRoles();
        Task<RoleCategoryAndRole> AddRoleCategoryAndRole(RoleCategoryAndRole roleCategoryAndRole);
        Task<RoleCategoryAndRole> UpdateRoleCategoryAndRole(RoleCategoryAndRole roleCategoryAndRole);
        Task<bool> DeleteRoleCategoryAndRole(int id);
        #endregion
    }
}
