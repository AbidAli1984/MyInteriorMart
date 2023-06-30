using BAL.Services.Contracts;
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
            return await _userRoleRepository.GetRolesCount();
        }
    }
}
