using BOL.CATEGORIES;
using BOL.LISTING;
using BOL.SHARED;
using BOL.VIEWMODELS;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Http;
using BOL.AUDITTRAIL;
using AntDesign;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using BOL.BANNERADS;
using BAL.Services.Contracts;
using BOL.IDENTITY;
using DAL.Models;

namespace FRONTEND.BLAZOR.Listings
{
    public partial class ListingDetails
    {
        [Inject]
        private IUserService userService { get; set; }
        [Inject]
        private IUserProfileService userProfileService { get; set; }

        [Inject]
        private IListingService listingService { get; set; }

        [Inject]
        private IAuditService auditService { get; set; }

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }

        [Inject]
        public IWebHostEnvironment hostEnv { get; set; }

        [Parameter]
        public string ListingID { get; set; }



        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public ApplicationUser iUser { get; set; }


        // Begin: Get All Listing Banner
        public int Banner1Count { get; set; }

        public IEnumerable<ListingBanner> ListingBannerList { get; set; }
        public async Task GetListingBannerListAsync()
        {
            ListingBannerList = await listingService.GetSecCatListingByListingId(Int32.Parse(ListingID));

            Banner1Count = ListingBannerList.Where(i => i.Placement == "banner-1").Count();
        }
        // End: Get All Listing Banner

        // Begin: Toggle Contact
        public bool HideContact = true;
        public async Task ToggleContact()
        {
            HideContact = !HideContact;
            await Task.Delay(10);
        }
        // End:

        // Begin: Like, Bookmark and 
        public bool userAlreadySubscribed = false;
        public bool userAlreadyBookmarked = false;
        public bool userAlreadyLiked = false;

        public int countBookmark { get; set; }
        public int countLike { get; set; }
        public int countSubscribe { get; set; }
        // End:

        // Begin: User Details
        public string userAgent { get; set; }
        public string ipAddress { get; set; }
        // End:

        public FreeListingViewModel listingViewModel { get; set; }

        // Bein: Open Close Properties
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string OpenOn { get; set; }
        public bool IsClosed { get; set; }
        // End:

        // Begin: Rating properties
        public string RatingAverage { get; set; }
        public decimal rating1 { get; set; }
        public decimal rating2 { get; set; }
        public decimal rating3 { get; set; }
        public decimal rating4 { get; set; }
        public decimal rating5 { get; set; }
        // End:
        public async Task GetListing()
        {
            // Parse ListingID
            int listingId = Int32.Parse(ListingID);

            // Get User Name
            var authstate = await authenticationState.GetAuthenticationStateAsync();
            var user = authstate.User;
            var userName = user.Identity.Name;
            string userGuid = (await userService.GetUserByUserName(userName)).Id;

            // Check if logged in user already subscribed
            userAlreadySubscribed = await auditService.CheckIfUserSubscribedToListing(listingId, userGuid);
            userAlreadyBookmarked = await auditService.CheckIfUserBookmarkedListing(listingId, userGuid);
            userAlreadyLiked = await auditService.CheckIfUserLikedListing(listingId, userGuid);

            // Shafi: Get Listing Owner Guid
            Listing listing = await listingService.GetListingByListingId(listingId);
            string listingOwnerGuid = listing.OwnerGuid;
            string Designation = listing.Designation;
            // End:

            UserProfile userProfile = await userProfileService.GetProfileByOwnerGuid(listingOwnerGuid);
            string Name = userProfile.Name;
            int ProfileID = userProfile.ProfileID;

            listingViewModel = new FreeListingViewModel();

            // Begin: Address View Model
            var country = await GetCountry(listingId);
            var state = await GetState(listingId);
            var city = await GetCity(listingId);
            var assembly = await GetAssembly(listingId);
            var pincode = await GetPincode(listingId);
            var locality = await GetLocality(listingId);
            var localAddress = await GetLocalAddress(listingId);
            var specialisation = await GetSpecialisation(listingId);

            // Begin: Get Open Close
            await BusinessOpenClose(listingId);
            // End:

            ListingAddressVM lavm = new ListingAddressVM
            {
                Country = country,
                State = state,
                City = city,
                Assembly = assembly,
                Pincode = pincode,
                Locality = locality,
                LocalAddress = localAddress
            };
            // End:

            // Begin: Category View Model
            var firstCat = await GetFirstCat(listingId);
            var secondCat = await GetSecondCat(listingId);

            ListingCategoryVM lcvm = new ListingCategoryVM
            {
                FirstCategory = firstCat,
                SecondCategory = secondCat
            };
            // End:

            listingViewModel.Listing = listing;
            listingViewModel.Communication = await listingService.GetCommunicationByListingId(listingId);
            listingViewModel.Address = lavm;
            listingViewModel.Category = lcvm;
            listingViewModel.Specialisation = specialisation;
            listingViewModel.PaymentMode = await listingService.GetPaymentModeByListingId(listingId);
            listingViewModel.WorkingHour = await listingService.GetWorkingHoursByListingId(listingId); ;
            listingViewModel.FromTime = OpenTime;
            listingViewModel.ToTime = CloseTime;
            listingViewModel.OpenOn = OpenOn;
            listingViewModel.Closed = IsClosed;

            // Shafi: Get Time Zone
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            // End:

            // Rating Average
            await GetRatingAverage(listingId);

            // Count Bookmark, Like and Subscribe
            countBookmark = await auditContext.Bookmarks
                .Where(i => i.ListingID == listingId)
                .Where(i => i.Bookmark == true)
                .CountAsync();

            countLike = await auditContext.ListingLikeDislike
                .Where(i => i.ListingID == listingId)
                .Where(i => i.Like == true)
                .CountAsync();

            countSubscribe = await auditContext.Subscribes
                .Where(i => i.ListingID == listingId)
                .Where(i => i.Subscribe == true)
                .CountAsync();
        }

