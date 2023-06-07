using DAL.LISTING;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOL.VIEWMODELS.Dashboards;
using DAL.SHARED;
using BAL.Category;
using DAL.CATEGORIES;
using BOL.AUDITTRAIL;
using BOL.LISTING;
using DAL.AUDIT;
using System.Security.Cryptography;

namespace BAL.Dashboard.History
{
    public class DashboardUserHistory : IDashboardUserHistory
    {
        private readonly ListingDbContext listingContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly CategoriesDbContext categoryManager;
        private readonly AuditDbContext auditContext;

        public DashboardUserHistory(ListingDbContext listingContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostEnvironment, CategoriesDbContext categoryManager, AuditDbContext auditContext)
        {
            this.listingContext = listingContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.hostEnvironment = hostEnvironment;
            this.categoryManager = categoryManager;
            this.auditContext = auditContext;
        }

        public async Task<IEnumerable<UserHistory>> GetRegisteredUsersAsync(int subtractDays)
        {
            // Shafi: Create date and subtract days
            DateTime date = DateTime.Now;
            date.AddDays(-subtractDays);
            // End:

            // Shafi: Testing Purpose
            // Commented Where(x => x.VisitDate < date) because userHistoryAudit not working
            var result = await auditContext.UserHistory/*.Where(x => x.VisitDate < date)*/.Where(x => x.UserGuid != "Anonymous").Take(30).OrderByDescending(x => x.HistoryID).ToListAsync();
            return result;
        }

        public async Task<int> CountRegisteredUsersAsync(int subtractDays)
        {
            // Shafi: Create date and subtract days
            DateTime date = DateTime.Now;
            date.AddDays(-subtractDays);
            // End:

            // Shafi: Testing Purpose
            // Commented Where(x => x.VisitDate < date) because userHistoryAudit not working
            var result = await auditContext.UserHistory/*.Where(x => x.VisitDate < date)*/.Where(x => x.UserGuid != "Anonymous").CountAsync();
            return result;
        }

        public async Task<IEnumerable<UserHistory>> GetAnonymousUserAsync(int subtractDays)
        {
            // Shafi: Create date and subtract days
            DateTime date = DateTime.Now;
            date.AddDays(-subtractDays);
            // End:

            // Shafi: Testing Purpose
            // Commented Where(x => x.VisitDate < date) because userHistoryAudit not working
            var result = await auditContext.UserHistory/*.Where(x => x.VisitDate < date)*/.Where(x => x.UserGuid == "Anonymous").Take(30).OrderByDescending(x => x.HistoryID).ToListAsync();
            return result;
        }

        public async Task<int> CountAnonymousUsersAsync(int subtractDays)
        {
            // Shafi: Create date and subtract days
            DateTime date = DateTime.Now;
            date.AddDays(-subtractDays);
            // End:

            var result = await auditContext.UserHistory/*.Where(x => x.VisitDate < date)*/.Where(x => x.UserGuid == "Anonymous").CountAsync();
            return result;
        }

        public async Task<IEnumerable<ListingLastUpdated>> UsersUpdatedListingAsync(int subtractDays)
        {
            // Shafi: Create date and subtract days
            DateTime date = DateTime.Now;
            date.AddDays(-subtractDays);
            // End:

            var result = await auditContext.ListingLastUpdated.Where(x => x.UpdatedDate < date).Take(30).OrderByDescending(x => x.LastUpdatedID).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ListingLikeDislike>> UsersLikedListingsAsync(int subtractDays)
        {
            // Shafi: Create date and subtract days
            DateTime date = DateTime.Now;
            date.AddDays(-subtractDays);
            // End:

            var result = await auditContext.ListingLikeDislike.Where(x => x.VisitDate < date).Where(x => x.Like == true).Take(30).OrderByDescending(x => x.LikeDislikeID).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Subscribes>> UsersSubscribedListingsAsync(int subtractDays)
        {
            // Shafi: Create date and subtract days
            DateTime date = DateTime.Now;
            date.AddDays(-subtractDays);
            // End:

            var result = await auditContext.Subscribes.Where(x => x.VisitDate < date).Where(x => x.Subscribe == true).Take(30).OrderByDescending(x => x.SubscribeID).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Bookmarks>> UsersBookmarkedListingsAsync(int subtractDays)
        {
            // Shafi: Create date and subtract days
            DateTime date = DateTime.Now;
            date.AddDays(-subtractDays);
            // End:

            var result = await auditContext.Bookmarks.Where(x => x.VisitDate < date).Where(x => x.Bookmark == true).Take(30).OrderByDescending(x => x.BookmarksID).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<UserHistory>> MostActiveUsersAsync(int subtractDays)
        {
            // Shafi:  Get all users
            var allUsers = await userManager.Users.ToListAsync();
            // End:

            // Shafi:  Get all orphan listings and count
            var mostActiveUsers = await auditContext.UserHistory.Where(l => allUsers.Any(u => u.Id.Contains(l.UserGuid))).Distinct().Take(30).ToListAsync();
            // End:

            // Shafi: Return
            return mostActiveUsers;
            // End:
        }

        public Task<IEnumerable<UserHistory>> MostInactiveUsersAsync(int subtractDays)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserManager<IdentityUser>>> GetUsersWithThreeOrMoreListingAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserManager<IdentityUser>>> GetUsersWithZeroListingAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserManager<IdentityUser>>> GetUsersWithMostReviewAsync()
        {
            throw new NotImplementedException();
        }
    }
}
