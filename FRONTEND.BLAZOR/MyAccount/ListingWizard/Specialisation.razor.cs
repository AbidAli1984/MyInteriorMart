using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using BAL.Services.Contracts;
using BOL;
using BOL.ComponentModels.MyAccount.ListingWizard;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Specialisation
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        private ISharedService sharedService { get; set; }
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        IListingService listingService { get; set; }
        [Inject]
        Helper helper { get; set; }

        public SpecialisationVM SpecialisationVM { get; set; } = new SpecialisationVM();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public bool IsSpecialisationExist { get; set; }


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
                    helper.NavigateToPageByStep(listing, Constants.CategoryComplete, navManager);

                    ListingId = listing.ListingID;
                    var specialisation = await listingService.GetSpecialisationByListingId(ListingId);
                    if (specialisation != null)
                    {
                        IsSpecialisationExist = true;
                        SpecialisationVM.SetViewModel(specialisation);
                    }
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task SelectAllAsync()
        {
            SpecialisationVM.SelectOrUnselectAll(true);
            await Task.Delay(1);
        }

        public async Task CreateSpecialisationAsync()
        {
            if (!SpecialisationVM.isValid())
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Please select at least one specialisation.");
                return;
            }

            try
            {
                buttonBusy = true;
                var specialisation = await listingService.GetSpecialisationByListingId(ListingId);
                bool recordNotFound = specialisation == null;

                if (recordNotFound)
                    specialisation = new BOL.LISTING.Specialisation();

                SpecialisationVM.SetContextModel(specialisation);

                if (recordNotFound)
                {
                    specialisation.ListingID = ListingId;
                    specialisation.OwnerGuid = CurrentUserGuid;
                    specialisation.IPAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

                    await listingService.AddAsync(specialisation);
                }
                else
                {
                    await listingService.UpdateAsync(specialisation);
                }

                await listingService.UpdateListingStepByOwnerId(CurrentUserGuid, Constants.SpecialisationComplete);
                navManager.NavigateTo($"/MyAccount/Listing/WorkingHours");
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
            finally
            {
                buttonBusy = false;
            }
        }
    }
}
