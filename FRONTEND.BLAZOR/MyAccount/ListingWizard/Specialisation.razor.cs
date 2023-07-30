using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AntDesign;
using DAL.Models;
using BAL.Services.Contracts;
using BAL;
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
                    if (listing == null)
                        navManager.NavigateTo("/MyAccount/Listing/Company");
                    else if (listing.Steps < Constants.CategoryComplete)
                        helper.NavigateToPageByStep(listing.Steps, navManager);

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
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Please select at least one specialisation.");
                return;
            }

            try
            {
                buttonBusy = true;
                var specialisation = await listingService.GetSpecialisationByListingId(ListingId);
                bool recordNotFound = specialisation == null;

                if (recordNotFound)
                    specialisation = new BOL.LISTING.Specialisation();

                SpecialisationVM.SetSpecialisation(specialisation);

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

                var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                if (listing.Steps < Constants.SpecialisationComplete)
                {
                    listing.Steps = Constants.SpecialisationComplete;
                    await listingService.UpdateAsync(listing);
                }

                navManager.NavigateTo($"/MyAccount/Listing/WorkingHours");
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
            finally
            {
                buttonBusy = false;
            }
        }
    }
}
