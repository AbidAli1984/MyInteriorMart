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
    public partial class PaymentModeForm
    {
        [Parameter] public int MenuId { get; set; }
        [Parameter] public bool IsCreateFreeListing { get; set; }

        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] IUserService userService { get; set; }
        [Inject] IListingService listingService { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Inject] NavigationManager navManager { get; set; }

        PaymentModeVM PaymentModeVM { get; set; } = new PaymentModeVM();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public int Steps { get; set; }
        public bool IsPaymentModeExist { get; set; }
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
                    helper.NavigateToPageByStep(listing, Constants.WorkingHourComplete, navManager);

                    ListingId = listing.ListingID;
                    Steps = listing.Steps;
                    var paymentMode = await listingService.GetPaymentModeByListingId(ListingId);
                    if (paymentMode != null)
                    {
                        IsPaymentModeExist = true;
                        PaymentModeVM.SetViewModel(paymentMode);
                    }
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task AddOrUpdatePaymentMode()
        {
            if (!PaymentModeVM.isValid())
            {
                helper.ShowNotification(_notice, "Please select at least one payment mode.", NotificationType.Info);
                return;
            }

            try
            {
                buttonBusy = true;
                var paymentMode = await listingService.GetPaymentModeByListingId(ListingId);
                bool recordNotFound = paymentMode == null;

                if (recordNotFound)
                    paymentMode = new BOL.LISTING.PaymentMode();

                PaymentModeVM.SetContextModel(paymentMode);

                if (recordNotFound)
                {
                    paymentMode.ListingID = ListingId;
                    paymentMode.OwnerGuid = CurrentUserGuid;
                    paymentMode.IPAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

                    await listingService.AddAsync(paymentMode);
                }
                else
                {
                    await listingService.UpdateAsync(paymentMode);
                }

                await listingService.UpdateListingStepByOwnerId(CurrentUserGuid, Constants.PaymentModeComplete, Steps);
                if (!IsCreateFreeListing)
                    navManager.NavigateTo($"{url}/Images");
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
