using BAL.Services.Contracts;
using BOL.AUDITTRAIL;
using BOL.LISTING;
using BOL.VIEWMODELS;
using DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class AuditService : IAuditService
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IListingRepository _listingRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        public AuditService(IAuditRepository auditRepository, IListingRepository listingRepository, IUserProfileRepository userProfileRepository)
        {
            _auditRepository = auditRepository;
            _listingRepository = listingRepository;
            _userProfileRepository = userProfileRepository;
        }

        public async Task AddAsync(object data)
        {
            await _auditRepository.AddAsync(data);
        }

        public async Task UpdateAsync(object data)
        {
            await _auditRepository.UpdateAsync(data);
        }

        public async Task<Bookmarks> GetBookmarkByListingAndUserId(int listingId, string userGuid)
        {
            return await _auditRepository.GetBookmarkByListingAndUserId(listingId, userGuid);
        }

        public async Task<ListingLikeDislike> GetLikeByListingAndUserId(int listingId, string userGuid)
        {
            return await _auditRepository.GetLikeByListingAndUserId(listingId, userGuid);
        }

        public async Task<Subscribes> GetSubscribeByListingAndUserId(int listingId, string userGuid)
        {
            return await _auditRepository.GetSubscribeByListingAndUserId(listingId, userGuid);
        }

        public async Task<bool> CheckIfUserBookmarkedListing(int listingId, string userGuid)
        {
            var bookmark = await GetBookmarkByListingAndUserId(listingId, userGuid);
            return bookmark != null && bookmark.Bookmark;
        }

        public async Task<bool> CheckIfUserLikedListing(int listingId, string userGuid)
        {
            var like = await GetLikeByListingAndUserId(listingId, userGuid);
            return like != null && like.Like;
        }

        public async Task<bool> CheckIfUserSubscribedToListing(int listingId, string userGuid)
        {
            var subscribe = await GetSubscribeByListingAndUserId(listingId, userGuid);
            return subscribe != null && subscribe.Subscribe;
        }

        public async Task<IList<LikeListingViewModel>> GetLikesByOwnerIdAsync(string ownerId)
        {
            var listing = await _listingRepository.GetListingByOwnerId(ownerId);
            return await GetLikes(listing);
        }

        private async Task<IList<LikeListingViewModel>> GetLikes(Listing listing)
        {
            if (listing == null)
                return null;

            IList<LikeListingViewModel> listLikes = new List<LikeListingViewModel>();

            var likes = await _auditRepository.GetLikesByListingId(listing.ListingID);
            if (likes != null)
            {
                foreach (var like in likes)
                {
                    var profile = await _userProfileRepository.GetProfileByOwnerGuid(like.UserGuid);
                    LikeListingViewModel likeListingnVM = new LikeListingViewModel
                    {
                        VisitDate = like.VisitDate.ToString(Constants.dateFormat1),
                        CompanyName = listing.CompanyName,
                    };

                    if (profile != null)
                    {
                        likeListingnVM.UserName = profile.Name;
                        likeListingnVM.UserImage = string.IsNullOrWhiteSpace(profile.ImageUrl) ? "resources/img/icon/profile.svg" : profile.ImageUrl;
                    }

                    listLikes.Add(likeListingnVM);
                }
            }
            return listLikes;
        }

        public async Task<IList<BookmarkListingViewModel>> GetBookmarksByOwnerIdAsync(string ownerId)
        {
            var listing = await _listingRepository.GetListingByOwnerId(ownerId);
            return await GetBookmarks(listing);
        }

        private async Task<IList<BookmarkListingViewModel>> GetBookmarks(Listing listing)
        {
            if (listing == null)
                return null;

            IList<BookmarkListingViewModel> listBookmarks = new List<BookmarkListingViewModel>();

            var bookmarks = await _auditRepository.GetBookmarksByListingId(listing.ListingID);
            if (bookmarks != null)
            {
                foreach (var bookmark in bookmarks)
                {
                    var profile = await _userProfileRepository.GetProfileByOwnerGuid(bookmark.UserGuid);
                    BookmarkListingViewModel bookmarListingnVM = new BookmarkListingViewModel
                    {
                        VisitDate = bookmark.VisitDate.ToString(Constants.dateFormat1),
                        CompanyName = listing.CompanyName
                    };

                    if (profile != null)
                    {
                        bookmarListingnVM.UserName = profile.Name;
                        bookmarListingnVM.UserImage = string.IsNullOrWhiteSpace(profile.ImageUrl) ? "resources/img/icon/profile.svg" : profile.ImageUrl;
                    }

                    listBookmarks.Add(bookmarListingnVM);
                }
            }
            return listBookmarks;
        }

        public async Task<IList<SubscribeListingViewModel>> GetSubscribesByOwnerIdAsync(string ownerId)
        {
            var listing = await _listingRepository.GetListingByOwnerId(ownerId);
            return await GetSubscribes(listing);
        }

        private async Task<IList<SubscribeListingViewModel>> GetSubscribes(Listing listing)
        {
            if (listing == null)
                return null;

            IList<SubscribeListingViewModel> listSubscribes = new List<SubscribeListingViewModel>();

            var subscribes = await _auditRepository.GetSubscriberByListingId(listing.ListingID);
            if (subscribes != null)
            {
                foreach (var subscribe in subscribes)
                {
                    var profile = await _userProfileRepository.GetProfileByOwnerGuid(subscribe.UserGuid);
                    SubscribeListingViewModel subscribeListingnVM = new SubscribeListingViewModel
                    {
                        VisitDate = subscribe.VisitDate.ToString(Constants.dateFormat1),
                        CompanyName = listing.CompanyName
                    };

                    if (profile != null)
                    {
                        subscribeListingnVM.UserName = profile.Name;
                        subscribeListingnVM.UserImage = string.IsNullOrWhiteSpace(profile.ImageUrl) ? "resources/img/icon/profile.svg" : profile.ImageUrl;
                    }

                    listSubscribes.Add(subscribeListingnVM);
                }
            }
            return listSubscribes;
        }

    }
}
