using AntDesign;
using BOL.LISTING;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BAL;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Company
    {

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        IUserService userService { get; set; }
        [Inject]
        IListingService listingService { get; set; }
        [Inject]
        ISharedService sharedService { get; set; }
        [Inject]
        Helper helper { get; set; }

        public CompanyVM CompanyVM { get; set; } = new CompanyVM();

        public bool buttonBusy { get; set; }

        public string CurrentUserGuid { get; set; }
        public bool IsCompanyExists { get; set; }

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
                    CompanyVM.NatureOfBusinesses = await sharedService.GetNatureOfBusinesses();
                    CompanyVM.Designations = await sharedService.GetDesignations();
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    if (listing != null)
                    {
                        IsCompanyExists = true;
                        CompanyVM.SetViewModel(listing);
                    }
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task AddOrUpdateCompanyAsync()
        {
            if (!CompanyVM.isValid())
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "All fields are compulsory.");
                return;
            }

            try
            {
                buttonBusy = true;
                var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                bool recordNotFound = listing == null;

                if (recordNotFound)
                    listing = new Listing();

                CompanyVM.SetContextModel(listing);
                
                if (recordNotFound)
                {
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    listing.OwnerGuid = CurrentUserGuid;
                    listing.CreatedDate = timeZoneDate;
                    listing.CreatedTime = timeZoneDate;
                    listing.IPAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    listing.Approved = false;

                    await listingService.AddAsync(listing);
                }
                else
                {
                    await listingService.UpdateAsync(listing);
                }

                navManager.NavigateTo($"/MyAccount/Listing/Communication");
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
