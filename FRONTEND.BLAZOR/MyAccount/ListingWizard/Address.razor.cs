using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using AntDesign;
using BOL.SHARED;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BOL;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Address
    {
        [Inject] private IHttpContextAccessor httpConAccess { get; set; }
        [Inject] private ISharedService sharedService { get; set; }
        [Inject] IUserService userService { get; set; }
        [Inject] IListingService listingService { get; set; }
        [Inject] Helper helper { get; set; }
        [Inject] BAL.HelperFunctions helperFunction { get; set; }

        LWAddressVM LWAddressVM { get; set; } = new LWAddressVM();

        public bool buttonBusy { get; set; }
        public string CurrentUserGuid { get; set; }
        public int ListingId { get; set; }
        public int Steps { get; set; }
        public bool IsAddressExist { get; set; }


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
                    var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                    helper.NavigateToPageByStep(listing, Constants.CommunicationComplete, navManager);
                    
                    ListingId = listing.ListingID;
                    Steps = listing.Steps;
                    var address = await listingService.GetAddressByListingId(ListingId);
                    if (address != null)
                    {
                        LWAddressVM.CountryId = address.CountryID;
                        IsAddressExist = true;
                        LWAddressVM.SetViewModel(address);
                    }
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async void ExecuteStateHasChanged()
        {
            //StateHasChanged();
        }

        public async Task GetLocalitiesByCityId()
        {
            await helperFunction.GetLocalitiesByCityId(LWAddressVM);
            StateHasChanged();
        }

        public async Task GetPincodesByAreaId()
        {
            await helperFunction.GetPincodesByLocalityId(LWAddressVM);
            StateHasChanged();
        }

        public async Task GetLocalitiesByPincodeId()
        {
            await helperFunction.GetAreasByPincodeId(LWAddressVM);
            StateHasChanged();
        }

        public async Task AddOrUpdateAddress()
        {
            if (!LWAddressVM.isValid())
            {
                helper.ShowNotification(_notice, $"Country, State, City, Area, Pincode, Locality and Address is Compulsory.", NotificationType.Info);
                return;
            }

            try
            {
                buttonBusy = true;
                var address = await listingService.GetAddressByListingId(ListingId);
                bool recordNotFound = address == null;

                if (recordNotFound)
                    address = new BOL.LISTING.Address();

                LWAddressVM.SetContextModel(address);

                if (recordNotFound)
                {
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    address.ListingID = ListingId;
                    address.OwnerGuid = CurrentUserGuid;
                    address.IPAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();

                    await listingService.AddAsync(address);
                }
                else
                {
                    await listingService.UpdateAsync(address);
                }

                await listingService.UpdateListingStepByOwnerId(CurrentUserGuid, Constants.AddressComplete, Steps);
                navManager.NavigateTo($"/MyAccount/Listing/Category");
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
