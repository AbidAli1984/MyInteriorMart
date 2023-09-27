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
using System.Net;

namespace FRONTEND.BLAZOR.Listings
{
    public partial class ListingDetails
    {
        [Inject] private IUserService userService { get; set; }
        [Inject] private IListingService listingService { get; set; }
        [Inject] private IAuditService auditService { get; set; }
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] NavigationManager navigationManager { get; set; }

        [Parameter] public string City { get; set; }
        [Parameter] public string Detail { get; set; }
        [Parameter] public string ListingID { get; set; }

        public ApplicationUser applicationUser { get; set; }
        public ListingDetailVM listingDetailVM { get; set; } = new ListingDetailVM();

        public int count { get; set; }
        public string CurrentUserGuid { get; set; } = string.Empty;
        public bool userAuthenticated { get; set; } = false;
        public string ErrorMessage { get; set; }
        public string userAgent { get; set; }
        public bool ToggleCompleteAddress { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;

                    userAuthenticated = true;
                }

                listingDetailVM = await listingService.GetListingDetailByListingId(ListingID, CurrentUserGuid);
                if (listingDetailVM != null)
                {
                    listingDetailVM.shareUrl = navigationManager.Uri.Replace("%20", "-");
                    string title = "Catalogue: ";
                    if (listingDetailVM.Listing != null && !string.IsNullOrWhiteSpace(listingDetailVM.Listing.CompanyName))
                        title += listingDetailVM.Listing.CompanyName;

                    if (listingDetailVM.Address != null)
                        title += listingDetailVM.Address.City + ", " + listingDetailVM.Address.Locality;
                    await jsRuntime.InvokeVoidAsync("setTitle", title);
                }
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
                await jsRuntime.InvokeVoidAsync("initializeListingDetails");
            }
        }

        public async Task ToggleBookmark()
        {
            var bookmark = await auditService.GetBookmarkByListingAndUserId(listingDetailVM.ListingId, CurrentUserGuid);
            bool recordNotFound = bookmark == null;

            if (recordNotFound)
            {
                string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];
                string UserAgent = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                bookmark = new Bookmarks
                {
                    ListingID = listingDetailVM.ListingId,
                    UserGuid = CurrentUserGuid,
                    IPAddress = RemoteIpAddress,
                    UserAgent = UserAgent,
                    Mobile = applicationUser.PhoneNumber,
                    Email = applicationUser.Email,
                    VisitDate = DateTime.Now,
                    VisitTime = DateTime.Now,
                    Bookmark = true,
                };

                await auditService.AddAsync(bookmark);
            }
            else
            {
                bookmark.Bookmark = !bookmark.Bookmark;
                await auditService.UpdateAsync(bookmark);
            }
            listingDetailVM.IsBookmarked = bookmark.Bookmark;
        }

        public async Task ToggleLikeAsync()
        {
            var like = await auditService.GetLikeByListingAndUserId(listingDetailVM.ListingId, CurrentUserGuid);
            bool recordNotFound = like == null;

            if (recordNotFound)
            {
                string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];
                string UserAgent = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                like = new ListingLikeDislike
                {
                    ListingID = listingDetailVM.ListingId,
                    UserGuid = CurrentUserGuid,
                    IPAddress = RemoteIpAddress,
                    UserAgent = UserAgent,
                    Mobile = applicationUser.PhoneNumber,
                    Email = applicationUser.Email,
                    VisitDate = DateTime.Now,
                    VisitTime = DateTime.Now,
                    Like = true
                };

                await auditService.AddAsync(like);
            }
            else
            {
                like.Like = !like.Like;
                await auditService.UpdateAsync(like);
            }
            listingDetailVM.IsLiked = like.Like;
        }

        public async Task ToggleSubscribeAsync()
        {
            var subscriber = await auditService.GetSubscribeByListingAndUserId(listingDetailVM.ListingId, CurrentUserGuid);
            bool recordNotFound = subscriber == null;

            if (recordNotFound)
            {
                string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];
                string UserAgent = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                subscriber = new Subscribes
                {
                    ListingID = listingDetailVM.ListingId,
                    UserGuid = CurrentUserGuid,
                    IPAddress = RemoteIpAddress,
                    UserAgent = UserAgent,
                    Mobile = applicationUser.PhoneNumber,
                    Email = applicationUser.Email,
                    VisitDate = DateTime.Now,
                    VisitTime = DateTime.Now,
                    Subscribe = true
                };

                await auditService.AddAsync(subscriber);
            }
            else
            {
                subscriber.Subscribe = !subscriber.Subscribe;
                await auditService.UpdateAsync(subscriber);
            }
            listingDetailVM.IsSubscribe = subscriber.Subscribe;
        }

        public async Task ToggleCompleteAddressAsync()
        {
            ToggleCompleteAddress = !ToggleCompleteAddress;
            await Task.Delay(1);
        }

        public async Task CreateOrUpdateRating()
        {
            if (listingDetailVM.CurrentUserRating.Ratings <= 0 || string.IsNullOrEmpty(listingDetailVM.CurrentUserRating.Comment))
            {
                helper.ShowNotification(_notice, "Rating & Comment Required.", NotificationType.Info);
                return;
            }

            DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string time = timeZoneDate.ToString("hh:mm tt");
            DateTime currentTime = DateTime.Parse(time, System.Globalization.CultureInfo.CurrentCulture);

            if (userAuthenticated == true)
            {
                try
                {
                    var rating = await listingService.GetRatingByListingIdAndOwnerId(listingDetailVM.ListingId, CurrentUserGuid);
                    bool recordNotFound = rating == null;

                    if (recordNotFound)
                    {
                        string RemoteIpAddress = httpConAccess.HttpContext.Request.Headers["User-Agent"];
                        string UserAgent = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                        rating = new Rating
                        {
                            ListingID = listingDetailVM.ListingId,
                            OwnerGuid = CurrentUserGuid,
                            IPAddress = RemoteIpAddress,
                            Date = currentTime,
                            Time = currentTime,
                            Ratings = listingDetailVM.CurrentUserRating.Ratings,
                            Comment = listingDetailVM.CurrentUserRating.Comment
                        };

                        await listingService.AddAsync(rating);
                    }
                    else
                    {
                        rating.Ratings = listingDetailVM.CurrentUserRating.Ratings;
                        rating.Comment = listingDetailVM.CurrentUserRating.Comment;
                        await listingService.UpdateAsync(rating);
                    }
                    listingDetailVM.listReviews = await listingService.GetReviewsAsync(listingDetailVM.ListingId);
                    helper.ShowNotification(_notice, "Thank you for submitting your review.");
                }
                catch (Exception exc)
                {
                    string msg = exc.Message + "\n\n" + exc.InnerException.ToString();
                    helper.ShowNotification(_notice, msg, NotificationType.Error);
                }
            }
            else
            {
                helper.ShowNotification(_notice, "Use must be login to post reviews.", NotificationType.Info);
            }

        }
    }
}
