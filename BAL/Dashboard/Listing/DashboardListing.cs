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

namespace BAL.Dashboard.Listing
{
    public class DashboardListing : IDashboardListing
    {
        private readonly ListingDbContext listingContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly CategoriesDbContext categoryManager;
        private readonly AuditDbContext auditContext;
        public DashboardListing(ListingDbContext listingContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment hostEnvironment, CategoriesDbContext categoryManager, AuditDbContext auditContext)
        {
            this.listingContext = listingContext;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.hostEnvironment = hostEnvironment;
            this.categoryManager = categoryManager;
            this.auditContext = auditContext;

        }

        public async Task<int> CountAllAsync()
        {
            var result = await listingContext.Listing.CountAsync();
            return result;
        }

        public Task<int> CountClaimedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountClaimListingRequestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountFreeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountListingVerificationRequestAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountOrphanAsync()
        {
            // Shafi: Get roles of Administrator, Accountant and Staff
            var administrator = await userManager.GetUsersInRoleAsync("Administrator");
            var accountant = await userManager.GetUsersInRoleAsync("Accountant");
            var staff = await userManager.GetUsersInRoleAsync("Staff");
            // End:

            // Shafi:  Concatenate
            var usersInRole = administrator.ToList().Concat(accountant.ToList()).Concat(staff.ToList()).Distinct().ToList();
            // End:

            // Shafi: Get all listings and count
            var listings = await listingContext.Listing.ToListAsync();
            var allListingCount = listings.Count();
            // End:

            // Shafi:  Get all orphan listings and count
            var orphanListings = listings.Where(l => usersInRole.Any(u => u.Id == l.OwnerGuid)).ToList();
            var result = orphanListings.Count();
            // End:

            // Shafi: 
            return result;
            // End:
        }

        public async Task<int> CountOwnedAsync()
        {
            // Shafi: Get roles of Administrator, Accountant and Staff
            var administrator = await userManager.GetUsersInRoleAsync("Administrator");
            var accountant = await userManager.GetUsersInRoleAsync("Accountant");
            var staff = await userManager.GetUsersInRoleAsync("Staff");
            // End:

            // Shafi:  Concatenate
            var usersInRole = administrator.ToList().Concat(accountant.ToList()).Concat(staff.ToList()).Distinct().ToList();
            // End:

            // Shafi: Get all listings and count
            var listings = await listingContext.Listing.ToListAsync();
            var allListingCount = listings.Count();
            // End:

            // Shafi:  Get all orphan listings and count
            var orphanListings = listings.Where(l => usersInRole.Any(u => u.Id == l.OwnerGuid)).ToList();
            var orphanListingsCount = orphanListings.Count();
            // End:

            // Shafi: Subtract orphan listings from all listings to get owned listings
            var result = allListingCount - orphanListingsCount;
            // End:

            // Shafi: 
            return result;
            // End:
        }

        public Task<int> CountPaidAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountUnverifiedAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountVerifiedAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountWithoutProfileImageAsync()
        {
            // Shafi: Get all listings and reviews
            var listings = await listingContext.Listing.Select(i => i.ListingID).ToListAsync();
            // End:

            // Shafi: Get profile image folder path
            var profileImageLocation = hostEnvironment.WebRootPath + "\\FileManager\\ProfileImages\\";
            DirectoryInfo profileImages = new DirectoryInfo(profileImageLocation);
            var profileImageCount = profileImages.EnumerateFiles().Count();
            // End:

            // Shafi: Subtract totalListing count by total profileImages
            var totalListing = listings.Count();
            var listingWithoutProfileImage = totalListing - profileImageCount;
            // End:

            return listingWithoutProfileImage;
        }

        public async Task<int> CountZeroReviewsAsync()
        {
            // Shafi: Get all listings and reviews
            var listings = await listingContext.Listing.Select(i => i.ListingID).ToListAsync();
            var reviews = await listingContext.Rating.Select(i => i.ListingID).ToListAsync();
            // End:

            // Shafi: Get count of listings with zero reviews
            var listingWithReviews = listings.Where(l => reviews.Any(r => r == l)).ToList().Count();
            // End:

            // Shafi: Subtract totalListing count by total listingWithReviews
            var totalListing = listings.Count();
            var zeroReviewsCount = totalListing - listingWithReviews;
            // End:

            return zeroReviewsCount;
        }

