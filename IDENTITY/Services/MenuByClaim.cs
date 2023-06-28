using DAL.Models;
using IDENTITY.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY.Services
{
    public class MenuByClaim : IMenuByClaim
    {
        private readonly ApplicationDbContext applicationContext;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public MenuByClaim(ApplicationDbContext applicationContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.applicationContext = applicationContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<bool> RoleHasClaimType(string userGuid, string ClaimType)
        {
            // Shafi: Find user
            var user = await userManager.FindByIdAsync(userGuid);
            // End:

            if (user != null)
            {
                // Shafi: Get List of RoleID's users in in
                var usersRole = await userManager.GetRolesAsync(user);
                // End:

                // Shafi: Crate string List<roleIds>
                List<string> roleNames = usersRole.ToList();
                // End:

                // Shafi: Get list of all role claim types
                var roleClaims = await applicationContext.RoleClaims.Select(x => x.ClaimType).Distinct().ToListAsync();
                // End:

                // Shafi: Create an empty roleHasClaimTypes string list
                List<string> roleHasClaimTypes = new List<string>();
                // End:

                // Shafi: Create claimType list for each roleId and then concatenate all
                foreach (var name in roleNames)
                {
                    // Shafi: Find role
                    var role = await roleManager.FindByNameAsync(name);
                    // End:

                    // Shafi: Get all claimType for this role
                    var claimTypesForThisRole = await applicationContext.RoleClaims.Where(i => i.RoleId == role.Id).Select(i => i.ClaimType).Distinct().ToListAsync();
                    // End:

                    // Shafi: Foreach claimType in ClaimTypeForThisRole add claimType to roleHasClaimTypes list
                    if (claimTypesForThisRole != null)
                    {
                        foreach (var type in claimTypesForThisRole)
                        {
                            roleHasClaimTypes.Add(type);
                        }
                    }
                    // End:
                }
                // End:

                // Check if value of ClaimType exists in roleHasClaimTypes list
                if (roleHasClaimTypes.Contains(ClaimType) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                // End:
            }
            else
            {
                return false;
            }
            // End:
        }

        public async Task<bool> RoleHasClaim(string userGuid, string ClaimType, string ClaimValue)
        {
            // Shafi: Find user
            var user = await userManager.FindByIdAsync(userGuid);
            // End:

            if (user != null)
            {
                // Shafi: Get List of RoleID's users in in
                var usersRole = await userManager.GetRolesAsync(user);
                // End:

                // Shafi: Crate string List<roleIds>
                List<string> roleNames = usersRole.ToList();
                // End:

                // Shafi: Create an empty roleHasClaim list of type string
                List<string> roleHasClaim = new List<string>();
                // End:

                // Shafi: Create ClaimValue list for each roleId and then concatenate all
                foreach (var name in roleNames)
                {
                    // Shafi: Find role
                    var role = await roleManager.FindByNameAsync(name);
                    // End:

                    // Shafi: Get all claims for role
                    var claimsForRole = await applicationContext.RoleClaims.Where(r => r.RoleId == role.Id && r.ClaimType == ClaimType).ToListAsync();
                    // End:

                    // Shafi: Get all claim values for this role
                    var claim = claimsForRole.Where(i => i.ClaimValue == ClaimValue).Select(i => i.ClaimValue).FirstOrDefault();
                    // End:

                    // Shafi: Add claim in roleHasClaim add claimValue to roleHasClaim list
                    if (claim != null)
                    {
                        roleHasClaim.Add(claim);
                    }
                    // End:
                }
                // End:

                // Check if value of claim exists in roleHasClaim list
                if (roleHasClaim.Contains(ClaimValue) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                // End:
            }
            else
            {
                return false;
            }
            // End:
        }
    }
}
