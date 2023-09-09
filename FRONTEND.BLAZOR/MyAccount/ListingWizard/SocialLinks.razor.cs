using AntDesign;
using BAL.Services.Contracts;
using BOL;
using BOL.LISTING;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class SocialLinks
    {
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] IUserService userService { get; set; }
        [Inject] IListingService listingService { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Inject] NavigationManager navManager { get; set; }
        [Inject] Helper helper { get; set; }

        public SocialNetwork SocialNetwork { get; set; } = new SocialNetwork();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public int Steps { get; set; }
        public bool IsSocialNetworkExist { get; set; }

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
                    helper.NavigateToPageByStep(listing, Constants.UploadImageComplete, navManager);

                    ListingId = listing.ListingID;
                    Steps = listing.Steps;
                    SocialNetwork = await listingService.GetSocialNetworkByListingId(ListingId);
                    IsSocialNetworkExist = SocialNetwork != null;
                    if (!IsSocialNetworkExist)
                        SocialNetwork = new SocialNetwork();
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        private bool isModelValid()
        {
            return !string.IsNullOrWhiteSpace(SocialNetwork.Facebook) || !string.IsNullOrWhiteSpace(SocialNetwork.WhatsappGroupLink) || !string.IsNullOrWhiteSpace(SocialNetwork.Linkedin)
                || !string.IsNullOrWhiteSpace(SocialNetwork.Twitter) || !string.IsNullOrWhiteSpace(SocialNetwork.Youtube) || !string.IsNullOrWhiteSpace(SocialNetwork.Instagram)
                || !string.IsNullOrWhiteSpace(SocialNetwork.Pinterest);
        }

        public async Task AddOrUpdateSocialNetwork()
        {
            if (!isModelValid())
            {
                helper.ShowNotification(_notice, "Please provide atlseat one social link.", NotificationType.Info);
                return;
            }

            try
            {
                buttonBusy = true;
                var socialNetwork = await listingService.GetSocialNetworkByListingId(ListingId);

                if (socialNetwork == null)
                {
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    SocialNetwork.ListingID = ListingId;
                    SocialNetwork.OwnerGuid = CurrentUserGuid;
                    SocialNetwork.IPAddress = Helper.GetIpAddress(httpConAccess);

                    await listingService.AddAsync(SocialNetwork);
                }
                else
                {
                    socialNetwork.WhatsappGroupLink = SocialNetwork.WhatsappGroupLink;
                    socialNetwork.Youtube = SocialNetwork.Youtube;
                    socialNetwork.Facebook = SocialNetwork.Facebook;
                    socialNetwork.Instagram = SocialNetwork.Instagram;
                    socialNetwork.Linkedin = SocialNetwork.Linkedin;
                    socialNetwork.Pinterest = SocialNetwork.Pinterest;
                    socialNetwork.Twitter = SocialNetwork.Twitter;
                    await listingService.UpdateAsync(socialNetwork);
                }

                await listingService.UpdateListingStepByOwnerId(CurrentUserGuid, Constants.SocialLinkComplete, Steps);
                navManager.NavigateTo($"/MyAccount/Listing/Keywords");
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, exc.Message, NotificationType.Error);
            }
            finally
            {
                buttonBusy = false;
            }
        }
    }
}
