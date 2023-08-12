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
        public bool IsAddressExist { get; set; }
        public bool showAreaModal { get; set; } = false;
        public string AreaName { get; set; }
        public bool showPincodeModal { get; set; } = false;
        public int PinNumber { get; set; }
        public bool showLocalityModal { get; set; } = false;
        public string LocalityName { get; set; }

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
                    //await helperFunction.GetCountries(LWAddressVM);
                    var address = await listingService.GetAddressByListingId(ListingId);
                    if (address != null)
                    {
                        LWAddressVM.CountryId = address.CountryID;
                        IsAddressExist = true;
                        LWAddressVM.SetViewModel(address);
                        //GetStateByCountryId();
                        //await GetCityByStateId();
                        //await GetAreaByCityId(null);
                        //await GetPincodesByAreaId(null);
                        //await GetLocalitiesByPincodeId(null);
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
            //await helperFunction.GetStateByCountryId(LWAddressVM);
            StateHasChanged();
        }

        public async Task GetCityByStateId()
        {
            await helperFunction.GetCityByStateId(LWAddressVM);
            StateHasChanged();
        }

        public async Task GetAreaByCityId(ChangeEventArgs events)
        {
            await helperFunction.GetAreaByCityId(LWAddressVM);
            StateHasChanged();
        }

        public async Task GetPincodesByAreaId(ChangeEventArgs events)
        {
            await helperFunction.GetPincodesByAreaId(LWAddressVM);
            StateHasChanged();
        }

        public async Task GetLocalitiesByPincodeId(ChangeEventArgs events)
        {
            await helperFunction.GetLocalitiesByPincodeId(LWAddressVM);
            StateHasChanged();
        }

        public void SetLocalityId(ChangeEventArgs events)
        {
            LWAddressVM.LocalityId = Convert.ToInt32(events.Value.ToString());
        }

        public async Task AddOrUpdateAddress()
        {
            if (!LWAddressVM.isValid())
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State, City, Area, Pincode, Locality and Address is Compulsory.");
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

                await listingService.UpdateListingStepByOwnerId(CurrentUserGuid, Constants.AddressComplete);
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

        public async Task ShowHideAreaModal()
        {
            if (LWAddressVM.CityId <= 0)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Please select Country, State and City first");
                return;
            }
            showAreaModal = !showAreaModal;
            await Task.Delay(5);
        }
        public async Task CreateAreaAsync()
        {
            if (string.IsNullOrEmpty(AreaName) || LWAddressVM.CityId <= 0)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State and City must be selected and area name must not be blank.");
                return;
            }
            try
            {
                var areaExist = await sharedService.GetAreaByAreaName(AreaName);
                if(areaExist != null)
                {
                    helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Area {AreaName} already exists.");
                    return;
                }

                Location station = new Location
                {
                    CityID = LWAddressVM.CityId,
                    Name = AreaName
                };

                await sharedService.AddAsync(station);
                await GetAreaByCityId(null);
                await ShowHideAreaModal();
                var city = await sharedService.GetCityByCityId(LWAddressVM.CityId);

                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Area {AreaName} created inside city {city.Name}.");
                AreaName = string.Empty;
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }

        public async Task ShowHidePincodeModal()
        {
            if (LWAddressVM.StationId <= 0)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Pleae select Country, State, City and Locality first.");
                return;
            }
            showPincodeModal = !showPincodeModal;
            await Task.Delay(5);
        }
        public async Task CreatePincodeAsync()
        {
            if (PinNumber <= 0 || LWAddressVM.StationId <= 0)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State, City and Area must be selected and pincode number must not be blank.");
                return;
            }
            try
            {
                var pincodeExist = await sharedService.GetPincodeByPinNumber(PinNumber);
                if (pincodeExist != null)
                {
                    helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Pincode {PinNumber} already exists.");
                    return;
                }

                Pincode pincode = new Pincode
                {
                    StationID = LWAddressVM.StationId,
                    PincodeNumber = PinNumber
                };

                await sharedService.AddAsync(pincode);
                await GetPincodesByAreaId(null);
                await ShowHidePincodeModal();
                var station = await sharedService.GetAreaByAreaId(LWAddressVM.StationId);

                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Pincode {PinNumber} created inside {station.Name}.");
                PinNumber = 0;
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }

        public async Task ShowHideLocalityModal()
        {
            if (LWAddressVM.PincodeId <= 0)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Pleae select Country, State, City, Locality and Pincode first.");
                return;
            }
            showLocalityModal = !showLocalityModal;
            await Task.Delay(5);
        }
        public async Task CreateLocalityAsync()
        {
            if (string.IsNullOrEmpty(LocalityName) || LWAddressVM.PincodeId <= 0)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State, City, Area and Pincode must be selected and Area Name must not be blank.");
                return;
            }
            try
            {
                var localityExist = await sharedService.GetLocalityByLocalityName(LocalityName);
                if (localityExist != null)
                {
                    helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Locality {LocalityName} already exists.");
                    return;
                }

                Area locality = new Area
                {
                    Name = LocalityName,
                    PincodeID = LWAddressVM.PincodeId,
                    LocationId = LWAddressVM.StationId
                };

                await sharedService.AddAsync(locality);
                await GetLocalitiesByPincodeId(null);
                await ShowHideLocalityModal();

                var pincode = await sharedService.GetPincodeByPincodeId(LWAddressVM.PincodeId);
                helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Locality {LocalityName} created inside pincode {pincode.PincodeNumber}.");
                LocalityName = string.Empty;
            }
            catch (Exception exc)
            {
                helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
    }
}
