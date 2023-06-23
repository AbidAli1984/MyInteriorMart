using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FRONTEND.BLAZOR.Services;
using AntDesign;
using DAL.SHARED;
using BOL.SHARED;

namespace FRONTEND.BLAZOR.MyAccount.ListingWizard
{
    public partial class AddressEdit
    {
        // Begin: Check if record exisit with listingId
        [Parameter]
        public int? listingId { get; set; }

        public string currentPage = "nav-address";
        public bool buttonBusy { get; set; }

        // Begin: Toggle Edit
        public bool toggleEdit { get; set; } = true;

        public async Task ToggleEditAsync()
        {
            toggleEdit = !toggleEdit;
            await Task.Delay(5);
        }
        // End: Toggle Edit

        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public IdentityUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }

        // Begin: Check if record exists
        public bool companyExist { get; set; }
        public bool communicationExist { get; set; }
        public bool addressExist { get; set; }
        public bool categoryExist { get; set; }
        public bool specialisationExist { get; set; }
        public bool workingHoursExist { get; set; }
        public bool paymentModeExist { get; set; }

        public async Task CompanyExistAsync()
        {
            if (listingId != null)
            {
                var company = await listingContext.Listing.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (company != null)
                {
                    companyExist = true;
                }
                else
                {
                    companyExist = false;
                }
            }
        }

        public async Task CommunicationExistAsync()
        {
            if (listingId != null)
            {
                var communication = await listingContext.Communication.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (communication != null)
                {
                    communicationExist = true;
                }
                else
                {
                    communicationExist = false;
                }
            }
        }

        public async Task AddressExistAsync()
        {
            if (listingId != null)
            {
                var address = await listingContext.Address.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (address != null)
                {
                    addressExist = true;
                }
                else
                {
                    addressExist = false;
                }
            }
        }

        public async Task CategoryExistAsync()
        {
            if (listingId != null)
            {
                var category = await listingContext.Categories.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (category != null)
                {
                    categoryExist = true;
                }
                else
                {
                    categoryExist = false;
                }
            }
        }

        public async Task SpecialisationExistAsync()
        {
            if (listingId != null)
            {
                var specialisation = await listingContext.Specialisation.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (specialisation != null)
                {
                    specialisationExist = true;
                }
                else
                {
                    specialisationExist = false;
                }
            }
        }

        public async Task WorkingHoursExistAsync()
        {
            if (listingId != null)
            {
                var wh = await listingContext.WorkingHours.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (wh != null)
                {
                    workingHoursExist = true;
                }
                else
                {
                    workingHoursExist = false;
                }
            }
        }

        public async Task PaymentModeExistAsync()
        {
            if (listingId != null)
            {
                var pm = await listingContext.PaymentMode.Where(i => i.ListingID == listingId).FirstOrDefaultAsync();

                if (pm != null)
                {
                    paymentModeExist = true;
                }
                else
                {
                    paymentModeExist = false;
                }
            }
        }
        // End: Check if record exists

        // Properties
        public int? countryId { get; set; }
        public int? stateId { get; set; }
        public int? cityId { get; set; }
        public int? areaId { get; set; }
        public int? pincodeId { get; set; }
        public int? localityId { get; set; }
        public string address { get; set; }

        public Country selectedCountry { get; set; }
        public State selectedState { get; set; }
        public City selectedCity { get; set; }
        public Station selectedArea { get; set; }
        public Pincode selectedPincode { get; set; }
        public Locality selectedLocality { get; set; }

        // Begin: Find Address
        public async Task FindAddressAsync()
        {

            if(listingId != null)
            {
                var add = await listingContext.Address
                .Where(i => i.ListingID == listingId)
                .FirstOrDefaultAsync();

                if (add != null)
                {
                    countryId = add.CountryID;
                    stateId = add.StateID;
                    cityId = add.City;
                    areaId = add.AddressID;
                    pincodeId = add.PincodeID;
                    localityId = add.LocalityID;
                    address = add.LocalAddress;

                    selectedCountry = await sharedContext.Country.FindAsync(add.CountryID);
                    selectedState = await sharedContext.State.FindAsync(add.StateID);
                    selectedCity = await sharedContext.City.FindAsync(add.City);
                    selectedArea = await sharedContext.Station.FindAsync(add.AssemblyID);
                    selectedPincode = await sharedContext.Pincode.FindAsync(add.PincodeID);
                    selectedLocality = await sharedContext.Locality.FindAsync(add.LocalityID);
                }
            }
        }
        // End: Find Address