        // Begin: Get Local Address
        public async Task<string> GetLocalAddress(int listingId)
        {
            var localAddress = await listingService.GetAddressByListingId(listingId);

            return localAddress.LocalAddress;

        }
        // End:

        // Begin: Get Specialisation
        public async Task<Specialisation> GetSpecialisation(int listingId)
        {
            var specialisation = await listingService.GetSpecialisationByListingId(listingId);

            return specialisation;

        }
        // End:

        // Begin: Toggle Complete Address
        public bool ToggleCompleteAddress { get; set; }

        public async Task ToggleCompleteAddressAsync()
        {
            ToggleCompleteAddress = !ToggleCompleteAddress;
            await Task.Delay(1);
        }
        // End: Toggle Complete Address

        // Begin: Bookmark

        public bool bookarmk { get; set; }

        public async Task CheckBookmarkAsync()
        {
            var bookmarkExist = await auditContext.Bookmarks.Where(i => i.ListingID == Int32.Parse(ListingID) && i.UserGuid == CurrentUserGuid && i.Bookmark == true).FirstOrDefaultAsync();

            if(bookmarkExist == null)
            {
                bookarmk = false;
            }
            else if(bookmarkExist != null && bookmarkExist.Bookmark == true)
            {
                bookarmk = true;
            }
            else if(bookmarkExist != null && bookmarkExist.Bookmark == false)
            {
                bookarmk = false;
            }
        }

        public async Task CountBookmarkAsync(int listingId)
        {
            countBookmark = await auditContext.Bookmarks
                .Where(i => i.ListingID == listingId)
                .Where(i => i.Bookmark == true)
                .CountAsync();
        }

        public async Task ToggleBookmark(int listingId)
        {
            var bookmarkExist = await auditContext.Bookmarks.Where(i => i.ListingID == listingId && i.UserGuid == CurrentUserGuid && i.Bookmark == true).FirstOrDefaultAsync();

            string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];
            string UserAgent = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

