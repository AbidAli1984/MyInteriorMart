using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BAL;

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
                    if (listing == null)
                        navManager.NavigateTo("/MyAccount/Listing/Company");

                    IsCommunicationExist = listing.Steps >= Constants.CommunicationComplete;
                    ListingId = listing.ListingID;
                    var communication = await listingService.GetCommunicationByOwnerId(CurrentUserGuid);
                    if (communication != null)
                    {
                        CommunicationVM.Email = communication.Email;
                        CommunicationVM.Mobile = communication.Mobile;
                        CommunicationVM.Whatsapp = communication.Whatsapp;
                        CommunicationVM.Telephone = communication.Telephone;
                        CommunicationVM.TelephoneSecond = communication.TelephoneSecond;
                        CommunicationVM.Website = communication.Website;
                        CommunicationVM.TollFree = communication.TollFree;
                        CommunicationVM.Fax = communication.Fax;
                        CommunicationVM.SkypeID = communication.SkypeID;
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
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "Email, Mobile, Whatsapp and Skype ID is compulsory..");
                return;
            }
            if (!string.IsNullOrEmpty(CommunicationVM.WebsiteErrorMessage))
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", CommunicationVM.WebsiteErrorMessage);
                return;
            }
            if (!string.IsNullOrEmpty(CommunicationVM.MobileErrMessage) || !string.IsNullOrEmpty(CommunicationVM.WhatsappErrMessage))
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", CommunicationVM.MobileErrMessage + " or Whatsapp");
                return;
            }
            if (!string.IsNullOrEmpty(CommunicationVM.EmailErrMessage))
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", CommunicationVM.EmailErrMessage);
                return;
            }

            try
            {
                buttonBusy = true;
                var communication = await listingService.GetCommunicationByOwnerId(CurrentUserGuid);
                bool recordNotFound = communication == null;

                if (recordNotFound)
                    communication = new BOL.LISTING.Communication();

                communication.Email = CommunicationVM.Email;
                communication.Mobile = CommunicationVM.Mobile;
                communication.Whatsapp = CommunicationVM.Whatsapp;
                communication.Telephone = CommunicationVM.Telephone;
                communication.TelephoneSecond = CommunicationVM.TelephoneSecond;
                communication.Website = CommunicationVM.Website;
                communication.TollFree = CommunicationVM.TollFree;
                communication.Fax = CommunicationVM.Fax;
                communication.SkypeID = CommunicationVM.SkypeID;

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

                var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                listing.Steps = listing.Steps <= Constants.CommunicationComplete ? Constants.CommunicationComplete : listing.Steps;
                await listingService.UpdateAsync(listing);

                navManager.NavigateTo($"/MyAccount/Listing/Address");
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.InnerException.ToString());
            }
            finally
            {
                buttonBusy = false;
            }
        }
    }
}
