using BOL.LISTING;
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
using BAL.Services.Contracts;
using DAL.Models;
using BOL.ComponentModels.Listings;

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
        private IHttpContextAccessor httpConAccess { get; set; }

        public int count { get; set; }

        [Parameter]
        public string ListingID { get; set; }

        public ListingDetailVM listingDetailVM { get; set; } = new ListingDetailVM();
        public string CurrentUserGuid { get; set; }
        public bool userAuthenticated { get; set; } = false;

        public string ErrorMessage { get; set; }
        public ApplicationUser iUser { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                int listingId = Int32.Parse(ListingID);
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    iUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;
                }
                listingDetailVM = await listingService.GetListingDetailByListingId(listingId, CurrentUserGuid);
                //await CheckBookmarkAsync();
                //await GetReviewsAsync();
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
                await jsRuntime.InvokeVoidAsync("initializeListingGallerySlick");
            }
        }

        public bool HideContact = true;
        public async Task ToggleContact()
        {
            HideContact = !HideContact;
            await Task.Delay(10);
        }

        public int countBookmark { get; set; }
        public int countLike { get; set; }
        public int countSubscribe { get; set; }
        public string userAgent { get; set; }        
        public bool ToggleCompleteAddress { get; set; }

        public async Task ToggleCompleteAddressAsync()
        {
            ToggleCompleteAddress = !ToggleCompleteAddress;
            await Task.Delay(1);
        }

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

        private async Task NoticeWithIcon(NotificationType type, string message, string description)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = message,
                Description = description,
                NotificationType = type
            });
        }
    }
}