            if (bookmarkExist != null && bookmarkExist.Bookmark == true)
            {
                bookmarkExist.Bookmark = false;
                bookmarkExist.UserAgent = UserAgent;
                bookmarkExist.IPAddress = RemoteIpAddress;
                try
                {
                    auditContext.Update(bookmarkExist);
                    await auditContext.SaveChangesAsync();
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }               

                await CheckBookmarkAsync();
                await CountBookmarkAsync(listingId);
            }
            else if(bookmarkExist != null && bookmarkExist.Bookmark == false)
            {
                bookmarkExist.Bookmark = true;
                bookmarkExist.UserAgent = UserAgent;
                bookmarkExist.IPAddress = RemoteIpAddress;
                auditContext.Update(bookmarkExist);
                await auditContext.SaveChangesAsync();
                await CheckBookmarkAsync();
                await CountBookmarkAsync(listingId);
            }
            else
            {
                Bookmarks bm = new Bookmarks
                {
                    IPAddress = RemoteIpAddress,
                    UserAgent = UserAgent,
                    UserGuid = CurrentUserGuid,
                    Mobile = iUser.PhoneNumber,
                    Email = iUser.Email,
                    VisitDate = DateTime.Now,
                    VisitTime = DateTime.Now,
                    Bookmark = true,
                    ListingID = listingId
                };

                try
                {
                    await auditContext.AddAsync(bm);
                    await auditContext.SaveChangesAsync();
                    await CheckBookmarkAsync();
                    await CountBookmarkAsync(listingId);
                }
                catch(Exception e)
                {
                    ErrorMessage = e.Message + e.InnerException;
                    await CheckBookmarkAsync();
                    await CountBookmarkAsync(listingId);
                }
            }
        }
        // End: Bookmark

        // Begin: Like

        public bool like { get; set; }

        public async Task CheckLikeAsync()
        {
            var likeExist = await auditContext.ListingLikeDislike.Where(i => i.ListingID == Int32.Parse(ListingID) && i.UserGuid == CurrentUserGuid).FirstOrDefaultAsync();

            if (likeExist == null)
            {
                like = false;
            }
            else if (likeExist != null && likeExist.Like == true)
            {
                like = true;
            }
            else if (likeExist != null && likeExist.Like == false)
            {
                like = false;
            }
        }

        public async Task CountLikeAsync(int listingId)
        {
            try
            {
                countLike = await auditContext.ListingLikeDislike
                    .Where(i => i.ListingID == listingId)
                    .Where(i => i.Like == true)
                    .CountAsync();
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        public async Task ToggleLikeAsync(int listingId)
        {
            var likeExist = await auditContext.ListingLikeDislike
                .Where(i => i.ListingID == listingId && i.UserGuid == CurrentUserGuid)
                .FirstOrDefaultAsync();

            string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];
            string UserAgent = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

            if (likeExist != null && likeExist.Like == true)
            {
                likeExist.Like = false;
                likeExist.UserAgent = UserAgent;
                likeExist.IPAddress = RemoteIpAddress;
                try
                {
                    auditContext.Update(likeExist);
                    await auditContext.SaveChangesAsync();
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }

                await CheckLikeAsync();
                await CountLikeAsync(listingId);
            }
            else if (likeExist != null && likeExist.Like == false)
            {
                likeExist.Like = true;
                likeExist.UserAgent = UserAgent;
                likeExist.IPAddress = RemoteIpAddress;

                try
                {
                    auditContext.Update(likeExist);
                    await auditContext.SaveChangesAsync();
                }
                catch(Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
                
                await CheckLikeAsync();
                await CountLikeAsync(listingId);
            }
            else
            {
                try
                {
                    ListingLikeDislike objLike = new ListingLikeDislike
                    {
                        IPAddress = RemoteIpAddress,
                        UserAgent = UserAgent,
                        UserGuid = CurrentUserGuid,
                        Mobile = iUser.PhoneNumber,
                        Email = iUser.Email,
                        VisitDate = DateTime.Now,
                        VisitTime = DateTime.Now,
                        Like = true,
                        ListingID = listingId
                    };

                    try
                    {
                        await auditContext.AddAsync(objLike);
                        await auditContext.SaveChangesAsync();
                        await CheckLikeAsync();
                        await CountLikeAsync(listingId);
                    }
                    catch (Exception e)
                    {
                        ErrorMessage = e.Message + e.InnerException;
                        await CheckLikeAsync();
                        await CountLikeAsync(listingId);
                    }
                }
                catch(Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
        // End: Like

        // Begin: Subscribe

        public bool subscribe { get; set; }

        public async Task CheckSubscribeAsync()
        {
            var subscribeExist = await auditContext.Subscribes.Where(i => i.ListingID == Int32.Parse(ListingID) && i.UserGuid == CurrentUserGuid && i.Subscribe == true).FirstOrDefaultAsync();

            if (subscribeExist == null)
            {
                subscribe = false;
            }
            else if (subscribeExist != null && subscribeExist.Subscribe == true)
            {
                subscribe = true;
            }
            else if (subscribeExist != null && subscribeExist.Subscribe == false)
            {
                subscribe = false;
            }
        }

        public async Task CountSubscribeAsync(int listingId)
        {
            try
            {
                countSubscribe = await auditContext.Subscribes
                    .Where(i => i.ListingID == listingId)
                    .Where(i => i.Subscribe == true)
                    .CountAsync();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }

        public async Task ToggleSubscribeAsync(int listingId)
        {
            var subscribeExist = await auditContext.Subscribes.Where(i => i.ListingID == listingId && i.UserGuid == CurrentUserGuid).FirstOrDefaultAsync();

            string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];
            string UserAgent = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

            if (subscribeExist != null && subscribeExist.Subscribe == true)
            {
                subscribeExist.Subscribe = false;
                subscribeExist.UserAgent = UserAgent;
                subscribeExist.IPAddress = RemoteIpAddress;
                try
                {
                    auditContext.Update(subscribeExist);
                    await auditContext.SaveChangesAsync();
                    await CheckSubscribeAsync();
                    await CountSubscribeAsync(listingId);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
            else if (subscribeExist != null && subscribeExist.Subscribe == false)
            {
                subscribeExist.Subscribe = true;
                subscribeExist.UserAgent = UserAgent;
                subscribeExist.IPAddress = RemoteIpAddress;

                try
                {
                    auditContext.Update(subscribeExist);
                    await auditContext.SaveChangesAsync();
                    await CheckSubscribeAsync();
                    await CountSubscribeAsync(listingId);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
            else
            {
                try
                {
                    Subscribes objSubscribe = new Subscribes
                    {
                        IPAddress = RemoteIpAddress,
                        UserAgent = UserAgent,
                        UserGuid = CurrentUserGuid,
                        Mobile = iUser.PhoneNumber,
                        Email = iUser.Email,
                        VisitDate = DateTime.Now,
                        VisitTime = DateTime.Now,
                        Subscribe = true,
                        ListingID = listingId
                    };

                    try
                    {
                        await auditContext.AddAsync(objSubscribe);
                        await auditContext.SaveChangesAsync();
                        await CheckSubscribeAsync();
                        await CountSubscribeAsync(listingId);
                    }
                    catch (Exception e)
                    {
                        ErrorMessage = e.Message + e.InnerException;
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
        // End: Subscribe

        // Begin: Review
        public IList<ReviewListingViewModel> listReviews = new List<ReviewListingViewModel>();
        public async Task GetReviewsAsync()
        {
            var listingAllReviews = await listingService.GetRatingsByListingId(Int32.Parse(ListingID));

            foreach(var i in listingAllReviews)
            {
                var profile = await userProfileService.GetProfileByOwnerGuid(i.OwnerGuid);
                ReviewListingViewModel rlvm = new ReviewListingViewModel
                {
                    ReviewID = i.RatingID,
                    OwnerGuid = i.OwnerGuid,
                    Comment = i.Comment,
                    Date = i.Date,
                    VisitTime = i.Time.ToString(),
                    Ratings = i.Ratings,
                };

                rlvm.Name = profile != null ? profile.Name : "";
                listReviews.Add(rlvm);
            }
        }
        // End: Write Review

        // Begin: Country
        public async Task<Country> GetCountry(int listingId)
        {
            var add = await listingService.GetAddressByListingId(listingId);

            var country = await sharedContext.Country.Where(i => i.CountryID == add.CountryID).FirstOrDefaultAsync();

            return country;

        }
        // End:

        // Begin: Get State
        public async Task<State> GetState(int listingId)
        {
            var add = await listingService.GetAddressByListingId(listingId);

            var state = await sharedContext.State.Where(i => i.StateID == add.StateID).FirstOrDefaultAsync();

            return state;

        }
        // End:

        // Begin: Get State
        public async Task<City> GetCity(int listingId)
        {
            var add = await listingService.GetAddressByListingId(listingId);

            var city = await sharedContext.City.Where(i => i.CityID == add.City).FirstOrDefaultAsync();

            return city;

        }
        // End:

        // Begin: Get State
        public async Task<Station> GetAssembly(int listingId)
        {
            var add = await listingService.GetAddressByListingId(listingId);

            var assembly = await sharedContext.Station.Where(i => i.StationID == add.AssemblyID).FirstOrDefaultAsync();

            return assembly;

        }
        // End:

        // Begin: Get Pincode
        public async Task<Pincode> GetPincode(int listingId)
        {
            var add = await listingService.GetAddressByListingId(listingId);

            var pincode = await sharedContext.Pincode.Where(i => i.PincodeID == add.PincodeID).FirstOrDefaultAsync();

            return pincode;

        }
        // End:

        // Begin: Get Pincode
        public async Task<Locality> GetLocality(int listingId)
        {
            var add = await listingService.GetAddressByListingId(listingId);

            var locality = await sharedContext.Locality.Where(i => i.LocalityID == add.LocalityID).FirstOrDefaultAsync();

            return locality;

        }
        // End:

        // Begin: Get First Category
        public async Task<FirstCategory> GetFirstCat(int listingId)
        {
            var firstCat = await listingService.GetCategoryByListingId(listingId);

            var category = await categoriesContext.FirstCategory.Where(i => i.FirstCategoryID == firstCat.FirstCategoryID).FirstOrDefaultAsync();

            return category;
        }
        // End:

        // Begin: Get Second Category
        public async Task<SecondCategory> GetSecondCat(int listingId)
        {
            var firstCat = await listingService.GetCategoryByListingId(listingId);

            var category = await categoriesContext.SecondCategory.Where(i => i.SecondCategoryID == firstCat.SecondCategoryID).FirstOrDefaultAsync();

            return category;
        }
        // End:

        // Begin: Business Open Or Close
        public async Task BusinessOpenClose(int listingId)
        {
            // Get Listing
            var listingWh = await listingService.GetWorkingHoursByListingId(listingId);
            // End:

            // Get Time Zone
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            // End:

            if (listingViewModel != null)
            {
                try
                {
                    if (day == "Monday")
                    {
                        OpenTime = listingWh.MondayFrom.ToString("hh:mm tt");
                        CloseTime = listingWh.MondayTo.ToString("hh:mm tt");
                        OpenOn = "Tuesday";
                    }
                    else if (day == "Tuesday")
                    {
                        OpenTime = listingWh.TuesdayFrom.ToString("hh:mm tt");
                        CloseTime = listingWh.TuesdayTo.ToString("hh:mm tt");
                        OpenOn = "Wednesday";
                    }
                    else if (day == "Wednesday")
                    {
                        OpenTime = listingWh.WednesdayFrom.ToString("hh:mm tt");
                        CloseTime = listingWh.WednesdayTo.ToString("hh:mm tt");
                        OpenOn = "Thursday";
                    }
                    else if (day == "Thursday")
                    {
                        OpenTime = listingWh.ThursdayFrom.ToString("hh:mm tt");
                        CloseTime = listingWh.ThursdayTo.ToString("hh:mm tt");
                        OpenOn = "Friday";
                    }
                    else if (day == "Friday")
                    {
                        OpenTime = listingWh.FridayFrom.ToString("hh:mm tt");
                        CloseTime = listingWh.FridayTo.ToString("hh:mm tt");
                        OpenOn = "Saturday";
                    }
                    else if (day == "Saturday")
                    {
                        OpenTime = listingWh.SaturdayFrom.ToString("hh:mm tt");
                        CloseTime = listingWh.SaturdayTo.ToString("hh:mm tt");
                        if (listingWh.SundayHoliday != true)
                        {
                            OpenOn = "Sunday";
                        }
                    }
                    else if (day == "Sunday")
                    {
                        OpenTime = listingWh.SundayFrom.ToString("hh:mm tt");
                        CloseTime = listingWh.SundayTo.ToString("hh:mm tt");
                        OpenOn = "Monday";
                    }

                    if (!String.IsNullOrEmpty(CloseTime))
                    {
                        DateTime cToTime = DateTime.Parse(CloseTime, System.Globalization.CultureInfo.CurrentCulture);
                        IsClosed = currentTime > cToTime;
                    }

                    if ((listingWh.SaturdayHoliday == true && listingWh.SundayHoliday == true) 
                        || listingWh.SaturdayHoliday == true || listingWh.SundayHoliday == true)
                    {
                        OpenTime = null;
                        CloseTime = null;
                        IsClosed = true;

                        if ((listingWh.SaturdayHoliday == true && listingWh.SundayHoliday == true))
                            OpenOn = "Monday";
                        else if (listingWh.SaturdayHoliday == true)
                            OpenOn = "Sunday";
                        else
                            OpenOn = "Monday";
                    }
                }
                catch (Exception exc)
                {
                }
            }
        }
        // End: Business Open Or Close


        // Begin: Get Rating Average
        public async Task GetRatingAverage(int listingId)
        {
            // Get rating
            var ratingCount = await listingService.GetRatingAsync(listingId);

            if (ratingCount.Count() > 0)
            {
                var R1 = await listingService.CountRatingAsync(listingId, 1);
                var R2 = await listingService.CountRatingAsync(listingId, 2);
                var R3 = await listingService.CountRatingAsync(listingId, 3);
                var R4 = await listingService.CountRatingAsync(listingId, 4);
                var R5 = await listingService.CountRatingAsync(listingId, 5);

                decimal averageCount = 5 * R5 + 4 * R4 + 3 * R3 + 2 * R2 + 1 * R1;
                decimal weightedCount = R5 + R4 + R3 + R2 + R1;
                decimal ratingAverage = averageCount / weightedCount;
                RatingAverage = ratingAverage.ToString("0.0");

                decimal R1Total = R1 * 15;
                decimal R2Total = R2 * 15;
                decimal R3Total = R3 * 15;
                decimal R4Total = R4 * 15;
                decimal R5Total = R5 * 15;

                decimal SubTotalOfAllR = R1Total + R2Total + R3Total + R4Total + R5Total;

                decimal R1Percent = (R1Total * 100) / SubTotalOfAllR;
                decimal R2Percent = (R2Total * 100) / SubTotalOfAllR;
                decimal R3Percent = (R3Total * 100) / SubTotalOfAllR;
                decimal R4Percent = (R4Total * 100) / SubTotalOfAllR;
                decimal R5Percent = (R5Total * 100) / SubTotalOfAllR;

                rating1 = R1Percent;
                rating2 = R2Percent;
                rating3 = R3Percent;
                rating4 = R4Percent;
                rating5 = R5Percent;
            }
            else
            {
                RatingAverage = "0";
            }
        }
        // End: Get rating average

        // Begin: Write Review
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public Rating CurrentUserRating { get; set; }

        public async Task CreateRatingAsync()
        {
            // Get Time Zone
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            // End:

            int? rating = (int)Rating;

            if(userAuthenticated == true)
            {
                string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];

                try
                {
                    if (rating == null && string.IsNullOrEmpty(Comment) == true)
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "Rating & Comment Required.");
                    }
                    else if (rating == null && string.IsNullOrEmpty(Comment) == false)
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "Rating Required.");
                    }
                    else if (rating != null && string.IsNullOrEmpty(Comment) == true)
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "Comment Required.");
                    }
                    else if (rating == 0 && string.IsNullOrEmpty(Comment) == false)
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "You must select rating.");
                    }
                    else if (rating >= 1 && string.IsNullOrEmpty(Comment) == false)
                    {
                        try
                        {
                            Rating objRating = new Rating()
                            {
                                ListingID = Int32.Parse(ListingID),
                                OwnerGuid = CurrentUserGuid,
                                IPAddress = RemoteIpAddress,
                                Date = currentTime,
                                Time = currentTime,
                                Ratings = rating.Value,
                                Comment = Comment
                            };

                            await listingService.AddAsync(objRating);
                            await GetReviewsAsync();

                            await NoticeWithIcon(NotificationType.Success, "Success", "Thank you for submitting your review.");
                        }
                        catch (Exception exc)
                        {
                            string msg = exc.Message + "\n\n" + exc.InnerException.ToString();
                            await NoticeWithIcon(NotificationType.Error, "Error", msg);
                        }
                    }
                    else
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "Something went wrong.");
                    }
                }
                catch (Exception exc)
                {
                    await NoticeWithIcon(NotificationType.Error, "Error", exc.Message);
                }
            }
            else
            {
                await NoticeWithIcon(NotificationType.Error, "Error", "Use must be login to post reviews.");
            }
            
        }

        public async Task EditSaveRatingAsync()
        {
            // Get Time Zone
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            // End:

            int? rating = (int)Rating;

            if (userAuthenticated == true)
            {
                string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];

                try
                {
                    if (rating == null && string.IsNullOrEmpty(Comment) == true)
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "Rating & Comment Required.");
                    }
                    else if (rating == null && string.IsNullOrEmpty(Comment) == false)
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "Rating Required.");
                    }
                    else if (rating != null && string.IsNullOrEmpty(Comment) == true)
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "Comment Required.");
                    }
                    else if (rating == 0 && string.IsNullOrEmpty(Comment) == false)
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "You must select rating.");
                    }
                    else if (rating >= 1 && string.IsNullOrEmpty(Comment) == false)
                    {
                        try
                        {
                            var cur = await listingService.GetRatingsByListingIdAndOwnerId(Int32.Parse(ListingID), CurrentUserGuid);

                            if(cur != null)
                            {
                                cur.OwnerGuid = CurrentUserGuid;
                                cur.IPAddress = RemoteIpAddress;
                                cur.Date = currentTime;
                                cur.Time = currentTime;
                                cur.Ratings = rating.Value;
                                cur.Comment = Comment;
                            }

                            await listingService.UpdateAsync(cur);
                            await GetReviewsAsync();

                            await NoticeWithIcon(NotificationType.Success, "Success", "Thank you for editing your review.");
                        }
                        catch (Exception exc)
                        {
                            string msg = exc.Message + "\n\n" + exc.InnerException.ToString();
                            await NoticeWithIcon(NotificationType.Error, "Error", msg);
                        }
                    }
                    else
                    {
                        await NoticeWithIcon(NotificationType.Error, "Error", "Something went wrong.");
                    }
                }
                catch (Exception exc)
                {
                    await NoticeWithIcon(NotificationType.Error, "Error", exc.Message);
                }
            }
            else
            {
                await NoticeWithIcon(NotificationType.Error, "Error", "Use must be login to post reviews.");
            }

        }

        public async Task CheckUserRatingExist()
        {
            CurrentUserRating = await listingService.GetRatingsByListingIdAndOwnerId(Int32.Parse(ListingID), CurrentUserGuid);
            if(CurrentUserRating != null)
            {
                Rating = CurrentUserRating.Ratings;
                Comment = CurrentUserRating.Comment;
            }
        }
        // End: Write Review

        // Begin: Antdesign Blazor Notification
        private async Task NoticeWithIcon(NotificationType type, string message, string description)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = message,
                Description = description,
                NotificationType = type
            });
        }
        // End: Antdesign Blazor Notification

        // Begin: Check if logo exists
        public bool LogoExist { get; set; }
        public string LogoUrl { get; set; }

        public async Task CheckIfLogoExist()
        {
            var file = hostEnv.WebRootPath + "\\FileManager\\ListingLogo\\" + ListingID + ".jpg";
            LogoExist = File.Exists(file);
            if (LogoExist)
            {
                LogoUrl = "/FileManager/ListingLogo/" + ListingID + ".jpg";
            }
            await Task.Delay(10);
        }
        // End: Check if logo exists

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if(user.Identity.IsAuthenticated)
                {
                    iUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;
                }               

                await GetListing();
                await GetListingBannerListAsync();
                await CheckBookmarkAsync();
                await GetReviewsAsync();
                await CheckUserRatingExist();
                await CheckIfLogoExist();
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }

        protected override async Task OnAfterRenderAsync(bool render)
        {
            if (render)
            {
                await jsRuntime.InvokeVoidAsync("InitializeCarousel");
            }
        }
    }
}
