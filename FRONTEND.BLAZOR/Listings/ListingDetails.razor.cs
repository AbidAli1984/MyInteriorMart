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
using FRONTEND.BLAZOR.Services;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Http;
using BOL.AUDITTRAIL;
using Microsoft.AspNetCore.Identity;
using AntDesign;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using BOL.BANNERADS;

namespace FRONTEND.BLAZOR.Listings
{
    public partial class ListingDetails
    {
        [Inject]
        private IListingService listingService { get; set; }

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }

        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public IdentityUser iUser { get; set; }

        [Parameter]
        public string ListingID { get; set; }

        // Begin: Get All Listing Banner
        public int Banner1Count { get; set; }

        public IEnumerable<ListingBanner> ListingBannerList { get; set; }
        public async Task GetListingBannerListAsync()
        {
            var listingCat = await listingContext.Categories
                .Where(i => i.ListingID == Int32.Parse(ListingID))
                .FirstOrDefaultAsync();

            ListingBannerList = await listingContext.ListingBanner
                .Where(i => i.SecondCategoryID == listingCat.SecondCategoryID)
                .OrderBy(i => i.Priority)
                .ToListAsync();

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

        // Begin: Like, Bookmark and Subscribe
        public int countBookmark { get; set; }
        public int countLike { get; set; }
        public int countSubscribe { get; set; }

        public bool userAlreadySubscribed = false;
        public bool userAlreadyBookmarked = false;
        public bool userAlreadyLiked = false;
        // End:

        // Begin: User Details
        public string userAgent { get; set; }
        public string ipAddress { get; set; }
        // End:

        public FreeListingViewModel listing { get; set; }

        // Bein: Open Close Properties
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string OpenOn { get; set; }
        public bool Closed { get; set; }
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
            string userGuid = await applicationContext.Users.Where(i => i.UserName == userName).Select(i => i.Id).FirstOrDefaultAsync();

            // Shafi: Get Listing Owner Guid
            string listingOwnerGuid = await listingContext.Listing.Where(l => l.ListingID == listingId).Select(l => l.OwnerGuid).FirstOrDefaultAsync();
            // End:

            // Shafi: Get Owner Name
            string Name = await applicationContext.UserProfile.Where(p => p.OwnerGuid == listingOwnerGuid).Select(p => p.Name).FirstOrDefaultAsync();
            // End:

            // Shafi: Get Profile ID for Profile Image
            int ProfileID = await applicationContext.UserProfile.Where(p => p.OwnerGuid == listingOwnerGuid).Select(p => p.ProfileID).FirstOrDefaultAsync();
            // End:

            // Shafi: Get Owner Designation
            string Designation = await listingContext.Listing.Where(l => l.ListingID == listingId).Select(l => l.Designation).FirstOrDefaultAsync();
            // End:

            listing = new FreeListingViewModel();

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

            listing.Listing = await listingContext.Listing.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
            listing.Communication = await listingContext.Communication.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
            listing.Address = lavm;
            listing.Category = lcvm;
            listing.Specialisation = specialisation;
            listing.PaymentMode = await listingContext.PaymentMode.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
            listing.WorkingHour = await listingContext.WorkingHours.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();
            listing.FromTime = FromTime;
            listing.ToTime = ToTime;
            listing.OpenOn = OpenOn;
            listing.Closed = Closed;

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

            // Check if logged in user already subscribed
            userAlreadySubscribed = await listingService.CheckIfUserSubscribedToListing(listingId, userGuid);
            userAlreadyBookmarked = await listingService.CheckIfUserBookmarkedListing(listingId, userGuid);
            userAlreadyLiked = await listingService.CheckIfUserLikedListing(listingId, userGuid);
        }

        // Begin: Get Local Address
        public async Task<string> GetLocalAddress(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var localAddress = await listingContext.Address.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            return localAddress.LocalAddress;

        }
        // End:

        // Begin: Get Specialisation
        public async Task<Specialisation> GetSpecialisation(int listingId)
        {
            var specialisation = await listingContext.Specialisation.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

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
            var listingAllReviews = await listingContext.Rating
                .Where(i => i.ListingID == Int32.Parse(ListingID))
                .ToListAsync();

            foreach(var i in listingAllReviews)
            {
                var profile = await applicationContext.UserProfile
                    .Where(r => r.OwnerGuid == i.OwnerGuid)
                    .FirstOrDefaultAsync();

                if (profile != null)
                {
                    ReviewListingViewModel rlvm = new ReviewListingViewModel
                    {
                        ReviewID = i.RatingID,
                        OwnerGuid = i.OwnerGuid,
                        Comment = i.Comment,
                        Date = i.Date,
                        VisitTime = i.Time.ToString(),
                        Ratings = i.Ratings,
                        Name = profile.Name
                    };

                    listReviews.Add(rlvm);
                }
                else
                {
                    ReviewListingViewModel rlvm = new ReviewListingViewModel
                    {
                        ReviewID = i.RatingID,
                        OwnerGuid = i.OwnerGuid,
                        Comment = i.Comment,
                        Date = i.Date,
                        VisitTime = i.Time.ToString(),
                        Ratings = i.Ratings,
                        Name = ""
                    };

                    listReviews.Add(rlvm);
                }
            }
        }
        // End: Write Review

        // Begin: Country
        public async Task<Country> GetCountry(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var country = await sharedContext.Country.Where(i => i.CountryID == add.CountryID).FirstOrDefaultAsync();

            return country;

        }
        // End:

        // Begin: Get State
        public async Task<State> GetState(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var state = await sharedContext.State.Where(i => i.StateID == add.StateID).FirstOrDefaultAsync();

            return state;

        }
        // End:

        // Begin: Get State
        public async Task<City> GetCity(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var city = await sharedContext.City.Where(i => i.CityID == add.City).FirstOrDefaultAsync();

            return city;

        }
        // End:

        // Begin: Get State
        public async Task<Station> GetAssembly(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var assembly = await sharedContext.Station.Where(i => i.StationID == add.AssemblyID).FirstOrDefaultAsync();

            return assembly;

        }
        // End:

        // Begin: Get Pincode
        public async Task<Pincode> GetPincode(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var pincode = await sharedContext.Pincode.Where(i => i.PincodeID == add.PincodeID).FirstOrDefaultAsync();

            return pincode;

        }
        // End:

        // Begin: Get Pincode
        public async Task<Locality> GetLocality(int listingId)
        {
            var add = await listingContext.Address.Where(l => l.ListingID == listingId).FirstOrDefaultAsync();

            var locality = await sharedContext.Locality.Where(i => i.LocalityID == add.LocalityID).FirstOrDefaultAsync();

            return locality;

        }
        // End:

        // Begin: Get First Category
        public async Task<FirstCategory> GetFirstCat(int listingId)
        {
            var firstCat = await listingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            var category = await categoriesContext.FirstCategory.Where(i => i.FirstCategoryID == firstCat.FirstCategoryID).FirstOrDefaultAsync();

            return category;
        }
        // End:

        // Begin: Get Second Category
        public async Task<SecondCategory> GetSecondCat(int listingId)
        {
            var firstCat = await listingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

            var category = await categoriesContext.SecondCategory.Where(i => i.SecondCategoryID == firstCat.SecondCategoryID).FirstOrDefaultAsync();

            return category;
        }
        // End:

        // Begin: Business Open Or Close
        public async Task BusinessOpenClose(int listingId)
        {
            // Get Listing
            var listingWh = await listingContext.WorkingHours.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();
            // End:

            // Get Time Zone
            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string day = timeZoneDate.ToString("dddd");
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);
            // End:

            if (listing != null)
            {
                try
                {
                    if (day == "Monday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.MondayFrom.ToString("hh:mm tt");
                        string ToTime = listingWh.MondayTo.ToString("hh:mm tt");
                        OpenOn = "Tuesday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.MondayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Tuesday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.TuesdayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.TuesdayTo.ToString("hh:mm tt");
                        OpenOn = "Wednesday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.TuesdayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Wednesday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.WednesdayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.WednesdayTo.ToString("hh:mm tt");
                        OpenOn = "Thursday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.WednesdayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Thursday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.ThursdayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.ThursdayTo.ToString("hh:mm tt");
                        OpenOn = "Friday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.ThursdayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Friday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.FridayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.FridayTo.ToString("hh:mm tt");
                        OpenOn = "Saturday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.FridayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Saturday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.SaturdayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.SaturdayTo.ToString("hh:mm tt");
                        if (listingWh.SundayHoliday != true)
                        {
                            OpenOn = "Sunday";
                        }
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.SaturdayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (day == "Sunday")
                    {
                        // Shafi: Display ViewBag in Index view
                        FromTime = listingWh.SundayFrom.ToString("hh:mm tt");
                        ToTime = listingWh.SundayTo.ToString("hh:mm tt");
                        OpenOn = "Monday";
                        // End:

                        // Shafi: Get ToTime in ("hh:mm tt") format then convert it to string then create date time from it
                        string timeString = listingWh.SundayTo.ToString("hh:mm tt");
                        DateTime cToTime = DateTime.Parse(timeString, System.Globalization.CultureInfo.CurrentCulture);
                        // End:

                        if (currentTime > cToTime)
                        {
                            Closed = true;
                        }
                        else
                        {
                            Closed = false;
                        }
                    }

                    if (listingWh.SaturdayHoliday == true && listingWh.SundayHoliday == true)
                    {
                        FromTime = null;
                        ToTime = null;
                        OpenOn = "Monday";
                        Closed = true;
                    }

                    if (listingWh.SaturdayHoliday == true && listingWh.SundayHoliday == false)
                    {
                        FromTime = null;
                        ToTime = null;
                        OpenOn = "Sunday";
                        Closed = true;
                    }

                    if (listingWh.SundayHoliday == true)
                    {
                        FromTime = null;
                        ToTime = null;
                        OpenOn = "Monday";
                        Closed = true;
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
            var ratings = await listingContext.Rating.Where(r => r.ListingID == listingId).ToListAsync();

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

                            await listingContext.AddAsync(objRating);
                            await listingContext.SaveChangesAsync();
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
                            var cur = await listingContext.Rating
                                .Where(i => i.OwnerGuid == CurrentUserGuid && i.ListingID == Int32.Parse(ListingID)).FirstOrDefaultAsync();

                            if(cur != null)
                            {
                                cur.OwnerGuid = CurrentUserGuid;
                                cur.IPAddress = RemoteIpAddress;
                                cur.Date = currentTime;
                                cur.Time = currentTime;
                                cur.Ratings = rating.Value;
                                cur.Comment = Comment;
                            }

                            listingContext.Update(cur);
                            await listingContext.SaveChangesAsync();
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
            CurrentUserRating = await listingContext.Rating
                .Where(i => i.ListingID == Int32.Parse(ListingID) && i.OwnerGuid == CurrentUserGuid)
                .FirstOrDefaultAsync();
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

        [Inject]
        public IWebHostEnvironment hostEnv { get; set; }
        public async Task CheckIfLogoExist()
        {
            var file = hostEnv.WebRootPath + "\\FileManager\\ListingLogo\\" + ListingID + ".jpg";
            if (File.Exists(file) == true)
            {
                LogoExist = true;
                LogoUrl = "/FileManager/ListingLogo/" + ListingID + ".jpg";
            }
            else
            {
                LogoExist = false;
                LogoUrl = null;
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
                    iUser = await applicationContext.Users.Where(i => i.UserName == user.Identity.Name).FirstOrDefaultAsync();
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
