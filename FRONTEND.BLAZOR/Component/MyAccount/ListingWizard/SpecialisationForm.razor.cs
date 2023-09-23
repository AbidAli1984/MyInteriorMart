using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using BAL.Services.Contracts;
using BOL;
using BOL.ComponentModels.MyAccount.ListingWizard;
using Microsoft.AspNetCore.Components.Authorization;

namespace FRONTEND.BLAZOR.Component.MyAccount.ListingWizard
{
    public partial class SpecialisationForm
    {
        [Parameter] public int MenuId { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] IUserService userService { get; set; }
        [Inject] IListingService listingService { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Inject] NavigationManager navManager { get; set; }

        public SpecialisationVM SpecialisationVM { get; set; } = new SpecialisationVM();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public int Steps { get; set; }
        public bool IsSpecialisationExist { get; set; }
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
                    helper.NavigateToPageByStep(listing, Constants.CategoryComplete, navManager);

                    ListingId = listing.ListingID;
                    Steps = listing.Steps;
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
                helper.ShowNotification(_notice, $"Please select at least one specialisation.", NotificationType.Info);
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

                await listingService.UpdateListingStepByOwnerId(CurrentUserGuid, Constants.SpecialisationComplete, Steps);
                navManager.NavigateTo($"{url}/WorkingHours");
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
