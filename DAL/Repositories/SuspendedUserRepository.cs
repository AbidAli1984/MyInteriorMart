using BOL.IDENTITY;
using DAL.Repositories.Contracts;
using DAL.USER;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SuspendedUserRepository : ISuspendedUserRepository
    {
        private readonly UserDbContext userDbContext;
        public SuspendedUserRepository(UserDbContext userDbContext)
        {
            this.userDbContext = userDbContext;
        }
        public async Task<SuspendedUser> GetSuspendedUserById(string UserGuid)
        {
            return await userDbContext.SuspendedUser.FirstOrDefaultAsync(x => x.SuspendedTo.Contains(UserGuid) && x.Suspended == true);
        }

        public async Task<IEnumerable<SuspendedUser>> GetSuspendedUsers()
        {
            return await userDbContext.SuspendedUser.Where(x => x.Suspended == true).ToListAsync();
        }

        public async Task<IEnumerable<SuspendedUser>> GetUnSuspendedUsers()
        {
            return await userDbContext.SuspendedUser.Where(x => x.Suspended == false).ToListAsync();
        }

        public async Task SuspendUser(string SuspendedTo, string SuspendedBy, DateTime SuspendedDate, string ReasonForSuspending)
        {
            bool userPresent = await userDbContext.SuspendedUser.AnyAsync(x => x.SuspendedTo.Contains(SuspendedTo));

            if (userPresent == false && SuspendedTo != SuspendedBy)
            {
                SuspendedUser user = new SuspendedUser()
                {
                    SuspendedTo = SuspendedTo,
                    SuspendedBy = SuspendedBy,
                    SuspendedDate = SuspendedDate,
                    ReasonForSuspending = ReasonForSuspending,
                    Suspended = true,

                };

                await userDbContext.AddAsync(user);
                await userDbContext.SaveChangesAsync();
            }

            if (userPresent == true && SuspendedTo != SuspendedBy)
            {
                var user = await userDbContext.SuspendedUser.FirstOrDefaultAsync(i => i.SuspendedTo == SuspendedTo);

                if (user.Suspended == false)
                {
                    user.Suspended = true;
                    user.SuspendedBy = SuspendedBy;
                    user.SuspendedDate = SuspendedDate;
                    user.ReasonForSuspending = ReasonForSuspending;

                    userDbContext.Update(user);
                    await userDbContext.SaveChangesAsync();
                }
            }
        }

        public async Task UnSuspendUser(string UnsuspendedTo, string UnsuspendedBy, DateTime UnsuspendedDate, string ReasonForUnsuspending)
        {
            bool userPresent = await userDbContext.SuspendedUser.AnyAsync(x => x.SuspendedTo.Contains(UnsuspendedTo));

            if (userPresent == true && UnsuspendedTo != UnsuspendedBy)
            {
                var user = await userDbContext.SuspendedUser.FirstOrDefaultAsync(i => i.SuspendedTo == UnsuspendedTo);

                user.Suspended = false;
                user.UnsuspendedBy = UnsuspendedBy;
                user.UnsuspendedDate = UnsuspendedDate;
                user.ReasonForUnsuspending = ReasonForUnsuspending;

                userDbContext.Update(user);
                await userDbContext.SaveChangesAsync();
            }
        }
    }
}
