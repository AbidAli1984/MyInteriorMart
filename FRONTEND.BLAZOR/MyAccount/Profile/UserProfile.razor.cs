using AntDesign;
using BAL.Services.Contracts;
using BOL.ComponentModels.MyAccount.Profile;
using DAL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using IDUserProfile = BOL.IDENTITY.UserProfile;

namespace FRONTEND.BLAZOR.MyAccount.Profile
{
    public partial class UserProfile
    {
        [Inject]
        private IHttpContextAccessor httpConAccess { get; set; }
        [Inject]
        public IUserService userService { get; set; }
        [Inject]
        private IUserProfileService userProfileService { get; set; }

        public IDUserProfile userProfile { get; set; }

        EditProfileVM editProfileVM { get; set; }

        public bool disable { get; set; }
        public string CurrentUserGuid { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IpAddressUser { get; set; }
        public string TimeZoneOfCountry { get; set; }

        //public bool buttonBusy { get; set; }
        //public string ErrorMessage { get; set; }
        //public bool userAuthenticated { get; set; } = false;
        //public string IpAddress { get; set; }
        //public string OwnerGuid { get; set; }
        //public string Name { get; set; }
        //public string Gender { get; set; }
        //public string Email { get; set; }
        //public string Phone { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        //public int? CountryID { get; set; }
        //public int? StateID { get; set; }
        //public int? CityID { get; set; }
        //public int? AreaID { get; set; }
        //public int? PincodeID { get; set; }
        //public DateTime CreatedTime { get; set; }


        //public IList<string> timeZoneList = new List<string>();

        // Begin: Country
        //public IList<Country> listCountry = new List<Country>();
        //public async Task ListCountryAsync()
        //{
        //    try
        //    {
        //        listCountry = await sharedContext.Country.OrderBy(i => i.Name).ToListAsync();
        //    }
        //    catch (Exception exc)
        //    {
        //        ErrorMessage = exc.Message;
        //    }

        //}
        // End: Country

        // Begin: State
        //public IList<State> listState = new List<State>();
        //public async Task ListStateAsync(ChangeEventArgs e)
        //    {
        //    CountryID = Convert.ToInt32(e.Value.ToString());

        //    listState = await sharedContext.State
        //        .OrderBy(i => i.Name)
        //        .Where(i => i.CountryID == CountryID)
        //        .ToListAsync();
        //}
        // End: State

        // Begin: City
        //public IList<City> listCity = new List<City>();
        //public async Task ListCityAsync(ChangeEventArgs e)
        //{
        //    StateID = Convert.ToInt32(e.Value.ToString());

        //    listCity = await sharedContext.City
        //        .OrderBy(i => i.Name)
        //        .Where(i => i.StateID == StateID)
        //        .ToListAsync();
        //}
        // End: City

        // Begin: Area
        //public IList<Station> listArea = new List<Station>();
        //public async Task ListAreaAsync(ChangeEventArgs e)
        //{
        //    CityID = Convert.ToInt32(e.Value.ToString());

        //    listArea = await sharedContext.Station
        //        .OrderBy(i => i.Name)
        //        .Where(i => i.CityID == CityID)
        //        .ToListAsync();
        //}
        // End: Area

        // Begin: Pincode
        //public IList<Pincode> listPincode = new List<Pincode>();
        //public async Task ListPincodeAsync(ChangeEventArgs e)
        //{
        //    AreaID = Convert.ToInt32(e.Value.ToString());

        //    listPincode = await sharedContext.Pincode
        //        .OrderBy(i => i.PincodeNumber)
        //        .Where(i => i.StationID == AreaID)
        //        .ToListAsync();
        //}
        // End: Pincode

        // Begin: Get Time Zone
        //public async Task GetTimeZoneAsync()
        //{
        //    var listTimeZone = TimeZoneInfo.GetSystemTimeZones().OrderByDescending(t => t.Id == "India Standard Time").ThenBy(t => t.DisplayName);

        //    foreach (var i in listTimeZone)
        //    {
        //        timeZoneList.Add(i.StandardName);
        //    }
        //    await Task.Delay(1);
        //}
        // End: Get Time Zone

        //public async Task ToggleDisableAsync()
        //{
        //    disable = !disable;
        //    await Task.Delay(1);
        //}

        protected async override Task OnInitializedAsync()
        {
            try
            {
                editProfileVM = new EditProfileVM();
                var authstate = await authenticationState.GetAuthenticationStateAsync();
                var user = authstate.User;
                if (user.Identity.IsAuthenticated)
                {
                    TimeZoneOfCountry = "India Standard Time";
                    IpAddressUser = httpConAccess.HttpContext.Connection.RemoteIpAddress.ToString();
                    disable = true;

                    DateTime timeZoneDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(TimeZoneOfCountry));
                    CreatedDate = timeZoneDate.Date;

                    ApplicationUser applicationUser = await userService.GetUserByUserNameOrEmail(user.Identity.Name);
                    CurrentUserGuid = applicationUser.Id;
                    editProfileVM.Email = applicationUser.Email;
                    editProfileVM.Phone = applicationUser.PhoneNumber;
                    editProfileVM.isVendor = applicationUser.IsVendor;

                    await GetCurrentUserProfile();
                    //await ListCountryAsync();
                    //await GetTimeZoneAsync();
                }
            }
            catch (Exception exc)
            {

            }
        }

        public async Task GetCurrentUserProfile()
        {
            userProfile = await userProfileService.GetProfileByOwnerGuid(CurrentUserGuid);

            if (userProfile != null)
            {
                editProfileVM.Gender = userProfile.Gender;
                editProfileVM.Name = userProfile.Name;
            }
        }

        private async Task<bool> isValidFields()
        {
            if (string.IsNullOrEmpty(editProfileVM.Gender) || string.IsNullOrEmpty(editProfileVM.Name))
            {
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "All fields are compulsory.");
                return false;
            }

            return true;
        }

        public async Task CreateProfileAsync()
        {
            if (!(await isValidFields()))
                return;

            if (userProfile != null)
            {
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "User profile already exists.");
                return;
            }

            try
            {
                IDUserProfile userProfile = new IDUserProfile
                {
                    OwnerGuid = CurrentUserGuid,
                    IPAddress = IpAddressUser,
                    Name = editProfileVM.Name,
                    Gender = editProfileVM.Gender,
                    CreatedDate = CreatedDate,
                    TimeZoneOfCountry = TimeZoneOfCountry
                };

                await userProfileService.AddUserProfile(userProfile);
                await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your profile created successfully.");
            }
            catch (Exception exc)
            {
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }

        public async Task UpdateProfileAsync()
        {
            if (!(await isValidFields()))
                return;

            if (userProfile == null)
            {
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", "User profile does not exists.");
                return;
            }

            try
            {
                userProfile.OwnerGuid = CurrentUserGuid;
                userProfile.IPAddress = IpAddressUser;
                userProfile.Name = editProfileVM.Name;
                userProfile.Gender = editProfileVM.Gender;
                CreatedDate = CreatedDate;
                userProfile.TimeZoneOfCountry = TimeZoneOfCountry;

                await userProfileService.UpdateUserProfile(userProfile);

                // Show notification
                await NoticeWithIcon(NotificationType.Success, NotificationPlacement.BottomRight, "Success", "Your profile updated successfully.");
            }
            catch (Exception exc)
            {
                // Show notification
                await NoticeWithIcon(NotificationType.Error, NotificationPlacement.BottomRight, "Error", exc.Message);
            }
        }

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
    }
}