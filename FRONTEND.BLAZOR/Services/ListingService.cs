using BOL.AUDITTRAIL;
using BOL.LISTING;
using BOL.PLAN;
using DAL.BILLING;
using DAL.LISTING;
using DAL.SHARED;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.AUDIT;
using BOL.VIEWMODELS.Dashboards;
using System.IO;
using System.Globalization;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using FRONTEND.BLAZOR.Data;
using DAL.Models;

namespace FRONTEND.BLAZOR.Services
{
    public class ListingService_bak : IListingService_bak
    {
        private readonly ListingDbContext listingContext;
        private readonly BillingDbContext billingContext;
        private readonly SharedDbContext sharedManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment webHost;
        private readonly AuditDbContext auditContext;
        private readonly IHistoryAudit historyAudit;
        private readonly IHttpContextAccessor httpConAccess;

        public ListingService_bak(ListingDbContext listingContext, UserManager<ApplicationUser> userManager, SharedDbContext sharedManager, BillingDbContext billingContext, IWebHostEnvironment webHost, AuditDbContext auditContext, IHistoryAudit historyAudit, IHttpContextAccessor httpConAccess)
        {
            this.listingContext = listingContext;
            this.userManager = userManager;
            this.sharedManager = sharedManager;
            this.billingContext = billingContext;
            this.webHost = webHost;
            this.auditContext = auditContext;
            this.httpConAccess = httpConAccess;
        }

        // End:


        // Shafi: Check if business is open or close
        
        public async Task<int> CountViewsAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await listingContext.ListingViews.Where(x => x.Date.Day == date.Day && x.Date.Month == date.Month && x.Date.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }

        public async Task<int> CountReviewAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await listingContext.Rating.Where(x => x.Date.Day == date.Day && x.Date.Month == x.Date.Month && x.Date.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }

        public async Task<int> CountLikesAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await auditContext.ListingLikeDislike.Where(x => x.VisitDate.Day == date.Day && x.VisitDate.Month == x.VisitDate.Month && x.VisitDate.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }
        // End:

        // Shafi: Get Plans
        public async Task<IEnumerable<Plan>> GetPlansAsync()
        {
            var plans = await billingContext.Plan.ToListAsync();
            return plans;
        }
        // End:

        // Shafi: Function to count views and get day name
        public string GetDayName(int days)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            return date.ToString("d ddd");
        }

        public async Task<bool> CheckIfUserLikedListing(int listingId, string userGuid)
        {
            var liked = await auditContext.ListingLikeDislike.Where(i => i.ListingID == listingId && i.UserGuid == userGuid).AnyAsync();

            return liked == true;
        }

        // Begin: Listing Subscribe
        public async Task<int> CountSubscribesAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await auditContext.Subscribes.Where(x => x.VisitDate.Day == date.Day && x.VisitDate.Month == x.VisitDate.Month && x.VisitDate.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }
        // End: Listing Subscribe
        // End:

        public async Task<DashboardListingLast30DaysViews> GetLast30DaysListingViewsAsync(int ListingID)
        {
            var count1 = await CountViewsAsync(0, ListingID);
            var count2 = await CountViewsAsync(1, ListingID);
            var count3 = await CountViewsAsync(2, ListingID);
            var count4 = await CountViewsAsync(3, ListingID);
            var count5 = await CountViewsAsync(4, ListingID);
            var count6 = await CountViewsAsync(5, ListingID);
            var count7 = await CountViewsAsync(6, ListingID);
            var count8 = await CountViewsAsync(7, ListingID);
            var count9 = await CountViewsAsync(8, ListingID);
            var count10 = await CountViewsAsync(9, ListingID);
            var count11 = await CountViewsAsync(10, ListingID);
            var count12 = await CountViewsAsync(11, ListingID);
            var count13 = await CountViewsAsync(12, ListingID);
            var count14 = await CountViewsAsync(13, ListingID);
            var count15 = await CountViewsAsync(14, ListingID);
            var count16 = await CountViewsAsync(15, ListingID);
            var count17 = await CountViewsAsync(16, ListingID);
            var count18 = await CountViewsAsync(17, ListingID);
            var count19 = await CountViewsAsync(18, ListingID);
            var count20 = await CountViewsAsync(19, ListingID);
            var count21 = await CountViewsAsync(20, ListingID);
            var count22 = await CountViewsAsync(21, ListingID);
            var count23 = await CountViewsAsync(22, ListingID);
            var count24 = await CountViewsAsync(23, ListingID);
            var count25 = await CountViewsAsync(24, ListingID);
            var count26 = await CountViewsAsync(25, ListingID);
            var count27 = await CountViewsAsync(26, ListingID);
            var count28 = await CountViewsAsync(27, ListingID);
            var count29 = await CountViewsAsync(28, ListingID);
            var count30 = await CountViewsAsync(29, ListingID);
            DashboardListingLast30DaysViews view = new DashboardListingLast30DaysViews()
            {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return view;
        }

        public async Task<DashboardListingLast30DaysLikes> GetLast30DaysListingLikeAsync(int ListingID)
        {
            var count1 = await CountLikesAsync(0, ListingID);
            var count2 = await CountLikesAsync(1, ListingID);
            var count3 = await CountLikesAsync(2, ListingID);
            var count4 = await CountLikesAsync(3, ListingID);
            var count5 = await CountLikesAsync(4, ListingID);
            var count6 = await CountLikesAsync(5, ListingID);
            var count7 = await CountLikesAsync(6, ListingID);
            var count8 = await CountLikesAsync(7, ListingID);
            var count9 = await CountLikesAsync(8, ListingID);
            var count10 = await CountLikesAsync(9, ListingID);
            var count11 = await CountLikesAsync(10, ListingID);
            var count12 = await CountLikesAsync(11, ListingID);
            var count13 = await CountLikesAsync(12, ListingID);
            var count14 = await CountLikesAsync(13, ListingID);
            var count15 = await CountLikesAsync(14, ListingID);
            var count16 = await CountLikesAsync(15, ListingID);
            var count17 = await CountLikesAsync(16, ListingID);
            var count18 = await CountLikesAsync(17, ListingID);
            var count19 = await CountLikesAsync(18, ListingID);
            var count20 = await CountLikesAsync(19, ListingID);
            var count21 = await CountLikesAsync(20, ListingID);
            var count22 = await CountLikesAsync(21, ListingID);
            var count23 = await CountLikesAsync(22, ListingID);
            var count24 = await CountLikesAsync(23, ListingID);
            var count25 = await CountLikesAsync(24, ListingID);
            var count26 = await CountLikesAsync(25, ListingID);
            var count27 = await CountLikesAsync(26, ListingID);
            var count28 = await CountLikesAsync(27, ListingID);
            var count29 = await CountLikesAsync(28, ListingID);
            var count30 = await CountLikesAsync(29, ListingID);
            DashboardListingLast30DaysLikes likes = new DashboardListingLast30DaysLikes()
            {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return likes;
        }

        public async Task<DashboardListingLast30DaysSubscribes> GetLast30DaysListingSubscribesAsync(int ListingID)
        {
            var count1 = await CountSubscribesAsync(0, ListingID);
            var count2 = await CountSubscribesAsync(1, ListingID);
            var count3 = await CountSubscribesAsync(2, ListingID);
            var count4 = await CountSubscribesAsync(3, ListingID);
            var count5 = await CountSubscribesAsync(4, ListingID);
            var count6 = await CountSubscribesAsync(5, ListingID);
            var count7 = await CountSubscribesAsync(6, ListingID);
            var count8 = await CountSubscribesAsync(7, ListingID);
            var count9 = await CountSubscribesAsync(8, ListingID);
            var count10 = await CountSubscribesAsync(9, ListingID);
            var count11 = await CountSubscribesAsync(10, ListingID);
            var count12 = await CountSubscribesAsync(11, ListingID);
            var count13 = await CountSubscribesAsync(12, ListingID);
            var count14 = await CountSubscribesAsync(13, ListingID);
            var count15 = await CountSubscribesAsync(14, ListingID);
            var count16 = await CountSubscribesAsync(15, ListingID);
            var count17 = await CountSubscribesAsync(16, ListingID);
            var count18 = await CountSubscribesAsync(17, ListingID);
            var count19 = await CountSubscribesAsync(18, ListingID);
            var count20 = await CountSubscribesAsync(19, ListingID);
            var count21 = await CountSubscribesAsync(20, ListingID);
            var count22 = await CountSubscribesAsync(21, ListingID);
            var count23 = await CountSubscribesAsync(22, ListingID);
            var count24 = await CountSubscribesAsync(23, ListingID);
            var count25 = await CountSubscribesAsync(24, ListingID);
            var count26 = await CountSubscribesAsync(25, ListingID);
            var count27 = await CountSubscribesAsync(26, ListingID);
            var count28 = await CountSubscribesAsync(27, ListingID);
            var count29 = await CountSubscribesAsync(28, ListingID);
            var count30 = await CountSubscribesAsync(29, ListingID);
            DashboardListingLast30DaysSubscribes subscribes = new DashboardListingLast30DaysSubscribes()
            {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return subscribes;
        }

        // Begin: Bookmark  
        public async Task<int> CountListingBookmarkAsync(int ListingID)
        {
            var count = await auditContext.Bookmarks
                .Where(r => r.ListingID == ListingID)
                .Where(r => r.Bookmark == true)
                .CountAsync();
            return count;
        }

        public async Task<int> CountBookmarksAsync(int days, int ListingId)
        {
            DateTime date = DateTime.Now.AddDays(-days);
            var result = await auditContext.Bookmarks.Where(x => x.VisitDate.Day == date.Day && x.VisitDate.Month == x.VisitDate.Month && x.VisitDate.Year == date.Year && x.ListingID == ListingId).CountAsync();
            return result;
        }

        public async Task Bookmark(int? ListingID, string Bookmark, string UserGuid)
        {

            // Shafi: Get user details
            ApplicationUser user = await userManager.FindByIdAsync(UserGuid);
            string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];
            string UserAgent = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
            string Email = user.Email;
            string Mobile = user.PhoneNumber;
            IList<string> userInRoleName = await userManager.GetRolesAsync(user);
            string UserRole = userInRoleName.FirstOrDefault();
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            // End:

            if (Bookmark != null && ListingID != null)
            {
                // Shafi: Check if record already exists
                var bookmarkExists = await auditContext.Bookmarks.Where(l => l.ListingID == ListingID.Value && l.UserGuid == UserGuid).FirstOrDefaultAsync();

                if (bookmarkExists == null)
                {
                    // Shafi: If record does not exisits & Bookmark == Bookmark then [CREATE NEW RECORD]
                    if (await historyAudit.ListingBookmarkByUser(ListingID.Value, UserGuid) == false && Bookmark == "Bookmark")
                    {
                        await historyAudit.CreateBookmarkAsync(ListingID.Value, UserGuid, Email, Mobile, RemoteIpAddress, UserRole, timeZoneDate.ToString("dd/MM/yyyy"), timeZoneDate.ToString("hh:mm:ss tt"), UserAgent, true);
                    }
                }

                if (bookmarkExists != null)
                {
                    // Shafi: If record exisits & Bookmark == Remove-Bookmark then update value of Bookmark to [FALSE] of existing record
                    if (await historyAudit.ListingBookmarkByUser(ListingID.Value, UserGuid) == true && Bookmark == "Remove-Bookmark")
                    {
                        await historyAudit.EditBookmarkAsync(ListingID.Value, UserGuid, timeZoneDate.ToString("dd/MM/yyyy"), timeZoneDate.ToString("hh:mm:ss tt"), false);
                    }

                    // Shafi: If record exisits & Bookmark == Remove-Bookmark then update value of Bookmark to [TRUE] of existing record
                    if (await historyAudit.ListingBookmarkByUser(ListingID.Value, UserGuid) == false && Bookmark == "Bookmark")
                    {
                        await historyAudit.EditBookmarkAsync(ListingID.Value, UserGuid, timeZoneDate.ToString("dd/MM/yyyy"), timeZoneDate.ToString("hh:mm:ss tt"), true);
                    }
                }
            }
        }
        // End: Bookmark

        public async Task<DashboardListingLast30DaysBookmarks> GetLast30DaysListingBookmarksAsync(int ListingID)
        {
            var count1 = await CountBookmarksAsync(0, ListingID);
            var count2 = await CountBookmarksAsync(1, ListingID);
            var count3 = await CountBookmarksAsync(2, ListingID);
            var count4 = await CountBookmarksAsync(3, ListingID);
            var count5 = await CountBookmarksAsync(4, ListingID);
            var count6 = await CountBookmarksAsync(5, ListingID);
            var count7 = await CountBookmarksAsync(6, ListingID);
            var count8 = await CountBookmarksAsync(7, ListingID);
            var count9 = await CountBookmarksAsync(8, ListingID);
            var count10 = await CountBookmarksAsync(9, ListingID);
            var count11 = await CountBookmarksAsync(10, ListingID);
            var count12 = await CountBookmarksAsync(11, ListingID);
            var count13 = await CountBookmarksAsync(12, ListingID);
            var count14 = await CountBookmarksAsync(13, ListingID);
            var count15 = await CountBookmarksAsync(14, ListingID);
            var count16 = await CountBookmarksAsync(15, ListingID);
            var count17 = await CountBookmarksAsync(16, ListingID);
            var count18 = await CountBookmarksAsync(17, ListingID);
            var count19 = await CountBookmarksAsync(18, ListingID);
            var count20 = await CountBookmarksAsync(19, ListingID);
            var count21 = await CountBookmarksAsync(20, ListingID);
            var count22 = await CountBookmarksAsync(21, ListingID);
            var count23 = await CountBookmarksAsync(22, ListingID);
            var count24 = await CountBookmarksAsync(23, ListingID);
            var count25 = await CountBookmarksAsync(24, ListingID);
            var count26 = await CountBookmarksAsync(25, ListingID);
            var count27 = await CountBookmarksAsync(26, ListingID);
            var count28 = await CountBookmarksAsync(27, ListingID);
            var count29 = await CountBookmarksAsync(28, ListingID);
            var count30 = await CountBookmarksAsync(29, ListingID);
            DashboardListingLast30DaysBookmarks bookmarks = new DashboardListingLast30DaysBookmarks()
            {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return bookmarks;
        }

        public async Task<DashboardListingLast30DaysReviews> GetLast30DaysListingReviewsAsync(int ListingID)
        {
            var count1 = await CountReviewAsync(0, ListingID);
            var count2 = await CountReviewAsync(1, ListingID);
            var count3 = await CountReviewAsync(2, ListingID);
            var count4 = await CountReviewAsync(3, ListingID);
            var count5 = await CountReviewAsync(4, ListingID);
            var count6 = await CountReviewAsync(5, ListingID);
            var count7 = await CountReviewAsync(6, ListingID);
            var count8 = await CountReviewAsync(7, ListingID);
            var count9 = await CountReviewAsync(8, ListingID);
            var count10 = await CountReviewAsync(9, ListingID);
            var count11 = await CountReviewAsync(10, ListingID);
            var count12 = await CountReviewAsync(11, ListingID);
            var count13 = await CountReviewAsync(12, ListingID);
            var count14 = await CountReviewAsync(13, ListingID);
            var count15 = await CountReviewAsync(14, ListingID);
            var count16 = await CountReviewAsync(15, ListingID);
            var count17 = await CountReviewAsync(16, ListingID);
            var count18 = await CountReviewAsync(17, ListingID);
            var count19 = await CountReviewAsync(18, ListingID);
            var count20 = await CountReviewAsync(19, ListingID);
            var count21 = await CountReviewAsync(20, ListingID);
            var count22 = await CountReviewAsync(21, ListingID);
            var count23 = await CountReviewAsync(22, ListingID);
            var count24 = await CountReviewAsync(23, ListingID);
            var count25 = await CountReviewAsync(24, ListingID);
            var count26 = await CountReviewAsync(25, ListingID);
            var count27 = await CountReviewAsync(26, ListingID);
            var count28 = await CountReviewAsync(27, ListingID);
            var count29 = await CountReviewAsync(28, ListingID);
            var count30 = await CountReviewAsync(29, ListingID);
            DashboardListingLast30DaysReviews reviews = new DashboardListingLast30DaysReviews()
            {
                // One:
                One = GetDayName(0),
                OneCount = count1,

                // Two:
                Two = GetDayName(1),
                TwoCount = count2,

                // Three:
                Three = GetDayName(2),
                ThreeCount = count3,

                // Four:
                Four = GetDayName(3),
                FourCount = count4,

                // Five:
                Five = GetDayName(4),
                FiveCount = count5,

                // Six:
                Six = GetDayName(5),
                SixCount = count6,

                // Seven:
                Seven = GetDayName(6),
                SevenCount = count7,

                // Eight:
                Eight = GetDayName(7),
                EightCount = count8,

                // Nine:
                Nine = GetDayName(8),
                NineCount = count9,

                // Ten:
                Ten = GetDayName(9),
                TenCount = count10,

                // Eleven:
                Eleven = GetDayName(10),
                ElevenCount = count11,

                // Twelve:
                Twelve = GetDayName(11),
                TwelveCount = count12,

                // Thirteen:
                Thirteen = GetDayName(12),
                ThirteenCount = count13,

                // Fourteen:
                Fourteen = GetDayName(13),
                FourteenCount = count14,

                // Fifteen:
                Fifteen = GetDayName(14),
                FifteenCount = count15,

                // Sixteen:
                Sixteen = GetDayName(15),
                SixteenCount = count16,

                // Seventeen:
                Seventeen = GetDayName(16),
                SeventeenCount = count17,

                // Eighteen:
                Eighteen = GetDayName(17),
                EighteenCount = count18,

                // Nineteen:
                Nineteen = GetDayName(18),
                NineteenCount = count19,

                // Twenty:
                Twenty = GetDayName(19),
                TwentyCount = count20,

                // TwentyOne:
                TwentyOne = GetDayName(20),
                TwentyOneCount = count21,

                // TwentyTwo:
                TwentyTwo = GetDayName(21),
                TwentyTwoCount = count22,

                // TwentyThree:
                TwentyThree = GetDayName(22),
                TwentyThreeCount = count23,

                // TwentyFour:
                TwentyFour = GetDayName(23),
                TwentyFourCount = count24,

                // TwentyFive:
                TwentyFive = GetDayName(24),
                TwentyFiveCount = count25,

                // TwentySix:
                TwentySix = GetDayName(25),
                TwentySixCount = count26,

                // TwentySeven:
                TwentySeven = GetDayName(26),
                TwentySevenCount = count27,

                // TwentyEight:
                TwentyEight = GetDayName(27),
                TwentyEightCount = count28,

                // TwentyNine:
                TwentyNine = GetDayName(28),
                TwentyNineCount = count29,

                // TwentyNine:
                Thirty = GetDayName(29),
                ThirtyCount = count30
            };

            return reviews;
        }
    }
}
