using AntDesign;
using BAL.Services.Contracts;
using BOL.SHARED;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class UserProfileEdit
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        public IUserProfileService userProfileService { get; set; }

        // Begin: Check if record exisit with listingId
        public string currentPage = "nav-address";
        public bool buttonBusy { get; set; }
        public bool disable { get; set; }

        public string CurrentUserGuid { get; set; }
        public string ErrorMessage { get; set; }
        public bool userAuthenticated { get; set; } = false;
        public string IpAddress { get; set; }
        public ApplicationUser iUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedTime { get; set; }

        // Properties
        public string OwnerGuid { get; set; }
        public string IpAddressUser { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? CountryID { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        public int? AreaID { get; set; }
        public int? PincodeID { get; set; }
        public string TimeZoneOfCountry { get; set; }

        // Other Properties
        public BOL.IDENTITY.UserProfile CurrentUserProfile { get; set; }

        public IList<string> timeZoneList = new List<string>();

        public Country country { get; set; }
        public State state { get; set; }
        public City city { get; set; }
        public Station station { get; set; }
        public Pincode pincode { get; set; }

        // Begin: Set Properties
        public async Task SetPropertiesAsync()
        {
            if(CurrentUserProfile != null)
            {
                OwnerGuid = CurrentUserProfile.OwnerGuid;
                IpAddressUser = CurrentUserProfile.IPAddress;
                Name = CurrentUserProfile.Name;
                Gender = CurrentUserProfile.Gender;
                DateOfBirth = CurrentUserProfile.DateOfBirth;
                CountryID = CurrentUserProfile.CountryID;
                StateID = CurrentUserProfile.StateID;
                CityID = CurrentUserProfile.CityID;
                AreaID = CurrentUserProfile.AssemblyID;
                PincodeID = CurrentUserProfile.PincodeID;
                TimeZoneOfCountry = CurrentUserProfile.TimeZoneOfCountry;
            }
        }
        // End: Set Properties

        public async Task GetLocationAsync()
        {
            if(CurrentUserProfile != null)
            {
                country = await sharedContext.Country.FindAsync(CurrentUserProfile.CountryID);

                state = await sharedContext.State.FindAsync(CurrentUserProfile.StateID);

                city = await sharedContext.City.FindAsync(CurrentUserProfile.CityID);

                station = await sharedContext.Station.FindAsync(CurrentUserProfile.AssemblyID);
                pincode = await sharedContext.Pincode.FindAsync(CurrentUserProfile.PincodeID);
            }
        }

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
            CountryID = Convert.ToInt32(e.Value.ToString());

            listState = await sharedContext.State
                .OrderBy(i => i.Name)
                .Where(i => i.CountryID == CountryID)
                .ToListAsync();
        }
        // End: State

        // Begin: City
        public IList<City> listCity = new List<City>();
        public async Task ListCityAsync(ChangeEventArgs e)
        {
            StateID = Convert.ToInt32(e.Value.ToString());

            listCity = await sharedContext.City
                .OrderBy(i => i.Name)
                .Where(i => i.StateID == StateID)
                .ToListAsync();
        }
        // End: City

        // Begin: Area
        public IList<Station> listArea = new List<Station>();
        public async Task ListAreaAsync(ChangeEventArgs e)
        {
            CityID = Convert.ToInt32(e.Value.ToString());

            listArea = await sharedContext.Station
                .OrderBy(i => i.Name)
                .Where(i => i.CityID == CityID)
                .ToListAsync();
        }
        // End: Area

        // Begin: Pincode
        public IList<Pincode> listPincode = new List<Pincode>();
        public async Task ListPincodeAsync(ChangeEventArgs e)
        {
            AreaID = Convert.ToInt32(e.Value.ToString());

            listPincode = await sharedContext.Pincode
                .OrderBy(i => i.PincodeNumber)
                .Where(i => i.StationID == AreaID)
                .ToListAsync();
        }
        // End: Pincode

        // Begin: Get Time Zone
        public async Task GetTimeZoneAsync()
        {
            var listTimeZone = TimeZoneInfo.GetSystemTimeZones().OrderByDescending(t => t.Id == "India Standard Time").ThenBy(t => t.DisplayName);

            foreach (var i in listTimeZone)
            {
                timeZoneList.Add(i.StandardName);
            }
            await Task.Delay(1);
        }
        // End: Get Time Zone

        // Begin: Toggle Disable
        public async Task ToggleDisableAsync()
        {
            disable = !disable;
            await Task.Delay(1);
        }
        // End: Toggle Disable

        // Begin: Check If Profile Exist
        public async Task GetCurrentUserProfile()
        {
            CurrentUserProfile = await userProfileService.GetProfileByOwnerGuid(CurrentUserGuid);

            if (CurrentUserProfile != null)
            {
                disable = true;
            }
            else
            {
                disable = false;
            }
        }
        // End: Check If Profile Exist

        // Begin: Create Profile
        public async Task UpdateProfileAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(IpAddressUser) == false && string.IsNullOrEmpty(Name) == false && string.IsNullOrEmpty(Gender) == false && DateOfBirth != null && CountryID != null && StateID != null && CityID != null && PincodeID != null && string.IsNullOrEmpty(TimeZoneOfCountry) == false)
                {

                    if (CurrentUserProfile != null)
                    {
                        try
                        {
                            CurrentUserProfile.OwnerGuid = CurrentUserGuid;
                            CurrentUserProfile.IPAddress = IpAddressUser;
                            CurrentUserProfile.Name = Name;
                            CurrentUserProfile.Gender = Gender;
                            CurrentUserProfile.DateOfBirth = DateOfBirth.Value;
                            CurrentUserProfile.CountryID = CountryID.Value;
                            CurrentUserProfile.StateID = StateID.Value;
                            CurrentUserProfile.CityID = CityID.Value;
                            CurrentUserProfile.AssemblyID = AreaID.Value;
                            CurrentUserProfile.PincodeID = PincodeID.Value;
                            CurrentUserProfile.TimeZoneOfCountry = TimeZoneOfCountry;

                            applicationContext.Update(CurrentUserProfile);
                            await applicationContext.SaveChangesAsync();

                            disable = true;

                            await GetLocationAsync();

                            // Show notification
                            await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your profile updated successfully.");
                        }
                        catch (Exception exc)
                        {
                            // Show notification
                            await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
                        }
                    }
                    else
                    {
                        // Show notification
                        await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "User profile does not exists.");
                    }
                }
                else
                {
                    // Show notification
                    await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "All fields are compulsory.");
                }
            }
            catch (Exception exc)
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }
        // End: Create Profile

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
                    IpAddressUser = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    CreatedDate = timeZoneDate;
                    CreatedTime = timeZoneDate;
                    // End:

                    iUser = await userService.GetUserByUserNameOrEmail(user.Identity.Name);
                    CurrentUserGuid = iUser.Id;

                    userAuthenticated = true;

                    await GetCurrentUserProfile();
                    await GetLocationAsync();
                    await SetPropertiesAsync();
                    await ListCountryAsync();
                    await GetTimeZoneAsync();


                    if(CurrentUserProfile == null)
                    {
                        navManager.NavigateTo("/MyAccount/UserProfile");
                    }
                }
            }
            catch (Exception exc)
            {
                ErrorMessage = exc.Message;
            }
        }
    }
}