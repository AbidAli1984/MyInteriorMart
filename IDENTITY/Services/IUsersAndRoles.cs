using IDENTITY.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDENTITY.Services
{
    public interface IUsersAndRoles
    {
        Task<IEnumerable<IdentityRole>> GetAllRolesAsync();

        // Shafi: Suspend and Unsuspend Users
        public Task<bool> UserSuspended(string UserGuid);
        public Task SuspendUser(string SuspendedTo, string SuspendedBy, DateTime SuspendedDate, string ReasonForSuspending);
        public Task UnsuspendUser(string UnsuspendedTo, string UnsuspendedBy, DateTime UnsuspendedDate, string ReasonForUnsuspending);
        public Task<IEnumerable<SuspendedUser>> ListBlockedUserAsync();

        public Task<IEnumerable<SuspendedUser>> ListUnblockedUserAsync();
        // End:

        public Task<bool> CheckIfUserWithSameMobileExists(string mobile);

    }
}
