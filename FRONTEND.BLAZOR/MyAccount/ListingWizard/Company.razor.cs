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
                        CompanyVM.Name = listing.Name;
                        CompanyVM.Gender = listing.Gender;
                        CompanyVM.YearOfEstablishment = listing.YearOfEstablishment;
                        CompanyVM.CompanyName = listing.CompanyName;
                        CompanyVM.GSTNumber = listing.GSTNumber;
                        CompanyVM.Turnover = listing.Turnover;
                        CompanyVM.NumberOfEmployees = listing.NumberOfEmployees;
                        CompanyVM.NatureOfBusiness = listing.NatureOfBusiness;
                        CompanyVM.Designation = listing.Designation;
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
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", "All fields are compulsory.");
                return;
            }

            try
            {
                buttonBusy = true;
                var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                bool recordNotFound = listing == null;

                if (recordNotFound)
                    listing = new Listing();

                listing.Name = CompanyVM.Name;
                listing.Gender = CompanyVM.Gender;
                listing.YearOfEstablishment = CompanyVM.YearOfEstablishment.Value;
                listing.CompanyName = CompanyVM.CompanyName;
                listing.GSTNumber = CompanyVM.GSTNumber;
                listing.Turnover = CompanyVM.Turnover;
                listing.NumberOfEmployees = CompanyVM.NumberOfEmployees;
                listing.NatureOfBusiness = CompanyVM.NatureOfBusiness;
                listing.Designation = CompanyVM.Designation;
                listing.ListingURL = CompanyVM.CompanyName.Replace(" ", "-");
                
                if (listing.Steps < Constants.CompanyComplete)
                    listing.Steps = Constants.CompanyComplete;

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
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
            finally
            {
                buttonBusy = false;
            }
        }
    }
}
