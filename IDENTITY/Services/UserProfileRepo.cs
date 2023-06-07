using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IDENTITY.Models;
using IDENTITY.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IDENTITY.Services
{
    public class UserProfileRepo : IUserProfileRepo
    {
        private readonly ApplicationDbContext applicationContext;
        private readonly UserManager<IdentityUser> UserManager;
        public UserProfileRepo(ApplicationDbContext applicationContext, UserManager<IdentityUser> userManager)
        {
            this.applicationContext = applicationContext;
            UserManager = userManager;
        }

        public async Task<UserProfile> GetUserProfileAsync(string OwnerGuid)
        {
            var profile = await applicationContext.UserProfile.Where(p => p.OwnerGuid == OwnerGuid).FirstOrDefaultAsync();
            return profile;
        }

        public string UserEmailByGuid(string userGuid)
        {
            var user = UserManager.FindByIdAsync(userGuid).GetAwaiter().GetResult();
            return user.Email;
        }
    }
}
