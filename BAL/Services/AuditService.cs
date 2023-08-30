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
            var listings = await _listingRepository.GetListingsByOwnerId(ownerId);
            return await GetLikes(listings);
        }

        private async Task<IList<LikeListingViewModel>> GetLikes(IEnumerable<Listing> listings)
        {
            if (listings.Count() <= 0)
                return null;

            IList<LikeListingViewModel> listLikes = new List<LikeListingViewModel>();

            foreach (var listing in listings)
            {
                var likes = await _auditRepository.GetLikesByListingId(listing.ListingID);
                if (likes == null)
                    continue;
                foreach (var like in likes)
                {
                    var profile = await _userProfileRepository.GetProfileByOwnerGuid(like.UserGuid);
                    LikeListingViewModel likeListingnVM = new LikeListingViewModel
                    {
                        LikeDislikeID = like.LikeDislikeID,
                        ListingID = like.ListingID,
                        OwnerGuid = like.UserGuid,
                        VisitDate = like.VisitDate.ToString("dd/MM/yyyy"),
                        Name = listing.CompanyName,
                        NameFirstLetter = listing.CompanyName[0].ToString(),
                        ListingUrl = listing.ListingURL,
                        BusinessCategory = listing.BusinessCategory
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

    }
}
