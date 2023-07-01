using BOL.IDENTITY;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Contracts
{
    public interface IUserRoleRepository
    {
        Task<List<string>> GetRoleClaimTypesByRoleId(string roleId);
        Task<string> GetRoleClaimNameByRoleIdClaimTypeAndClaimValue(string roleId,
            string claimType, string claimValue);
        Task<List<IdentityUserRole<string>>> GetUserRoles();

        Task<List<IdentityRole>> GetRoles();

        #region RoleCategory
        Task<List<RoleCategory>> GetRoleCategories();
        Task<RoleCategory> GetRoleCategoryById(int? roleCategoryId);
        Task<RoleCategory> AddRoleCategory(RoleCategory roleCategory);
        Task<RoleCategory> UpdateRoleCategory(RoleCategory roleCategory);
        Task<bool> DeleteRoleCategory(int id);
        #endregion

        #region RoleCategoryAndRole
        Task<List<RoleCategoryAndRole>> GetRoleCategoryAndRoles();
        Task<RoleCategoryAndRole> GetRoleCategoryAndRoleById(int? id);
        Task<List<RoleCategoryAndRole>> GetRoleCategoryAndRolesIncludeRoleCategory();
        Task<RoleCategoryAndRole> GetRoleCategoryAndRoleIncludeRoleCategoryById(int? id);
        Task<RoleCategoryAndRole> AddRoleCategoryAndRole(RoleCategoryAndRole roleCategoryAndRole);
        Task<RoleCategoryAndRole> UpdateRoleCategoryAndRole(RoleCategoryAndRole roleCategoryAndRole);
        Task<bool> DeleteRoleCategoryAndRole(int id);
        #endregion
    }
}