        // Shafi: Get all listing where month and year condition met
        public async Task<int> CountAllAsync(int month, int year)
        {
            var result = await listingContext.Listing.Where(l => l.CreatedDate.Month == month && l.CreatedDate.Year == year).CountAsync();
            return result;
        }
        // End:

        public async Task<int> CountOrphanAsync(int month, int year)
        {
            // Shafi: Get roles of Administrator, Accountant and Staff
            var administrator = await userManager.GetUsersInRoleAsync("Administrator");
            var accountant = await userManager.GetUsersInRoleAsync("Accountant");
            var staff = await userManager.GetUsersInRoleAsync("Staff");
            // End:

            // Shafi:  Concatenate
            var usersInRole = administrator.ToList().Concat(accountant.ToList()).Concat(staff.ToList()).Distinct().ToList();
            // End:

            // Shafi: Get all listings and count
            var listings = await listingContext.Listing.Where(l => l.CreatedDate.Month == month && l.CreatedDate.Year == year).ToListAsync();
            var allListingCount = listings.Count();
            // End:

            // Shafi:  Get all orphan listings and count
            var orphanListings = listings.Where(l => usersInRole.Any(u => u.Id == l.OwnerGuid)).Where(l => l.CreatedDate.Month == month && l.CreatedDate.Year == year).ToList();
            var result = orphanListings.Count();
            // End:

            // Shafi: 
            return result;
            // End:
        }

        public async Task<int> CountOwnedAsync(int month, int year)
        {
            // Shafi: Get roles of Administrator, Accountant and Staff
            var administrator = await userManager.GetUsersInRoleAsync("Administrator");
            var accountant = await userManager.GetUsersInRoleAsync("Accountant");
            var staff = await userManager.GetUsersInRoleAsync("Staff");
            // End:

            // Shafi:  Concatenate
            var usersInRole = administrator.ToList().Concat(accountant.ToList()).Concat(staff.ToList()).Distinct().ToList();
            // End:

            // Shafi: Get all listings and count
            var listings = await listingContext.Listing.Where(l => l.CreatedDate.Month == month && l.CreatedDate.Year == year).ToListAsync();
            var allListingCount = listings.Count();
            // End:

            // Shafi:  Get all orphan listings and count
            var orphanListings = listings.Where(l => usersInRole.Any(u => u.Id == l.OwnerGuid)).Where(l => l.CreatedDate.Month == month && l.CreatedDate.Year == year).ToList();
            var orphanListingsCount = orphanListings.Count();
            // End:

            // Shafi: Subtract orphan listings from all listings to get owned listings
            var result = allListingCount - orphanListingsCount;
            // End:

            // Shafi: 
            return result;
            // End:
        }

