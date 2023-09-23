using AntDesign;
using BOL.LISTING;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BAL;
using System.Linq;
using Microsoft.AspNetCore.Components.Authorization;

namespace FRONTEND.BLAZOR.Component.MyAccount.ListingWizard
{
    public partial class CompanyForm
    {
        [Parameter] public int MenuId { get; set; }
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] IUserService userService { get; set; }
        [Inject] IListingService listingService { get; set; }
        [Inject] ISharedService sharedService { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] AuthenticationStateProvider authenticationState { get; set; }
        [Inject] NotificationService _notice { get; set; }
        [Inject] NavigationManager navManager { get; set; }

        public CompanyVM CompanyVM { get; set; } = new CompanyVM();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public bool IsCompanyExists { get; set; }
        public string url;

        protected async override Task OnInitializedAsync()
        {
            try
            {
                url = BOL.Constants.getListingUrl(MenuId);
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
                    Constants.Keywords = await listingService.GetKeywords();
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
                helper.ShowNotification(_notice, "All fields are compulsory.", NotificationType.Info);
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

                navManager.NavigateTo($"{url}/Communication");
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, exc.Message, NotificationType.Info);
            }
            finally
            {
                buttonBusy = false;
            }
        }

        public void AddKeyword(AutoCompleteOption autoCompleteOption)
        {
            CompanyVM.BusinessCategory = autoCompleteOption.Label;
        }

        private void SearchBusinessCategory(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                Constants.ArrKeywords = Constants.Keywords.Select(x => x.label).ToArray();
                CompanyVM.BusinessCategory = string.Empty;
            }
            else
            {
                var filterKey = Constants.Keywords
                    .Select(x => x.label)
                    .Where(x => x.ToLower().Contains(searchText.ToLower()));

                Constants.ArrKeywords = filterKey.Any() ? filterKey.ToArray() : null;
                if (Constants.ArrKeywords == null)
                    CompanyVM.BusinessCategory = searchText;
            }
        }
    }
}
