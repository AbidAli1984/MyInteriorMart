using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL;
using Microsoft.AspNetCore.Components.Authorization;

namespace FRONTEND.BLAZOR.Component.MyAccount.ListingWizard
{
    public partial class CommunicationForm
    {
        [Parameter] public int MenuId { get; set; }
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] IUserService userService { get; set; }
        [Inject] IListingService listingService { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Inject] NavigationManager navManager { get; set; }

        public CommunicationVM CommunicationVM { get; set; } = new CommunicationVM();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public int Steps { get; set; }
        public bool IsCommunicationExist { get; set; }
        public string url;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                url = Constants.getListingUrl(MenuId);
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    var applicationUser = await userService.GetUserByUserName(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    helper.NavigateToPageByStep(listing, Constants.CompanyComplete, navManager);

                    ListingId = listing.ListingID;
                    Steps = listing.Steps;
                    var communication = await listingService.GetCommunicationByListingId(ListingId);
                    if (communication != null)
                    {
                        IsCommunicationExist = true;
                        CommunicationVM.SetViewModel(communication);
                    }
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task AddOrUpdateCommunication()
        {
            if (!CommunicationVM.isValid())
            {
                helper.ShowNotification(_notice, "Email, Mobile, Whatsapp and Skype ID is compulsory..", NotificationType.Info);
                return;
            }
            if (!string.IsNullOrEmpty(CommunicationVM.WebsiteErrorMessage))
            {
                helper.ShowNotification(_notice, CommunicationVM.WebsiteErrorMessage, NotificationType.Info);
                return;
            }
            if (!string.IsNullOrEmpty(CommunicationVM.MobileErrMessage) || !string.IsNullOrEmpty(CommunicationVM.WhatsappErrMessage))
            {
                helper.ShowNotification(_notice, CommunicationVM.MobileErrMessage + " or Whatsapp", NotificationType.Info);
                return;
            }
            if (!string.IsNullOrEmpty(CommunicationVM.EmailErrMessage))
            {
                helper.ShowNotification(_notice, CommunicationVM.EmailErrMessage, NotificationType.Info);
                return;
            }

            try
            {
                buttonBusy = true;
                var communication = await listingService.GetCommunicationByListingId(ListingId);
                bool recordNotFound = communication == null;

                if (recordNotFound)
                    communication = new BOL.LISTING.Communication();

                CommunicationVM.SetContextModel(communication);

                if (recordNotFound)
                {
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    communication.OwnerGuid = CurrentUserGuid;
                    communication.ListingID = ListingId;
                    communication.IPAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

                    await listingService.AddAsync(communication);
                }
                else
                {
                    await listingService.UpdateAsync(communication);
                }

                await listingService.UpdateListingStepByOwnerId(CurrentUserGuid, Constants.CommunicationComplete, Steps);
                navManager.NavigateTo($"{url}/Address");
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, exc.Message, NotificationType.Error);
                helper.ShowNotification(_notice, exc.InnerException.ToString(), NotificationType.Error);
            }
            finally
            {
                buttonBusy = false;
            }
        }
    }
}