        public async Task<DashboardListingViewModel> GraphAsync()
        {
            // Shafi: Create current date
            DateTime date = DateTime.Now;
            // End:

            // Shafi: First details
            var FirstDate = date.AddMonths(0);
            string First = FirstDate.ToString("MMM");
            var FirstAll = await CountAllAsync(FirstDate.Month, FirstDate.Year);
            var FirstOrphan = await CountOrphanAsync(FirstDate.Month, FirstDate.Year);
            var FirstOwned = await CountOwnedAsync(FirstDate.Month, FirstDate.Year);
            // End:

            // Shafi: Second details
            var SecondDate = date.AddMonths(-1);
            string Second = SecondDate.ToString("MMM");
            var SecondAll = await CountAllAsync(SecondDate.Month, SecondDate.Year);
            var SecondOrphan = await CountOrphanAsync(SecondDate.Month, SecondDate.Year);
            var SecondOwned = await CountOwnedAsync(SecondDate.Month, SecondDate.Year);
            // End:

            // Shafi: Third details
            var ThirdDate = date.AddMonths(-2);
            string Third = ThirdDate.ToString("MMM");
            var ThirdAll = await CountAllAsync(ThirdDate.Month, ThirdDate.Year);
            var ThirdOrphan = await CountOrphanAsync(ThirdDate.Month, ThirdDate.Year);
            var ThirdOwned = await CountOwnedAsync(ThirdDate.Month, ThirdDate.Year);
            // End:

            // Shafi: Fourth details
            var FourthDate = date.AddMonths(-3);
            string Fourth = FourthDate.ToString("MMM");
            var FourthAll = await CountAllAsync(FourthDate.Month, FourthDate.Year);
            var FourthOrphan = await CountOrphanAsync(FourthDate.Month, FourthDate.Year);
            var FourthOwned = await CountOwnedAsync(FourthDate.Month, FourthDate.Year);
            // End:

            // Shafi: Fifth details
            var FifthDate = date.AddMonths(-4);
            string Fifth = FifthDate.ToString("MMM");
            var FifthAll = await CountAllAsync(FifthDate.Month, FifthDate.Year);
            var FifthOrphan = await CountOrphanAsync(FifthDate.Month, FifthDate.Year);
            var FifthOwned = await CountOwnedAsync(FifthDate.Month, FifthDate.Year);
            // End:

            // Shafi: Sixth details
            var SixthDate = date.AddMonths(-5);
            string Sixth = SixthDate.ToString("MMM");
            var SixthAll = await CountAllAsync(SixthDate.Month, SixthDate.Year);
            var SixthOrphan = await CountOrphanAsync(SixthDate.Month, SixthDate.Year);
            var SixthOwned = await CountOwnedAsync(SixthDate.Month, SixthDate.Year);
            // End:

            // Shafi: Seventh details
            var SeventhDate = date.AddMonths(-6);
            string Seventh = SeventhDate.ToString("MMM");
            var SeventhAll = await CountAllAsync(SeventhDate.Month, SeventhDate.Year);
            var SeventhOrphan = await CountOrphanAsync(SeventhDate.Month, SeventhDate.Year);
            var SeventhOwned = await CountOwnedAsync(SeventhDate.Month, SeventhDate.Year);
            // End:

            // Shafi: Eight details
            var EighthDate = date.AddMonths(-7);
            string Eight = EighthDate.ToString("MMM");
            var EightAll = await CountAllAsync(EighthDate.Month, EighthDate.Year);
            var EightOrphan = await CountOrphanAsync(EighthDate.Month, EighthDate.Year);
            var EightOwned = await CountOwnedAsync(EighthDate.Month, EighthDate.Year);
            // End:

            // Shafi: Ninth details
            var NinthDate = date.AddMonths(-8);
            string Ninth = NinthDate.ToString("MMM");
            var NinthAll = await CountAllAsync(NinthDate.Month, NinthDate.Year);
            var NinthOrphan = await CountOrphanAsync(NinthDate.Month, NinthDate.Year);
            var NinthOwned = await CountOwnedAsync(NinthDate.Month, NinthDate.Year);
            // End:

            // Shafi: Tenth details
            var TenthDate = date.AddMonths(-9);
            string Tenth = TenthDate.ToString("MMM");
            var TenthAll = await CountAllAsync(TenthDate.Month, TenthDate.Year);
            var TenthOrphan = await CountOrphanAsync(TenthDate.Month, TenthDate.Year);
            var TenthOwned = await CountOwnedAsync(TenthDate.Month, TenthDate.Year);
            // End:

            // Shafi: Eleventh details
            var EleventhDate = date.AddMonths(-10);
            string Eleventh = EleventhDate.ToString("MMM");
            var EleventhAll = await CountAllAsync(EleventhDate.Month, EleventhDate.Year);
            var EleventhOrphan = await CountOrphanAsync(EleventhDate.Month, EleventhDate.Year);
            var EleventhOwned = await CountOwnedAsync(EleventhDate.Month, EleventhDate.Year);
            // End:

            // Shafi: Twelth details
            var TwelthDate = date.AddMonths(-11);
            string Twelth = TwelthDate.ToString("MMM");
            var TwelthAll = await CountAllAsync(TwelthDate.Month, TwelthDate.Year);
            var TwelthOrphan = await CountOrphanAsync(TwelthDate.Month, TwelthDate.Year);
            var TwelthOwned = await CountOwnedAsync(TwelthDate.Month, TwelthDate.Year);
            // End:

            // Shafi: Create object of DashboardListingViewModel
            DashboardListingViewModel graph = new DashboardListingViewModel()
            {
                // Shafi: First months data
                First = First,
                FirstAll = FirstAll,
                FirstOrphan = FirstOrphan,
                FirstOwned = FirstOwned,
                FirstViews = 0,
                // End:

                // Shafi: Second month's data
                Second = Second,
                SecondAll = SecondAll,
                SecondOrphan = SecondOrphan,
                SecondOwned = SecondOwned,
                SecondViews = 0,
                // End:

                // Shafi: Third month's data
                Third = Third,
                ThirdAll = ThirdAll,
                ThirdOrphan = ThirdOrphan,
                ThirdOwned = ThirdOwned,
                ThirdViews = 0,
                // End:

                // Shafi: Fourth month's data
                Fourth = Fourth,
                FourthAll = FourthAll,
                FourthOrphan = FourthOrphan,
                FourthOwned = FourthOwned,
                FourthViews = 0,
                // End:

                // Shafi: Fifth month's data
                Fifth = Fifth,
                FifthAll = FifthAll,
                FifthOrphan = FifthOrphan,
                FifthOwned = FifthOwned,
                FifthViews = 0,
                // End:

                // Shafi: Sixth month's data
                Sixth = Sixth,
                SixthAll = SixthAll,
                SixthOrphan = SixthOrphan,
                SixthOwned = SixthOwned,
                SixthViews = 0,
                // End:

                // Shafi: Seventh month's data
                Seventh = Seventh,
                SeventhAll = SeventhAll,
                SeventhOrphan = SeventhOrphan,
                SeventhOwned = SeventhOwned,
                SeventhViews = 0,
                // End:

                // Shafi: Eigth month's data
                Eigth = Eight,
                EigthAll = EightAll,
                EigthOrphan = EightOrphan,
                EigthOwned = EightOwned,
                EigthViews = 0,
                // End:

                // Shafi: Ninth month's data
                Ninth = Ninth,
                NinthAll = NinthAll,
                NinthOrphan = NinthOrphan,
                NinthOwned = NinthOwned,
                NinthViews = 0,
                // End:

                // Shafi: Tenth month's data
                Tenth = Tenth,
                TenthAll = TenthAll,
                TenthOrphan = TenthOrphan,
                TenthOwned = TenthOwned,
                TenthViews = 0,
                // End:

                // Shafi: Eleventh month's data
                Eleventh = Eleventh,
                EleventhAll = EleventhAll,
                EleventhOrphan = EleventhOrphan,
                EleventhOwned = EleventhOrphan,
                EleventhViews = 0,
                // End:

                // Shafi: Twelth month's data
                Twelth = Twelth,
                TwelthAll = TwelthAll,
                TwelthOrphan = TwelthOrphan,
                TwelthOwned = TwelthOwned,
                TwelthViews = 0
                // End:

            };
            // End:

            // Shafi: Return
            return graph;
            // End:
        }

