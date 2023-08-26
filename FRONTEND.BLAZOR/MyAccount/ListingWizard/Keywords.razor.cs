using AntDesign;
using BAL.Services.Contracts;
using BOL;
using BOL.LISTING;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Keywords
    {
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] IUserService userService { get; set; }
        [Inject] IListingService listingService { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Inject] NavigationManager navManager { get; set; }
        [Inject] Helper helper { get; set; }

        public List<Keyword> ListKeyword { get; set; }
        public IList<Keyword> InsertKeyword { get; set; } = new List<Keyword>();
        public IList<Keyword> DeleteKeywords { get; set; } = new List<Keyword>();
        public IList<string> dropDownKeyword { get; set; }

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public string SeoKeyword { get; set; }


        protected async override Task OnInitializedAsync()
        {
            try
            {
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    helper.NavigateToPageByStep(listing, Constants.CategoryComplete, navManager);

                    ListingId = listing.ListingID;
                    ListKeyword = await listingService.GetKeywordsByListingId(ListingId);
                    dropDownKeyword = await listingService.GetKeywords();


                    if (ListKeyword == null)
                        ListKeyword = new List<Keyword>();
                    if (dropDownKeyword == null)
                        dropDownKeyword = new List<string>();
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public void RemoveFromListKeyword(string keyword)
        {
            var deleteKeyword = ListKeyword.FirstOrDefault(x => x.SeoKeyword == keyword);
            if (deleteKeyword != null)
            {
                ListKeyword.Remove(deleteKeyword);
                DeleteKeywords.Add(deleteKeyword);
            }
        }

        public void RemoveFromInsertKeyword(string keyword)
        {
            var deleteKeyword = InsertKeyword.FirstOrDefault(x => x.SeoKeyword == keyword);
            if (deleteKeyword != null)
            {
                InsertKeyword.Remove(deleteKeyword);
            }
        }

        public void AddKeyword()
        {
            try
            {
                var existingListKeyword = ListKeyword.FirstOrDefault(x => x.SeoKeyword == SeoKeyword);
                var existingAddKeyword = InsertKeyword.FirstOrDefault(x => x.SeoKeyword == SeoKeyword);

                if (existingListKeyword == null && existingAddKeyword == null)
                {
                    var key = new Keyword
                    {
                        ListingID = ListingId,
                        OwnerGuid = CurrentUserGuid,
                        SeoKeyword = SeoKeyword
                    };
                    InsertKeyword.Add(key);
                }
                else
                {
                    helper.ShowNotification(_notice, NotificationType.Info, NotificationPlacement.BottomRight, "Notification", $"{SeoKeyword} is already exists in the listing");
                }
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
            finally
            {
                SeoKeyword = string.Empty;
            }
        }

        public async Task AddOrUpdateKeyword()
        {
            await listingService.DeleteKeywordsByListingId(DeleteKeywords);
            ListKeyword.AddRange(await listingService.AddKeywordsAsync(InsertKeyword));
            InsertKeyword.Clear();
        }

    }
}
