using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IAuditService
    {
        Task<bool> CheckIfUserSubscribedToListing(int listingId, string userGuid);
        Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid);
        Task<bool> CheckIfUserLikedListing(int listingId, string userGuid);
    }
}