        public async Task<IList<DashboardListingByFirstCatViewModel>> GetListingCountByFirstCatAsync()
        {
            var firstCategories = await categoryManager.FirstCategory.ToListAsync();

            IList<DashboardListingByFirstCatViewModel> model = new List<DashboardListingByFirstCatViewModel>();
            foreach (var item in firstCategories)
            {
                var count = await listingContext.Categories.Where(i => i.FirstCategoryID == item.FirstCategoryID).CountAsync();
                var category = new DashboardListingByFirstCatViewModel()
                {
                    FirstCategory = item.Name,
                    FirstCategoryUrl = item.URL,
                    FirstCategoryId = item.FirstCategoryID,
                    ListingCount = count
                };

                model.Add(category);
            }

            return model.ToList();
        }

        public async Task<IList<DahboardListingHighestCount>> GetHighestLikesAsync()
        {
            var allListings = await listingContext.Listing.ToListAsync();
            var allLikes = await auditContext.ListingLikeDislike.ToListAsync();

            var result = (from listing in allListings
                          from like in allLikes
                          where listing.ListingID == like.ListingID
                          group like by listing into mostLikedListings
                          select new DahboardListingHighestCount
                          {
                              Listing = mostLikedListings.Key,
                              Count = mostLikedListings.Count()
                          })
                          .OrderByDescending(x => x.Count)
                          .Distinct()
                          .Take(10)
                          .ToList();
            return result;
        }

