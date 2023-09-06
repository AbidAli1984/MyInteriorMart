﻿using BOL.AUDITTRAIL;
using BOL.VIEWMODELS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Contracts
{
    public interface IAuditService
    {
        Task AddAsync(object data);
        Task UpdateAsync(object data);

        Task<Bookmarks> GetBookmarkByListingAndUserId(int listingId, string userGuid);
        Task<ListingLikeDislike> GetLikeByListingAndUserId(int listingId, string userGuid);
        Task<Subscribes> GetSubscribeByListingAndUserId(int listingId, string userGuid);

        Task<bool> CheckIfUserSubscribedToListing(int listingId, string userGuid);
        Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid);
        Task<bool> CheckIfUserLikedListing(int listingId, string userGuid);

        Task<IList<LikeListingViewModel>> GetLikesByOwnerIdAsync(string userGuid);

        Task<IList<BookmarkListingViewModel>> GetBookmarksByOwnerIdAsync(string ownerId);
    }
}
