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

        public async Task<IEnumerable<IdentityUserRole<string>>> GetRoles()
        {
            return await userDbContext.UserRoles.ToListAsync();
        }
    }
}