        // Begin: Create Address
        public async Task UpdateAddressAsync()
        {
            buttonBusy = true;

            try
            {
                if (listingId != null && countryId != null && stateId != null && cityId != null && areaId != null && pincodeId != null && localityId != null && !string.IsNullOrEmpty(address))
                {
                    try
                    {
                        var add = await listingContext.Address
                            .Where(i => i.ListingID == listingId)
                            .FirstOrDefaultAsync();

                        add.CountryID = countryId.Value;
                        add.StateID = stateId.Value;
                        add.City = cityId.Value;
                        add.AssemblyID = areaId.Value;
                        add.PincodeID = pincodeId.Value;
                        add.LocalityID = localityId.Value;
                        add.LocalAddress = address;

                        listingContext.Update(add);
                        await listingContext.SaveChangesAsync();

                        if(categoryExist == true)
                        {
                            // Navigate To
                            navManager.NavigateTo($"/MyAccount/ListingWizard/CategoryEdit/{listingId}");
                        }
                        else
                        {
                            // Navigate To
                            navManager.NavigateTo($"/MyAccount/ListingWizard/Category/{listingId}");
                        }
                    }
                    catch (Exception exc)
                    {
                        // Show notification
                        await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);

                        buttonBusy = false;
                    }
                }
                else
                {
                    // Show notification
                    await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Country, State, City, Area, Pincode, Locality and Address is Compulsory.");

                    buttonBusy = false;
                }
            }
            catch (Exception exc)
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Error", exc.Message);