        public async Task<IList<DahboardListingHighestCount>> GetHighestSubscribesAsync()
        {
            var allListings = await listingContext.Listing.ToListAsync();
            var allSubscribed = await auditContext.Subscribes.ToListAsync();

            var result = (from listing in allListings
                          from subscribe in allSubscribed
                          where listing.ListingID == subscribe.ListingID
                          group subscribe by listing into mostSubscribed
                          select new DahboardListingHighestCount
                          {
                              Listing = mostSubscribed.Key,
                              Count = mostSubscribed.Count()
                          })
                          .OrderByDescending(x => x.Count)
                          .Distinct()
                          .Take(10)
                          .ToList();
            return result;
        }

        public async Task<IList<DahboardListingHighestCount>> GetHighestBookmarksAsync()
        {
            var allListings = await listingContext.Listing.ToListAsync();
            var allBookmarks = await auditContext.Bookmarks.ToListAsync();

            var result = (from listing in allListings
                          from bookmarks in allBookmarks
                          where listing.ListingID == bookmarks.ListingID
                          group bookmarks by listing into mostBookmakred
                          select new DahboardListingHighestCount
                          {
                              Listing = mostBookmakred.Key,
                              Count = mostBookmakred.Count()
                          })
                          .OrderByDescending(x => x.Count)
                          .Distinct()
                          .Take(10)
                          .ToList();
            return result;
        }

        public async Task<IList<DahboardListingHighestCount>> GetHighestRatingAsync()
        {
            var allListings = await listingContext.Listing.ToListAsync();
            var allRatings = await listingContext.Rating.ToListAsync();

            var result = (from listing in allListings
                          from ratings in allRatings
                          where listing.ListingID == ratings.ListingID
                          group ratings by listing into mostRated
                          select new DahboardListingHighestCount
                          {
                              Listing = mostRated.Key,
                              Count = mostRated.Count()
                          })
                          .OrderByDescending(x => x.Count)
                          .Distinct()
                          .Take(10)
                          .ToList();
            return result;
        }

        public async Task<IList<DahboardListingHighestCount>> GetHighestListingViewsAsync()
        {
            var allListings = await listingContext.Listing.ToListAsync();
            var allViews = await listingContext.ListingViews.ToListAsync();

            var result = (from listing in allListings
                          from views in allViews
                          where listing.ListingID == views.ListingID
                          group views by listing into mostViewed
                          select new DahboardListingHighestCount
                          {
                              Listing = mostViewed.Key,
                              Count = mostViewed.Count()
                          })
                          .OrderByDescending(x => x.Count)
                          .Distinct()
                          .Take(10)
                          .ToList();
            return result;
        }

        public async Task<IList<BOL.LISTING.Listing>> GetRecentCretedListings(int GetRecords)
        {
            var result = await listingContext.Listing.OrderByDescending(i => i.ListingID).Take(GetRecords).ToListAsync();
            return result;
        }

        public async Task<IList<DashboardListingsWithoutWebsite>> GetListingsWithoutWebsite(int GetRecords)
        {
            var result = await (from listing in listingContext.Listing
                          join communcation in listingContext.Communication
                          on listing.ListingID equals communcation.ListingID
                          where communcation.Website == ""
                          select new DashboardListingsWithoutWebsite {
                              Listing = listing,
                              Communication = communcation
                          })
                          .OrderByDescending(x => x.Listing.ListingID)
                          .Take(GetRecords)
                          .ToListAsync();
            return result;

        }
    }
}
