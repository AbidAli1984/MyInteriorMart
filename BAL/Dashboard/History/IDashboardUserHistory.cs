using System.Collections.Generic;
using System.Threading.Tasks;
using BOL.AUDITTRAIL;
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
        Task<IEnumerable<UserManager<IdentityUser>>> GetUsersWithThreeOrMoreListingAsync();
        Task<IEnumerable<UserManager<IdentityUser>>> GetUsersWithZeroListingAsync();
        Task<IEnumerable<UserManager<IdentityUser>>> GetUsersWithMostReviewAsync();
    }
}
