using IDENTITY.Data;
using IDENTITY.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY.Services
{
    public class UsersAndRoles : IUsersAndRoles
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext applicationContext;
        public UsersAndRoles(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext applicationContext)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.applicationContext = applicationContext;
        }
        public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return roles;
        }

        // Shafi: Suspend and Unsuspend Users
        public async Task<bool> UserSuspended(string UserGuid)
        {
            // Shafi: Check if user already present in Suspended list
            return await applicationContext.SuspendedUser.AnyAsync(x => x.SuspendedTo.Contains(UserGuid) && x.Suspended == true);
            // End:
        }

        public async Task SuspendUser(string SuspendedTo, string SuspendedBy, DateTime SuspendedDate, string ReasonForSuspending)
        {
            // Shafi: If user non present in Suspended list
            bool userPresent = await applicationContext.SuspendedUser.AnyAsync(x => x.SuspendedTo.Contains(SuspendedTo));
            // End:

            // Shafi: If userPresent in Suspended list == false executive this
            if(userPresent == false && SuspendedTo != SuspendedBy)
            {
                SuspendedUser user = new SuspendedUser()
                {
                    SuspendedTo = SuspendedTo,
                    SuspendedBy = SuspendedBy,
                    SuspendedDate = SuspendedDate,
                    ReasonForSuspending = ReasonForSuspending,
                    Suspended = true,
                    
                };

                await applicationContext.AddAsync(user);
                await applicationContext.SaveChangesAsync();
            }
            // End:

            // Shafi: Check if user already present in Suspended list
            if (userPresent == true && SuspendedTo != SuspendedBy)
            {
                // Shafi: Find user in Suspended list
                var user = await applicationContext.SuspendedUser.FirstOrDefaultAsync(i => i.SuspendedTo == SuspendedTo);
                // End:

                if(user.Suspended == false)
                {
                    // Shafi: Update unsuspanded user details
                    user.Suspended = true;
                    user.SuspendedBy = SuspendedBy;
                    user.SuspendedDate = SuspendedDate;
                    user.ReasonForSuspending = ReasonForSuspending;
                    // End:

                    applicationContext.Update(user);
                    await applicationContext.SaveChangesAsync();
                }
            }
            // End:
        }

        public async Task UnsuspendUser(string UnsuspendedTo, string UnsuspendedBy, DateTime UnsuspendedDate, string ReasonForUnsuspending)
        {
            // Shafi: Check if user already present in Suspended list
            bool userPresent = await applicationContext.SuspendedUser.AnyAsync(x => x.SuspendedTo.Contains(UnsuspendedTo));
            // End:

            // Shafi: If userPresent in Suspended list == true executive this
            if (userPresent == true && UnsuspendedTo != UnsuspendedBy)
            {
                // Shafi: Find user in Suspended list
                var user = await applicationContext.SuspendedUser.FirstOrDefaultAsync(i => i.SuspendedTo == UnsuspendedTo);
                // End:

                // Shafi: Update unsuspanded user details
                user.Suspended = false;
                user.UnsuspendedBy = UnsuspendedBy;
                user.UnsuspendedDate = UnsuspendedDate;
                user.ReasonForUnsuspending = ReasonForUnsuspending;
                // End:
                applicationContext.Update(user);
                await applicationContext.SaveChangesAsync();
            }
            // End:

        }
        // End:

        // Shafi: List blocked users
        public async Task<IEnumerable<SuspendedUser>> ListBlockedUserAsync()
        {
            return await applicationContext.SuspendedUser.Where(x => x.Suspended == true).ToListAsync();
        }
        // End:

        // Shafi: List Unblocked users
        public async Task<IEnumerable<SuspendedUser>> ListUnblockedUserAsync()
        {
            return await applicationContext.SuspendedUser.Where(x => x.Suspended == false).ToListAsync();
        }
        // End:

        public async Task<bool> CheckIfUserWithSameMobileExists(string mobile)
        {
            var result = await applicationContext.Users.AnyAsync(i => i.PhoneNumber == mobile);
            return result;
        }
    }
}
