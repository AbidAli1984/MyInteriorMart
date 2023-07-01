using BOL.IDENTITY;
using DAL.Repositories.Contracts;
using DAL.USER;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly UserDbContext userDbContext;
        public UserRoleRepository(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<List<string>> GetRoleClaimTypesByRoleId(string roleId)
        {
            return await userDbContext.RoleClaims
                .Where(i => i.RoleId == roleId)
                .Select(i => i.ClaimType)
                .Distinct()
                .ToListAsync();
        }

        public async Task<string> GetRoleClaimNameByRoleIdClaimTypeAndClaimValue(string roleId, 
            string claimType, string claimValue)
        {
            return await userDbContext.RoleClaims
                .Where(r => r.RoleId == roleId && r.ClaimType == claimType && r.ClaimValue == claimValue)
                .Select(i => i.ClaimValue)
                .FirstOrDefaultAsync();
        }

        public async Task<List<IdentityUserRole<string>>> GetUserRoles()
        {
            return await userDbContext.UserRoles.ToListAsync();
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await userDbContext.Roles.ToListAsync();
        }

        #region RoleCategory
        public async Task<List<RoleCategory>> GetRoleCategories()
        {
            return await userDbContext.RoleCategory.ToListAsync();
        }

        public async Task<RoleCategory> GetRoleCategoryById(int? roleCategoryId)
        {
            return await userDbContext.RoleCategory
                .FirstOrDefaultAsync(m => m.RoleCategoryID == roleCategoryId);
        }

        public async Task<RoleCategory> AddRoleCategory(RoleCategory roleCategory)
        {
            var result = await userDbContext.RoleCategory.AddAsync(roleCategory);
            await userDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<RoleCategory> UpdateRoleCategory(RoleCategory roleCategory)
        {
            var result = userDbContext.RoleCategory.Update(roleCategory);
            await userDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteRoleCategory(int id)
        {
            var roleCategory = await userDbContext.RoleCategory.FindAsync(id);
            userDbContext.RoleCategory.Remove(roleCategory);
            await userDbContext.SaveChangesAsync();
            return true;
        }
        #endregion

        #region RoleCategoryAndRole
        public async Task<List<RoleCategoryAndRole>> GetRoleCategoryAndRoles()
        {
            return await userDbContext.RoleCategoryAndRole.ToListAsync();
        }

        public async Task<RoleCategoryAndRole> GetRoleCategoryAndRoleById(int? id)
        {
            return await userDbContext.RoleCategoryAndRole.FindAsync(id);
        }

        public async Task<List<RoleCategoryAndRole>> GetRoleCategoryAndRolesIncludeRoleCategory()
        {
            return await userDbContext.RoleCategoryAndRole.Include(r => r.RoleCategory).ToListAsync();
        }

        public async Task<RoleCategoryAndRole> GetRoleCategoryAndRoleIncludeRoleCategoryById(int? id)
        {
            return await userDbContext.RoleCategoryAndRole
                .Include(r => r.RoleCategory)
                .FirstOrDefaultAsync(m => m.RoleCategoryAndRoleID == id); ;
        }

        public async Task<RoleCategoryAndRole> AddRoleCategoryAndRole(RoleCategoryAndRole roleCategoryAndRole)
        {
            var result = await userDbContext.RoleCategoryAndRole.AddAsync(roleCategoryAndRole);
            await userDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<RoleCategoryAndRole> UpdateRoleCategoryAndRole(RoleCategoryAndRole roleCategoryAndRole)
        {
            var result = userDbContext.RoleCategoryAndRole.Update(roleCategoryAndRole);
            await userDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> DeleteRoleCategoryAndRole(int id)
        {
            var roleCategoryAndRole = await userDbContext.RoleCategoryAndRole.FindAsync(id);
            userDbContext.RoleCategoryAndRole.Remove(roleCategoryAndRole);
            await userDbContext.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
