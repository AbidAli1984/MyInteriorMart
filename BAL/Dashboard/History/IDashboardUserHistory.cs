using System.Collections.Generic;
using System.Threading.Tasks;
using BOL.AUDITTRAIL;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace BAL.Dashboard.History
{
    public interface IDashboardUserHistory
    {
        Task<IEnumerable<UserHistory>> GetRegisteredUsersAsync(int subtractDays);
        Task<int> CountRegisteredUsersAsync(int subtractDays);
        Task<IEnumerable<UserHistory>> GetAnonymousUserAsync(int subtractDays);
        Task<int> CountAnonymousUsersAsync(int subtractDays);
        Task<IEnumerable<ListingLastUpdated>> UsersUpdatedListingAsync(int subtractDays);
        Task<IEnumerable<ListingLikeDislike>> UsersLikedListingsAsync(int subtractDays);
        Task<IEnumerable<Subscribes>> UsersSubscribedListingsAsync(int subtractDays);
        Task<IEnumerable<Bookmarks>> UsersBookmarkedListingsAsync(int subtractDays);
        Task<IEnumerable<UserHistory>> MostActiveUsersAsync(int subtractDays);
        Task<IEnumerable<UserHistory>> MostInactiveUsersAsync(int subtractDays);
        Task<IEnumerable<UserManager<ApplicationUser>>> GetUsersWithThreeOrMoreListingAsync();
        Task<IEnumerable<UserManager<ApplicationUser>>> GetUsersWithZeroListingAsync();
        Task<IEnumerable<UserManager<ApplicationUser>>> GetUsersWithMostReviewAsync();
    }
}
