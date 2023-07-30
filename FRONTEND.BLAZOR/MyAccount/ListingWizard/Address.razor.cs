using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AntDesign;
using BOL.SHARED;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.ListingWizard;
using BAL;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class Address
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

        AddressVM AddressVM { get; set; } = new AddressVM();

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
                    if (listing == null)
                        navManager.NavigateTo("/MyAccount/Listing/Company");
                    else if (listing.Steps < Constants.CommunicationComplete) //Checking if prev steps compeleted
                        helper.NavigateToPageByStep(listing.Steps, navManager);

                    IsAddressExist = listing.Steps >= 3;
                    ListingId = listing.ListingID;
                    AddressVM.Countries = await sharedService.GetCountries();
                    var address = await listingService.GetAddressByOwnerId(CurrentUserGuid);
                    if (address != null)
                    {
                        AddressVM.CountryId = address.CountryID;
                        AddressVM.StateId = address.StateID;
                        AddressVM.CityId = address.City;
                        AddressVM.StationId = address.AssemblyID;
                        AddressVM.PincodeId = address.PincodeID;
                        AddressVM.LocalityId = address.LocalityID;
                        AddressVM.Address = address.LocalAddress;

                        await GetStateByCountryId(null);
                        await GetCityByStateId(null);
                        await GetAreaByCityId(null);
                        await GetPincodesByAreaId(null);
                        await GetLocalitiesByPincodeId(null);
                    }
                }
            }
            catch (Exception exc)
            {
                string ErrorMessage = exc.Message;
            }
        }

        public async Task GetStateByCountryId(ChangeEventArgs events)
        {
            await helper.GetStateByCountryId(AddressVM, events);
            StateHasChanged();
        }

        public async Task GetCityByStateId(ChangeEventArgs events)
        {
            await helper.GetCityByStateId(AddressVM, events);
            StateHasChanged();
        }

        public async Task GetAreaByCityId(ChangeEventArgs events)
        {
            await helper.GetAreaByCityId(AddressVM, events);
            StateHasChanged();
        }

        public async Task GetPincodesByAreaId(ChangeEventArgs events)
        {
            await helper.GetPincodesByAreaId(AddressVM, events);
            StateHasChanged();
        }

        public async Task GetLocalitiesByPincodeId(ChangeEventArgs events)
        {
            await helper.GetLocalitiesByPincodeId(AddressVM, events);
            StateHasChanged();
        }

        public void SetLocalityId(ChangeEventArgs events)
        {
            AddressVM.LocalityId = Convert.ToInt32(events.Value.ToString());
        }

        public async Task AddOrUpdateAddress()
        {
            if (!AddressVM.isValid())
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State, City, Area, Pincode, Locality and Address is Compulsory.");
                return;
            }

            try
            {
                buttonBusy = true;
                var address = await listingService.GetAddressByOwnerId(CurrentUserGuid);
                bool recordNotFound = address == null;

                if (recordNotFound)
                    address = new BOL.LISTING.Address();

                address.CountryID = AddressVM.CountryId;
                address.StateID = AddressVM.StateId;
                address.City = AddressVM.CityId;
                address.AssemblyID = AddressVM.StationId;
                address.PincodeID = AddressVM.PincodeId;
                address.LocalityID = AddressVM.LocalityId;
                address.LocalAddress = AddressVM.Address;

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

                var listing = await listingService.GetListingByOwnerId(CurrentUserGuid);
                if (listing.Steps < Constants.AddressComplete)
                {
                    listing.Steps = Constants.AddressComplete;
                    await listingService.UpdateAsync(listing);
                }

                navManager.NavigateTo($"/MyAccount/Listing/Category");
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

        public async Task ShowHideAreaModal()
        {
            if (AddressVM.CityId <= 0)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Please select Country, State and City first");
                return;
            }
            showAreaModal = !showAreaModal;
            await Task.Delay(5);
        }
        public async Task CreateAreaAsync()
        {
            if (string.IsNullOrEmpty(AreaName) || AddressVM.CityId <= 0)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State and City must be selected and area name must not be blank.");
                return;
            }
            try
            {
                var areaExist = await sharedService.GetAreaByAreaName(AreaName);
                if(areaExist != null)
                {
                    await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Area {AreaName} already exists.");
                    return;
                }

                Station station = new Station
                {
                    CityID = AddressVM.CityId,
                    Name = AreaName
                };

                await sharedService.AddAsync(station);
                await GetAreaByCityId(null);
                await ShowHideAreaModal();
                var city = await sharedService.GetCityByCityId(AddressVM.CityId);

                await helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Area {AreaName} created inside city {city.Name}.");
                AreaName = string.Empty;
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }

        public async Task ShowHidePincodeModal()
        {
            if (AddressVM.StationId <= 0)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Pleae select Country, State, City and Locality first.");
                return;
            }
            showPincodeModal = !showPincodeModal;
            await Task.Delay(5);
        }
        public async Task CreatePincodeAsync()
        {
            if (PinNumber <= 0 || AddressVM.StationId <= 0)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State, City and Area must be selected and pincode number must not be blank.");
                return;
            }
            try
            {
                var pincodeExist = await sharedService.GetPincodeByPinNumber(PinNumber);
                if (pincodeExist != null)
                {
                    await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Pincode {PinNumber} already exists.");
                    return;
                }

                Pincode pincode = new Pincode
                {
                    StationID = AddressVM.StationId,
                    PincodeNumber = PinNumber
                };

                await sharedService.AddAsync(pincode);
                await GetPincodesByAreaId(null);
                await ShowHidePincodeModal();
                var station = await sharedService.GetAreaByAreaId(AddressVM.StationId);

                await helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Pincode {PinNumber} created inside {station.Name}.");
                PinNumber = 0;
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }

        public async Task ShowHideLocalityModal()
        {
            if (AddressVM.PincodeId <= 0)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Pleae select Country, State, City, Locality and Pincode first.");
                return;
            }
            showLocalityModal = !showLocalityModal;
            await Task.Delay(5);
        }
        public async Task CreateLocalityAsync()
        {
            if (string.IsNullOrEmpty(LocalityName) || AddressVM.PincodeId <= 0)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State, City, Area and Pincode must be selected and Area Name must not be blank.");
                return;
            }
            try
            {
                var localityExist = await sharedService.GetLocalityByLocalityName(LocalityName);
                if (localityExist != null)
                {
                    await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Locality {LocalityName} already exists.");
                    return;
                }

                Locality locality = new Locality
                {
                    LocalityName = LocalityName,
                    PincodeID = AddressVM.PincodeId,
                    StationID = AddressVM.StationId
                };

                await sharedService.AddAsync(locality);
                await GetLocalitiesByPincodeId(null);
                await ShowHideLocalityModal();

                var pincode = await sharedService.GetPincodeByPincodeId(AddressVM.PincodeId);
                await helper.ShowNotification(_notice, NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Locality {LocalityName} created inside pincode {pincode.PincodeNumber}.");
                LocalityName = string.Empty;
            }
            catch (Exception exc)
            {
                await helper.ShowNotification(_notice, NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
    }
}
