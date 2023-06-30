using BOL.IDENTITY;
using DAL.Repositories.Contracts;
using DAL.USER;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserDbContext userDbContext;
        public UserProfileRepository(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }

        public async Task<UserProfile> AddUserProfile(UserProfile userProfile)
        {
            var result = await userDbContext.UserProfile.AddAsync(userProfile);
            await userDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserProfile> UpdateUserProfile(UserProfile userProfile)
        {
            var result = userDbContext.UserProfile.Update(userProfile);
            await userDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<UserProfile> GetProfileByOwnerGuid(string ownerGuid)
        {
            return await userDbContext.UserProfile.Where(p => p.OwnerGuid == ownerGuid).FirstOrDefaultAsync();
        }
    }
}
