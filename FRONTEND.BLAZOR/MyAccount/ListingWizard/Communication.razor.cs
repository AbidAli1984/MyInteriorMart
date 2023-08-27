using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Communication
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        IListingService listingService { get; set; }
        [Inject]
        Helper helper { get; set; }

        public CommunicationVM CommunicationVM { get; set; } = new CommunicationVM();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public int Steps { get; set; }
        public bool IsCommunicationExist { get; set; }

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
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
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "Email, Mobile, Whatsapp and Skype ID is compulsory..");
                return;
            }
            if (!string.IsNullOrEmpty(CommunicationVM.WebsiteErrorMessage))
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", CommunicationVM.WebsiteErrorMessage);
                return;
            }
            if (!string.IsNullOrEmpty(CommunicationVM.MobileErrMessage) || !string.IsNullOrEmpty(CommunicationVM.WhatsappErrMessage))
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", CommunicationVM.MobileErrMessage + " or Whatsapp");
                return;
            }
            if (!string.IsNullOrEmpty(CommunicationVM.EmailErrMessage))
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", CommunicationVM.EmailErrMessage);
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
                navManager.NavigateTo($"/MyAccount/Listing/Address");
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.InnerException.ToString());
            }
            finally
            {
                buttonBusy = false;
            }
        }
    }
}