                buttonBusy = false;
            }
        }
        // End: Create Address

        // Begin: Country
        public IList<Country> listCountry = new List<Country>();
        public async Task ListCountryAsync()
        {
            try
            {
                listCountry = await sharedContext.Country.OrderBy(i => i.Name).ToListAsync();
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }

        }
        // End: Country

        // Begin: State
        public IList<State> listState = new List<State>();
        public async Task ListStateAsync(ChangeEventArgs e)
        {
            countryId = Convert.ToInt32(e.Value.ToString());

            listState = await sharedContext.State
                .OrderBy(i => i.Name)
                .Where(i => i.CountryID == countryId)
                .ToListAsync();

            selectedState = null;
        }
        // End: State

        // Begin: City
        public IList<City> listCity = new List<City>();
        public async Task ListCityAsync(ChangeEventArgs e)
        {
            stateId = Convert.ToInt32(e.Value.ToString());

            listCity = await sharedContext.City
                .OrderBy(i => i.Name)
                .Where(i => i.StateID == stateId)
                .ToListAsync();


            selectedCity = null;
        }
        // End: City

        // Begin: Area
        public IList<Station> listArea = new List<Station>();
        public async Task ListAreaAsync(ChangeEventArgs e)
        {
            cityId = Convert.ToInt32(e.Value.ToString());

            listArea = await sharedContext.Station
                .OrderBy(i => i.Name)
                .Where(i => i.CityID == cityId)
                .ToListAsync();

            selectedArea = null;
        }
        // End: Area

        // Begin: Pincode
        public IList<Pincode> listPincode = new List<Pincode>();
        public async Task ListPincodeAsync(ChangeEventArgs e)
        {
            areaId = Convert.ToInt32(e.Value.ToString());

            listPincode = await sharedContext.Pincode
                .OrderBy(i => i.PincodeNumber)
                .Where(i => i.StationID == areaId)
                .ToListAsync();

            selectedPincode = null;
        }
        // End: Pincode

        // Begin: Locality
        public IList<Locality> listLocality = new List<Locality>();
        public async Task ListLocalityAsync(ChangeEventArgs e)
        {
            pincodeId = Convert.ToInt32(e.Value.ToString());

            listLocality = await sharedContext.Locality
                .OrderBy(i => i.LocalityName)
                .Where(i => i.PincodeID == pincodeId)
                .ToListAsync();

            selectedLocality = null;
        }
        // End: Locality

        // Begin: Get Locality Id
        public async Task GetLocalityIdAsync(ChangeEventArgs e)
        {
            localityId = Convert.ToInt32(e.Value.ToString());
            await Task.Delay(5);
        }
        // End: Get Locality Id

        // Begin: All Popups
        public string CreateButtonText = "Create";
        public bool CreateBusy = false;

        // Begin: Area Popup
        public bool showAreaModal { get; set; } = false;
        public string AreaName { get; set; }

        public async Task ToggleShowAreaModal()
        {
            showAreaModal = !showAreaModal;
            await Task.Delay(5);
        }

        // Update Area List
        public async Task UpdateAreaListAsync()
        {
            listArea = await sharedContext.Station
                .OrderBy(i => i.Name)
                .Where(i => i.CityID == cityId)
                .ToListAsync();
        }

        // Create Area
        public async Task CreateAreaAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(AreaName) && countryId != null && stateId != null && cityId != null)
                {
                    var duplicate = await sharedContext.Station
                        .Where(i => i.Name == AreaName)
                        .FirstOrDefaultAsync();

                    if (duplicate == null)
                    {
                        // Create new area or station
                        Station station = new Station
                        {
                            CityID = cityId,
                            Name = AreaName
                        };

                        await sharedContext.AddAsync(station);
                        await sharedContext.SaveChangesAsync();

                        // Update Area
                        await UpdateAreaListAsync();

                        // Toggle Area Model
                        await ToggleShowAreaModal();

                        // Get Station
                        var city = await sharedContext.City.FindAsync(cityId);

                        // Show notification
                        await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Area {AreaName} created inside city {city.Name}.");

                        // Empty AreaName Value
                        AreaName = null;
                    }
                    else
                    {
                        // Show notification
                        await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Area {AreaName} already exists.");
                    }
                }
                else
                {
                    // Show notification
                    await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State and City must be selected and area name must not be blank.");
                }
            }
            catch (Exception exc)
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
        // End: Area Popup


        // Begin: Pincode Popup
        public bool showPincodeModal { get; set; } = false;
        public int? PincodeNumber { get; set; }
        public async Task ToggleShowPincodeModal()
        {
            showPincodeModal = !showPincodeModal;
            await Task.Delay(5);
        }

        // Update Pincode List
        public async Task UpdatePincodeListAsync()
        {
            listPincode = await sharedContext.Pincode
                .OrderBy(i => i.PincodeNumber)
                .Where(i => i.StationID == areaId)
                .ToListAsync();
        }

        // Create Pincode
        public async Task CreatePincodeAsync()
        {
            try
            {
                if (PincodeNumber != null && countryId != null && stateId != null && cityId != null && areaId != null)
                {
                    var duplicate = await sharedContext.Pincode
                        .Where(i => i.PincodeNumber == PincodeNumber.Value)
                        .FirstOrDefaultAsync();

                    if (duplicate == null)
                    {
                        // Create new pincode
                        Pincode pincode = new Pincode
                        {
                            StationID = areaId,
                            PincodeNumber = PincodeNumber.Value
                        };

                        await sharedContext.AddAsync(pincode);
                        await sharedContext.SaveChangesAsync();

                        // Update Pincode
                        await UpdatePincodeListAsync();

                        // Toggle Area Model
                        await ToggleShowPincodeModal();

                        // Get Station
                        var station = await sharedContext.Station.FindAsync(areaId);

                        // Show notification
                        await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Pincode {PincodeNumber} created inside {station.Name}.");

                        // Empty PincodeNumber Value
                        PincodeNumber = null;
                    }
                    else
                    {
                        // Show notification
                        await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Pincode {PincodeNumber} already exists.");
                    }
                }
                else
                {
                    // Show notification
                    await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State, City and Area must be selected and pincode number must not be blank.");
                }
            }
            catch (Exception exc)
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
        // End: Area Popup
        // End: Pincode Popup

        // Begin: Locality Popup
        public bool showLocalityModal { get; set; } = false;
        public string LocalityName { get; set; }
        public async Task ToggleShowLocalityModal()
        {
            showLocalityModal = !showLocalityModal;
            await Task.Delay(5);
        }

        // Update Locality List
        public async Task UpdateLocalityListAsync()
        {
            listLocality = await sharedContext.Locality
                .OrderBy(i => i.LocalityName)
                .Where(i => i.LocalityID == localityId)
                .ToListAsync();
        }

        // Create Pincode
        public async Task CreateLocalityAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(LocalityName) && countryId != null && stateId != null && cityId != null && areaId != null && pincodeId != null)
                {
                    var duplicate = await sharedContext.Locality
                        .Where(i => i.LocalityName == LocalityName)
                        .FirstOrDefaultAsync();

                    if (duplicate == null)
                    {
                        // Create new pincode
                        Locality locality = new Locality
                        {
                            LocalityName = LocalityName,
                            PincodeID = pincodeId,
                            StationID = areaId
                        };

                        await sharedContext.AddAsync(locality);
                        await sharedContext.SaveChangesAsync();

                        // Update locality
                        await UpdateLocalityListAsync();

                        // Toggle Locality Model
                        await ToggleShowLocalityModal();

                        // Get Station
                        var pincode = await sharedContext.Pincode.FindAsync(pincodeId);

                        // Show notification
                        await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Success", $"Locality {LocalityName} created inside pincode {pincode.PincodeNumber}.");

                        // Empty PincodeNumber Value
                        LocalityName = null;
                    }
                    else
                    {
                        // Show notification
                        await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Locality {LocalityName} already exists.");
                    }
                }
                else
                {
                    // Show notification
                    await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", $"Country, State, City, Area and Pincode must be selected and Area Name must not be blank.");
                }
            }
            catch (Exception exc)
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
        // End: Locality Popup
        // End: All Popups

        // Begin: Antdesign Blazor Notification
        private async Task NoticeWithIcon(NotificationType type, NotificationPlacement placement, string message, string description)
        {
            await _notice.Open(new NotificationConfig()
            {
                Message = message,
                Description = description,
                NotificationType = type,
                Placement = placement
            });
        }
        // End: Antdesign Blazor Notification

        protected async override Task OnInitializedAsync()
        {
            try
            {
                // Get User Name
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    // Shafi: Assign Time Zone to CreatedDate & Created Time
                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                    IpAddress = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    CreatedDate = timeZoneDate;
                    CreatedTime = timeZoneDate;
                    // End:

                    iUser = await applicationContext.Users.Where(i => i.UserName == user.Identity.Name).FirstOrDefaultAsync();
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;

                    // Begin: Check if record exists
                    await CompanyExistAsync();
                    await CommunicationExistAsync();
                    await AddressExistAsync();
                    await CategoryExistAsync();
                    await SpecialisationExistAsync();
                    await WorkingHoursExistAsync();
                    await PaymentModeExistAsync();
                    // End: Check if record exists

                    await FindAddressAsync();
                    await ListCountryAsync();
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}
